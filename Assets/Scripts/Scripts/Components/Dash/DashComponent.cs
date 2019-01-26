using UnityEngine;
using System;


public class DashComponent : EntityComponent
{
    [NonSerialized]
    public bool dash;
    [NonSerialized]
    public bool freeDash;

    [Header("Dash values")]
    public float dashForce;
    public float cooldown;
    public float requiredEnergy;
    public float gravityForbiddenTime;


    //Time
    private float lastTimeStamp = -float.MaxValue;

    //Components
    private MappingComponent mappingComponent;
    private EnergyComponent energyComponent;
    private CircleComponent circleComponent;
    private StateComponent stateComponent;
    private Rigidbody2D rbody;

    //Camera
    private Camera mainCamera;

    //Job
    private WaitJob resetJob;



    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Parameters
        dashForce = GameController.Instance.GameParameter.dashForce;
        cooldown = GameController.Instance.GameParameter.dashCoolDown;
        requiredEnergy = GameController.Instance.GameParameter.dashRequiredEnergy;
        gravityForbiddenTime = GameController.Instance.GameParameter.dashGravityForbiddenTime;

        //Components
        mappingComponent = GetComponent<MappingComponent>();
        energyComponent = GetComponent<EnergyComponent>();
        circleComponent = GetComponent<CircleComponent>();
        stateComponent = GetComponent<StateComponent>();
        rbody = mappingComponent.rbody;

        //Camera
        mainCamera = Camera.main;
    }


    public override void UpdateComponent()
    {
        if (!stateComponent.StateIsEnabled(StateType.Dash)) return;

        //Dash
        if (dash)
        {
            dash = false;

            if (lastTimeStamp + cooldown < Time.time)
            {
                if (freeDash || energyComponent.currentEnergy >= requiredEnergy)
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
        if (!freeDash) energyComponent.currentEnergy -= requiredEnergy;
        else
        {
            freeDash = false;
            circleComponent.reset = true;
        }

        float mag = rbody.velocity.magnitude;
        if (mag == 0) mag = 10;
        rbody.AddForce((mainCamera.ScreenToWorldPoint(Input.mousePosition) - mappingComponent.movementTransform.position).normalized * mag * dashForce);

        //Job
        if (resetJob != null) resetJob.CancelJob();
        resetJob = new WaitJob(delegate 
        {
            stateComponent.SetState(StateType.ExternalGravity, true);
        }, gravityForbiddenTime);
    }
}
