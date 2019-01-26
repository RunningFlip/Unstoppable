using UnityEngine;
using System;


/// <summary>
/// Handles the health and death of the entity.
/// </summary>
[RequireComponent(typeof(MappingComponent), typeof(StateComponent))]
public class EnergyComponent : EntityComponent
{
    [Header("Energy")]
    public int maxEnergy;
    private int lastMaxEnergy;
    public int currentEnergy;
    private int lastEnergy;

    [Header("Collision")]
    public int energyMalus;

    [Header("Audio")]
    public float volume;
    public AudioClip deathAudioClip;


    //Flag
    [NonSerialized]
    public bool isDead;

    //Components
    private StateComponent stateComponent;
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
        rbody = GetComponent<MappingComponent>().rbody;
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


    public static void AddEnergyComponent(EntityController _entityController, int _maxEnergy)
    {
        EnergyComponent component = _entityController.gameObject.AddComponent<EnergyComponent>();
        component.maxEnergy = _maxEnergy;
        _entityController.AddComponent(component);
    }
}
