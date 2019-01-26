using UnityEngine;
using System;


/// <summary>
/// Handles the movement of the entity.
/// </summary>
[RequireComponent(typeof(MappingComponent), typeof(StateComponent))]
public class MovementComponent : EntityComponent
{
    [Header("Impact values")]
    public Vector2 currentDirection;
    public float forceMultiplier;


    //Floats
    private float forceMagnitudeCap;
    private float currentMagnitudeCap; 

    //Vectors
    private Vector3 mousePos;

    //Components
    private StateComponent stateComponent;
    private Rigidbody2D rbody;
    private Transform movementTransform;

    //Camera
    private Camera mainCamera;


    public override void InitializeComponent()
    {
        updateType = UpdateType.FixedUpdate;

        //Components
        stateComponent = GetComponent<StateComponent>();

        MappingComponent mapping = GetComponent<MappingComponent>();
        rbody = mapping.rbody;
        movementTransform = mapping.movementTransform;

        //Parameters
        forceMultiplier = GameController.Instance.GameParameter.forceMultiplier;
        forceMagnitudeCap = GameController.Instance.GameParameter.forceMagnitudeCap;
        rbody.drag = GameController.Instance.GameParameter.linearDrag;
        rbody.angularDrag = GameController.Instance.GameParameter.angularDrag;
        currentMagnitudeCap = forceMagnitudeCap;

        //Camera
        mainCamera = Camera.main;
    }


    public override void UpdateComponent()
    {
        if (!stateComponent.StateIsEnabled(StateType.Movement)) return;

        //Mouseposition
        mousePos = Input.mousePosition;

        //Get current direction
        currentDirection = GetMoveDirection();

        //Force
        if (rbody.velocity.magnitude < currentMagnitudeCap)
        {
            rbody.AddForce(currentDirection * forceMultiplier);
            movementTransform.up = currentDirection; //Rotation
        }
    }


    /// <summary>
    /// Returns the direction from the playerobject to the mousecursor.
    /// </summary>
    /// <returns></returns>
    private Vector2 GetMoveDirection()
    {
        return (mousePos - mainCamera.WorldToScreenPoint(movementTransform.position)).normalized;
    }

}
