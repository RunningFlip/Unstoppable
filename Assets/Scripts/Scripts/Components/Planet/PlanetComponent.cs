using UnityEngine;


public class PlanetComponent : EntityComponent
{
    [Header("Planet Attributes")]
    public bool destroyable;
    public bool dangerous;
    public bool harvestable;

    [Header("Harvest")]
    public int energyBonus;
    public int energyStorage;
    public int energyStorageStart;
    private const float harvestConst = 6f;
    private const int bonus = 5;

    [Header("Planet Colliders")]
    public CircleCollider2D planetCollider;
    public CircleCollider2D orbitCollider;

    //Events
    public SimpleEvent onDeath = new SimpleEvent();


    public override void InitializeComponent()
    {
        energyBonus = (int)(bonus * planetCollider.radius);
        energyStorage = (int)(harvestConst * planetCollider.radius);
        energyStorageStart = energyStorage;
    }

    public override void UpdateComponent() { }


    private void OnDestroy()
    {
        onDeath.Invoke();
    }
}

