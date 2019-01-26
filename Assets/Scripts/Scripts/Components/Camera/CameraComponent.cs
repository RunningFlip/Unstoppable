using UnityEngine;


/// <summary>
/// Handles the movement of the camera and follows a specific target.
/// </summary>
public class CameraComponent : EntityComponent
{
    [Header("Camera values")]
    public bool follow = true;
    public Transform followedObject;


    //Vector
    private Vector3 moveVector;

    //Components
    private Transform trans;


    public override void InitializeComponent()
    {
        updateType = UpdateType.FixedUpdate;

        //Components
        trans = transform;

        //Vector
        moveVector = trans.position;
    }


    public override void UpdateComponent()
    {
        if (!follow) return;

        moveVector.x = followedObject.position.x;
        moveVector.y = followedObject.position.y;
        //z will be ignored (2D)

        trans.position = moveVector;
    }
}
