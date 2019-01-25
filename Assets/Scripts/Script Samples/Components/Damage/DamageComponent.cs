using UnityEngine;


public class DamageComponent : EntityComponent
{
    [Header("Damage values")]
    public float damage;
    
    [Header("DOT values")]
    public bool isDot;          //DOT
    public int dotTicks;        //DOT
    public float dotStep;       //DOT


    //Timer
    private int ticks;
    private float dotTimer;

    //Components
    private HealthComponent healthComponent;
    private StateComponent stateComponent;


    public override void InitializeComponent()
    {
        if (isDot) updateType = UpdateType.Update; //Only for DOT

        //Components
        healthComponent = GetComponent<HealthComponent>();
        stateComponent = GetComponent<StateComponent>();

        //Check if damage is allowed
        if (stateComponent.StateIsEnabled(StateType.Damageable) && !isDot) AddDamage();
        else Destroy(this);
    }


    //Only for DOT
    public override void UpdateComponent()
    {
        if (ticks < dotTicks)           //Increases dotTimer
        {
            if (dotTimer >= dotStep)    //Executs dot
            {
                healthComponent.currentHealth -= damage;
                ticks++;
                dotTimer = 0;
            }
            dotTimer += Time.deltaTime;
        }
        else                            //DOT is over
        {
            Destroy(this);
        }
    }


    /// <summary>
    /// Adds damage to the entities healthcomponent.
    /// </summary>
    private void AddDamage()
    {
        healthComponent.currentHealth -= damage;
        Destroy(this);
    }


    /// <summary>
    /// Adds a damage-component to the entity.
    /// </summary>
    /// <param name="_entityController">Current entity</param>
    /// <param name="_damage">Damage to deal on the entity</param>
    /// <param name="_isDot">If true, the damage will be executed as a dot (damage over time)</param>
    /// <param name="_dotTicks">Ticks of the dot</param>
    /// <param name="_dotStep">Timesteps between each tick</param>
    public static void AddDamageComponent(EntityController _entityController, float _damage, bool _isDot = false, int _dotTicks = 4, float _dotStep = 2.0f)
    {
        DamageComponent component = _entityController.gameObject.AddComponent<DamageComponent>();
        component.damage = _damage;
        component.isDot = _isDot;
        component.dotTicks = _dotTicks;
        component.dotStep = _dotStep;
        _entityController.AddComponent(component);
    }


}
