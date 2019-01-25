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
    public float gravityForbiddenTime;


    //Time
    private float lastTimeStamp = -float.MaxValue;

    //Components
    private EnergyComponent energyComponent;
    private StateComponent stateComponent;
    private Rigidbody2D rbody;

    //Job
    private WaitJob resetJob;



    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Components
        energyComponent = GetComponent<EnergyComponent>();
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
        stateComponent.SetState(StateType.ExternalGravity, false);

        lastTimeStamp = Time.time;
        energyComponent.currentEnergy -= requiredEnergy;

        rbody.AddForce(rbody.velocity * dashForce);

        //Job
        if (resetJob != null) resetJob.CancelJob();
        resetJob = new WaitJob(delegate 
        {
            stateComponent.SetState(StateType.ExternalGravity, true);
        }, gravityForbiddenTime);
    }
}
