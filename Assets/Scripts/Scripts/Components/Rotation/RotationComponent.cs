using UnityEngine;


public class RotationComponent : EntityComponent
{
    //Vectors
    private Vector3 mousePos;

    //Components
    private StateComponent stateComponent;
    private Transform movementTransform;

    //Camera
    private Camera mainCamera;


    public override void InitializeComponent()
    {
        updateType = UpdateType.FixedUpdate;

        //Components
        stateComponent = GetComponent<StateComponent>();
        movementTransform = GetComponent<MappingComponent>().movementTransform;

        //Camera
        mainCamera = Camera.main;
    }


    public override void UpdateComponent()
    {
        if (!stateComponent.StateIsEnabled(StateType.Movement)) return;

        //Mouseposition
        mousePos = Input.mousePosition;

        //Rotation
        movementTransform.up = GetMoveDirection(); //Rotation
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
