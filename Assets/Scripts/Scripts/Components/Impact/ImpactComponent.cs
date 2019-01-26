using UnityEngine;
using System;


public class ImpactComponent : EntityComponent
{
    [Header("Impact Values")]
    public float minHardBreach;

    //Flag
    //[NonSerialized]
    public bool hardBreachEnabled;
    private bool hardBreachBackLog;

    //Component
    private CollisionComponent collisionComponent;
    private Rigidbody2D rbody;

    //Job
    private WaitJob waitJob;


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Parameters
        minHardBreach = GameController.Instance.GameParameter.minHardBreach;

        //Components
        collisionComponent = GetComponent<CollisionComponent>();
        rbody = GetComponent<MappingComponent>().rbody;

        //Event
        collisionComponent.onCollision.AddListener(delegate
        {
            if (hardBreachEnabled) PlanetImpact();
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
                    hardBreachEnabled = false;
                }, 0.4f); //Resets the hardbreach flag
            }
        }
    }


    /// <summary>
    /// Triggers the impact on a planer´t,
    /// </summary>
    private void PlanetImpact()
    {
        if (collisionComponent.lastCollision.CompareTag("Planet"))
        {
            DeathSimpleComponent.AddDeathSimpleComponent(collisionComponent.lastCollision.GetComponent<EntityLink>().entityController);
            rbody.AddForce(-rbody.velocity / 2);
        }
    }

}
