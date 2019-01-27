using UnityEngine;
using System;


public class PlanetComponent : EntityComponent
{
    [Header("Planet Attributes")]
    public bool destroyable;  
    public bool harvestable;
    [NonSerialized]
    public bool dangerous;

    [Header("Harvest")]
    public int energyBonus;
    public int energyStorage;
    public int energyStorageStart;
    private const float harvestConst = 6f;
    private const int bonus = 5;

    [Header("Death")]
    public PlanetDeathType planetDeathType = PlanetDeathType.Normal_Death;
    private GameObject deathParticlePrefab;
    private GameObject deathStarPrefab;
    private GameObject blackHolePrefab;

    [Header("Planet Colliders")]
    public CircleCollider2D planetCollider;
    public CircleCollider2D orbitCollider;

    //Events
    public SimpleEvent onDeath = new SimpleEvent();


    public override void InitializeComponent()
    {
        dangerous = GetComponent<DamageEmitterComponent>();

        //Values
        energyBonus = (int)(bonus * planetCollider.radius);
        energyStorage = (int)(harvestConst * planetCollider.radius);
        energyStorageStart = energyStorage;

        //Prefabs
        deathParticlePrefab = GameController.Instance.GameParameter.deathParticlePrefab;
        deathStarPrefab = GameController.Instance.GameParameter.deathStarPrefabs[UnityEngine.Random.Range(0, GameController.Instance.GameParameter.deathStarPrefabs.Length)];
        blackHolePrefab = GameController.Instance.GameParameter.blackHolePrefab;
    }


    public override void UpdateComponent() { }

    
    private void OnDestroy()
    {
        onDeath.Invoke();
        HandleDeathType();
    }


    private void HandleDeathType()
    {
        switch (planetDeathType)
        {
            case PlanetDeathType.Normal_Death:
                Instantiate(deathParticlePrefab, planetCollider.transform.position, Quaternion.identity);
                break;

            case PlanetDeathType.Harvest_Death:
                if (dangerous)
                {
                    Instantiate(deathParticlePrefab, planetCollider.transform.position, Quaternion.identity);
                    Instantiate(blackHolePrefab, planetCollider.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(deathParticlePrefab, planetCollider.transform.position, Quaternion.identity);
                    Instantiate(deathStarPrefab, planetCollider.transform.position, Quaternion.identity);
                }               
                break;
        }
    }

}

