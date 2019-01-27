using UnityEngine;
using System;


public class ImpactComponent : EntityComponent
{
    [Header("Impact Values")]
    public float minHardBreach;

    [Header("Particles")]
    public float magnitude;
    public GameObject hitParticlesPrefab;
    public GameObject impactParticlesPrefab;

    [Header("Audio")]
    public AudioClip impactClip;


    //Flag
    [NonSerialized]
    public bool hardBreachEnabled;
    private bool hardBreachBackLog;

    //Component
    private CollisionComponent collisionComponent;
    private EnergyComponent energyComponent;
    private Rigidbody2D rbody;

    //Job
    private WaitJob waitJob;


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Parameters
        minHardBreach = GameController.Instance.GameParameter.minHardBreach;
        magnitude = GameController.Instance.GameParameter.spawnMagnitude;

        //Components
        collisionComponent = GetComponent<CollisionComponent>();
        energyComponent = GetComponent<EnergyComponent>();
        rbody = GetComponent<MappingComponent>().rbody;

        //Event
        collisionComponent.onCollision.AddListener(delegate
        {
            if (hardBreachEnabled) PlanetImpact(true);
            else PlanetImpact(false);
        });
    }


    public override void UpdateComponent()
    {
        if (rbody.velocity.magnitude >= minHardBreach)
        {
            hardBreachBackLog = true;
        }
        else
        {
            hardBreachBackLog = false;
        }


        //Set status
        if (hardBreachBackLog != hardBreachEnabled)
        {
            hardBreachEnabled = hardBreachBackLog;

            if (!hardBreachEnabled)
            {
                if (waitJob != null) waitJob.CancelJob();

                waitJob = new WaitJob(delegate 
                {
                    hardBreachEnabled = false; //Resets the hardbreach flag
                }, 0.4f); 
            }
        }
    }



    /// <summary>
    /// Triggers the impact on a planer´t,
    /// </summary>
    private void PlanetImpact(bool _impact)
    {
        if (collisionComponent.lastCollisionObject.CompareTag("Planet"))
        {
            PlanetComponent planet = collisionComponent.lastCollisionObject.GetComponent<EntityLink>().entityController.GetComponent<PlanetComponent>();

            if (rbody.velocity.magnitude >= magnitude) SpawnParticles(planet, ref collisionComponent.collision);

            if (!_impact)
            {
                energyComponent.currentEnergy -= (int)(rbody.velocity.magnitude * energyComponent.energyMalus);
            }

            if (planet.destroyable && !planet.dangerous && _impact)
            {
                DeathSimpleComponent.AddDeathSimpleComponent(collisionComponent.lastCollisionObject.GetComponent<EntityLink>().entityController);
                rbody.AddForce(-rbody.velocity / 2);
            }
        }
    }


    /// <summary>
    /// Spawns the particles of the impact.
    /// </summary>
    /// <param name="_planetComponent"></param>
    private void SpawnParticles(PlanetComponent _planetComponent, ref Collision2D _collision)
    {
        AudioSource.PlayClipAtPoint(impactClip, transform.position);

        Vector3 hit = _collision.contacts[0].point;
        Vector3 normal = -_collision.contacts[0].normal;

        Quaternion rotation = new Quaternion();
        rotation.SetLookRotation(normal);

        Instantiate(hitParticlesPrefab, hit, Quaternion.identity);
        Instantiate(impactParticlesPrefab, hit + (normal * _planetComponent.planetCollider.radius), rotation);
    }

}
