using UnityEngine;
using System;


/// <summary>
/// Handles the health and death of the entity.
/// </summary>
[RequireComponent(typeof(MappingComponent), typeof(StateComponent))]
public class EnergyComponent : EntityComponent
{
    [Header("Energy")]
    public float maxEnergy;
    private float lastMaxEnergy;
    public float currentEnergy;
    private float lastEnergy;

    [Header("Collision")]
    public float energyMalus;

    [Header("Audio")]
    public float volume;
    public AudioClip deathAudioClip;


    //Flag
    [NonSerialized]
    public bool isDead;

    //Components
    private StateComponent stateComponent;
    private CollisionComponent collisionComponent;
    private Rigidbody2D rbody;

    //Event
    public SimpleEvent onMaxHealthChanged = new SimpleEvent();
    public SimpleEvent onCurrentHealthChanged = new SimpleEvent();
    public SimpleEvent onDeath = new SimpleEvent();


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Health
        energyMalus = GameController.Instance.GameParameter.energyMalus;
        maxEnergy = GameController.Instance.GameParameter.maxEnergy;
        currentEnergy = maxEnergy;
        lastMaxEnergy = maxEnergy;
        lastEnergy = currentEnergy;

        //Components
        stateComponent = GetComponent<StateComponent>();
        collisionComponent = GetComponent<CollisionComponent>();
        rbody = GetComponent<MappingComponent>().rbody;

        //Event
        collisionComponent.onCollision.AddListener(delegate { CheckCollision(); });
    }


    public override void UpdateComponent()
    {
        if (!stateComponent.StateIsEnabled(StateType.Damageable)) return;

        //Max health
        if (maxEnergy != lastMaxEnergy)
        {
            lastMaxEnergy = maxEnergy;
            onMaxHealthChanged.Invoke();
        }

        //Current health
        if (currentEnergy != lastEnergy)
        {
            lastEnergy = currentEnergy;

            if (currentEnergy > maxEnergy) currentEnergy = maxEnergy;
            if (currentEnergy < 0) currentEnergy = 0;

            onCurrentHealthChanged.Invoke();
        }
    }


    /// <summary>
    /// Checks if the entity collides with an planet and decreases the energy value.
    /// </summary>
    private void CheckCollision()
    {
        if (collisionComponent.lastCollision.CompareTag("Planet"))
        {
            currentEnergy -= (rbody.velocity.magnitude * energyMalus);
        }
    }


    public static void AddEnergyComponent(EntityController _entityController, float _maxEnergy)
    {
        EnergyComponent component = _entityController.gameObject.AddComponent<EnergyComponent>();
        component.maxEnergy = _maxEnergy;
        _entityController.AddComponent(component);
    }
}
