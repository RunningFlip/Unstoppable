using UnityEngine;
using System;


/// <summary>
/// Stores informations of collisions and trigger that collided with the entity.
/// </summary>
public class CollisionComponent : EntityComponent
{
    [Header("Listeners")]
    public bool listenToCollisions = true;
    public bool listenToTriggers = true;

    //Last collision object
    [NonSerialized]
    public GameObject lastCollisionObject;
    private GameObject backupCollisionObject;
    [NonSerialized]
    public Collision2D collision;

    //Last trigger object
    [NonSerialized]
    public GameObject lastTriggerObject;
    private GameObject backupTriggerObject;
    [NonSerialized]
    public Collider2D trigger;

    //Event
    public SimpleEvent onCollision = new SimpleEvent();
    public SimpleEvent onTrigger = new SimpleEvent();


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;
    }


    public override void UpdateComponent()
    {
        if (listenToCollisions && lastCollisionObject != null)
        {
            if (lastCollisionObject != backupCollisionObject)
            {
                backupCollisionObject = lastCollisionObject;
                onCollision.Invoke();
            }
        }

        if (listenToTriggers && lastTriggerObject != null)
        {
            if (lastTriggerObject != backupTriggerObject)
            {
                backupTriggerObject = lastTriggerObject;
                onTrigger.Invoke();
            }
        }
    }

}
