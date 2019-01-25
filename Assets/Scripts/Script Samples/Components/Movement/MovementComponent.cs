using UnityEngine;
using System;


/// <summary>
/// Handles the movement of the entity.
/// </summary>
[RequireComponent(typeof(MappingComponent), typeof(StateComponent))]
public class MovementComponent : EntityComponent
{
    [Header("Movement values")]
    public float moveSpeed;
    public float acceleration;
    public AnimationCurve accelerationCurve;


    //Movement applier
    [NonSerialized]
    public bool move;
    [NonSerialized]
    public Vector3 movement;

    //Acceleation
    private float passedAcceleration;
    private float currentMoveSpeed;

    //Flag
    private bool lastMove;

    //Vectors
    private Vector3 moveVec = new Vector3(1, 1, 0);

    //Components
    private Transform movementTransform;
    private StateComponent stateComponent;
    private AnimationComponent animationComponent;


    public override void InitializeComponent()
    {
        updateType = UpdateType.FixedUpdate;

        //Values
        moveSpeed = GameController.Instance.GameParameter.moveSpeed;
        acceleration = GameController.Instance.GameParameter.acceleration;
        accelerationCurve = GameController.Instance.GameParameter.accelerationCurve;

        //Components
        movementTransform = GetComponent<MappingComponent>().movementTransform;
        stateComponent = GetComponent<StateComponent>();
        animationComponent = GetComponent<AnimationComponent>();
    }


    public override void UpdateComponent()
    {
        if (!stateComponent.StateIsEnabled(StateType.Movement)) return;

        //Animation control
        if (lastMove != move)
        {
            lastMove = move;
            animationComponent.move = move;

            passedAcceleration = 0;     //Resets acceleration
            currentMoveSpeed = 0;       //Resets movespeed
        }

        //Movement
        if (move)
        {
            Move();
        }
    }


    /// <summary>
    /// Moves the movement-transform of the entity.
    /// </summary>
    private void Move()
    {
        movement = movement.normalized;         //Normalizes the movement vector

        if (passedAcceleration < acceleration)  //Movement with acceleration
        {
            currentMoveSpeed = accelerationCurve.Evaluate(passedAcceleration / acceleration) * moveSpeed;  //Calculation of the acceleration
            passedAcceleration += Time.fixedDeltaTime;

            movementTransform.position += movement * (currentMoveSpeed * Time.fixedDeltaTime);
        }
        else                                    //Movement without acceleration
        {
            movementTransform.position += movement * (moveSpeed * Time.fixedDeltaTime);
        }
    }
}
