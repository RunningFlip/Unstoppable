using UnityEngine;
using System;


public class DashComponent : EntityComponent
{
    [NonSerialized]
    public bool dash;

    [Header("Dash values")]
    public float dashForce;
    public float cooldown;
    public float requiredEnergy;


    //Time
    private float lastTimeStamp = -float.MaxValue;

    //Components
    private EnergyComponent energyComponent;
    private MovementComponent movementComponent;
    private StateComponent stateComponent;
    private Rigidbody2D rbody;



    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Components
        energyComponent = GetComponent<EnergyComponent>();
        movementComponent = GetComponent<MovementComponent>();
        stateComponent = GetComponent<StateComponent>();
        rbody = GetComponent<MappingComponent>().rbody;
    }


    public override void UpdateComponent()
    {
        if (!stateComponent.StateIsEnabled(StateType.Movement)) return;

        //Dash
        if (dash)
        {
            dash = false;

            if (lastTimeStamp + cooldown < Time.time)
            {
                if (energyComponent.currentEnergy >= requiredEnergy)
                {
                    Dash();
                }
            }
        }
    }



    /// <summary>
    /// Adds a single force impact to the rigidbody.
    /// </summary>
    private void Dash()
    {
        lastTimeStamp = Time.time;
        energyComponent.currentEnergy -= requiredEnergy;

        rbody.AddForce(rbody.velocity * dashForce);
    }
}
