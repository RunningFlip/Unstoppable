using UnityEngine;
using System;


/// <summary>
/// Handles the health and death of the entity.
/// </summary>
[RequireComponent(typeof(MappingComponent), typeof(StateComponent))]
public class HealthComponent : EntityComponent
{
    [Header("Health")]
    public float maxHealth;
    private float lastMaxHealth;
    public float currentHealth;
    private float lastHealth;

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
        maxHealth = GameController.Instance.GameParameter.maxHealth;
        currentHealth = maxHealth;
        lastMaxHealth = maxHealth;
        lastHealth = currentHealth;

        //Components
        mappingComponent = GetComponent<MappingComponent>();
        stateComponent = GetComponent<StateComponent>();
        animationComponent = GetComponent<AnimationComponent>();
    }


    public override void UpdateComponent()
    {
        if (!stateComponent.StateIsEnabled(StateType.Damageable)) return;

        //Max health
        if (maxHealth != lastMaxHealth)
        {
            lastMaxHealth = maxHealth;
            onMaxHealthChanged.Invoke();
        }

        //Current health
        if (currentHealth != lastHealth)
        {
            lastHealth = currentHealth;
            onCurrentHealthChanged.Invoke();

            if (currentHealth <= 0)
            {
                isDead = true;
                onDeath.Invoke();
                OnDeath();
            }
        }
    }


    private void OnDeath()
    {
        //Set statecomponent
        stateComponent.SetState(StateType.Everything, false);

        //Disables colliders
        for (int i = 0; i < mappingComponent.colliders.Length; i++)
        {
            mappingComponent.colliders[i].isTrigger = true;
        }

        //Audio
        if (deathAudioClip != null)
        {
            AudioSource.PlayClipAtPoint(deathAudioClip, mappingComponent.movementTransform.position, volume);
        }

        //Animation
        animationComponent.death = true;

        //Adds a DeathSimpleComponent to the entity
        DeathSimpleComponent.AddDeathSimpleComponent(GetComponent<EntityController>(), DeathType.Disable);
    }


    public static void AddHealthComponent(EntityController _entityController, float _maxHealth)
    {
        HealthComponent component = _entityController.gameObject.AddComponent<HealthComponent>();
        component.maxHealth = _maxHealth;
        _entityController.AddComponent(component);
    }
}
