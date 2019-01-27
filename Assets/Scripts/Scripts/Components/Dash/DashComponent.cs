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
    public int requiredEnergy;
    public float gravityForbiddenTime;
    public float movementForbiddenTime;

    [Header("Particle")]
    public ParticleSystem particleSystem;

    [Header("Audio")]
    public AudioClip dashClip;


    //Flag
    private bool inGravityReset;
    private bool inMovementReset;

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


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Parameters
        dashForce = GameController.Instance.GameParameter.dashForce;
        cooldown = GameController.Instance.GameParameter.dashCoolDown;
        requiredEnergy = GameController.Instance.GameParameter.dashRequiredEnergy;
        gravityForbiddenTime = GameController.Instance.GameParameter.dashGravityForbiddenTime;
        movementForbiddenTime = GameController.Instance.GameParameter.dashMovementForbiddenTime;

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

        if (inGravityReset)
        {
            if (lastTimeStamp + gravityForbiddenTime < Time.time)
            {
                inGravityReset = false;
                stateComponent.SetState(StateType.ExternalGravity, true);
            }
        }

        if (inMovementReset)
        {
            if (lastTimeStamp + movementForbiddenTime < Time.time)
            {
                inMovementReset = false;
                stateComponent.SetState(StateType.Movement, true);
            }
        }
    }



    /// <summary>
    /// Adds a single force impact to the rigidbody.
    /// </summary>
    private void Dash()
    {
        if (inGravityReset)
        {
            inGravityReset = false;
            stateComponent.SetState(StateType.ExternalGravity, true);
        }

        if (inMovementReset)
        {
            inMovementReset = false;
            stateComponent.SetState(StateType.Movement, true);
        }

        //State
        inGravityReset = true;
        inMovementReset = true;
        stateComponent.SetState(StateType.ExternalGravity, false);
        stateComponent.SetState(StateType.Movement, false);

        //Audio
        AudioSource.PlayClipAtPoint(dashClip, mappingComponent.movementTransform.position);

        //Free dash
        lastTimeStamp = Time.time;
        if (!freeDash) energyComponent.currentEnergy -= requiredEnergy;
        else
        {
            freeDash = false;
            circleComponent.reset = true;
        }

        //Particle System
        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
        }

        //Force
        float mag = rbody.velocity.magnitude;
        if (mag == 0) mag = 10;
        rbody.AddForce((mainCamera.ScreenToWorldPoint(Input.mousePosition) - mappingComponent.movementTransform.position).normalized * mag * dashForce);
    }
}
