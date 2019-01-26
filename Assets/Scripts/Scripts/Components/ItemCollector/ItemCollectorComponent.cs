
using UnityEngine;

public class ItemCollectorComponent : EntityComponent
{
    //Entity
    private EntityController entity;

    //Components
    private CollisionComponent collisionComponent;


    public override void InitializeComponent()
    {
        //Entity
        entity = GetComponent<EntityController>();

        //Components
        collisionComponent = GetComponent<CollisionComponent>();

        //Event
        collisionComponent.onCollision.AddListener(delegate { CheckCollision(); });
    }


    public override void UpdateComponent()  {}


    /// <summary>
    /// Checks if the collided object is an item and triggers the functionality.
    /// </summary>
    private void CheckCollision()
    {
        if (collisionComponent.lastCollision.CompareTag("Item"))
        {
            //TODO --> what item was found????

            ItemHolderComponent itemHolder = collisionComponent.lastCollision.GetComponent<ItemHolderComponent>();

            if (itemHolder != null)
            {
                itemHolder.targetEntity = entity;
                itemHolder.executeEvent.Invoke();
            }
        }
    }
}
