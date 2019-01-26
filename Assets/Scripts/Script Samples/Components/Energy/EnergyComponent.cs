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

    [Header("Audio")]
    public float volume;
    public AudioClip deathAudioClip;


    //Flag
    [NonSerialized]
    public bool isDead;

    //Components
    private MappingComponent mappingComponent;
    private StateComponent stateComponent;
    private AnimationComponent animationComponent;

    //Event
    public SimpleEvent onMaxHealthChanged = new SimpleEvent();
    public SimpleEvent onCurrentHealthChanged = new SimpleEvent();
    public SimpleEvent onDeath = new SimpleEvent();


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Health
        maxEnergy = GameController.Instance.GameParameter.maxEnergy;
        currentEnergy = maxEnergy;
        lastMaxEnergy = maxEnergy;
        lastEnergy = currentEnergy;

        //Components
        mappingComponent = GetComponent<MappingComponent>();
        stateComponent = GetComponent<StateComponent>();
        animationComponent = GetComponent<AnimationComponent>();
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
            onCurrentHealthChanged.Invoke();
        }
    }


    public static void AddEnergyComponent(EntityController _entityController, float _maxEnergy)
    {
        EnergyComponent component = _entityController.gameObject.AddComponent<EnergyComponent>();
        component.maxEnergy = _maxEnergy;
        _entityController.AddComponent(component);
    }
}
