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
    public GameObject lastCollision;
    private GameObject backupCollision;

    //Last trigger object
    [NonSerialized]
    public GameObject lastTrigger;
    private GameObject backupTrigger;

    //Event
    public SimpleEvent onCollision = new SimpleEvent();
    public SimpleEvent onTrigger = new SimpleEvent();


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;
    }


    public override void UpdateComponent()
    {
        if (listenToCollisions && lastCollision != null)
        {
            if (lastCollision != backupCollision)
            {
                backupCollision = lastCollision;
                onCollision.Invoke();
            }
        }

        if (listenToTriggers && lastTrigger != null)
        {
            if (lastTrigger != backupTrigger)
            {
                backupTrigger = lastTrigger;
                onTrigger.Invoke();
            }
        }
    }

}
