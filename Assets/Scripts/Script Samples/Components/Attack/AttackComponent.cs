using UnityEngine;


/// <summary>
/// Handles the attack of the entity and triggers the attack-config.
/// </summary>
[RequireComponent(typeof(StateComponent))]
public class AttackComponent : EntityComponent
{
    [Header("Status")]
    public bool attack;
    public bool updateConfig;

    [Header("Config")]
    public AttackConfig attackConfig;


    //Entity
    private EntityController entityController;

    //Components
    private StateComponent stateComponent;
    private AnimationComponent animationComponent;


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Get entity
        entityController = GetComponent<EntityController>();
        if (entityController == null)
        {
            EntityLink link = GetComponent<EntityLink>();
            if (link != null) entityController = link.entityController;
        }

        //Components
        stateComponent = GetComponent<StateComponent>();
        animationComponent = GetComponent<AnimationComponent>();

        //Setup
        UpdateConfig();
    }


    public override void UpdateComponent()
    {
        if (!stateComponent.StateIsEnabled(StateType.Attacking)) return;

        //Change Config
        if (updateConfig)
        {
            updateConfig = false;
            UpdateConfig();
        }

        //Attack
        if (attack)
        {
            attack = false;
            InitAttack();
        }
    }


    /// <summary>
    /// Initialises te attack, triggers the config and starts the animation.
    /// </summary>
    private void InitAttack()
    {
        if (attackConfig != null)
        {           
            if (attackConfig.TryAttack())           //Tries to execute the attack.
            {
                animationComponent.attack = true;   //Animation
            }            
        }
    }


    /// <summary>
    /// Creates an instance of the config and executes the setup.
    /// </summary>
    private void UpdateConfig()
    {
        attackConfig = Instantiate(attackConfig); //Creates an instance to avoid changing the original.
        attackConfig.InitializeConfig(entityController);
    }
}
