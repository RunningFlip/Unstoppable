using UnityEngine;


public class ItemCollectorComponent : EntityComponent
{

    //Entity
    private EntityController entity;

    //Components
    private CollisionComponent collisionComponent;


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Entity
        entity = GetComponent<EntityController>();

        //Components
        collisionComponent = GetComponent<CollisionComponent>();
    }


    public override void UpdateComponent()
    {
        if (collisionComponent.lastTrigger != null && collisionComponent.lastTrigger.CompareTag("Item"))
        {
            ItemHolderComponent itemHolder = collisionComponent.lastTrigger.GetComponent<EntityLink>().entityController.GetComponent<ItemHolderComponent>();

            if (itemHolder != null)
            {
                itemHolder.targetEntity = entity;
                itemHolder.executeEvent.Invoke();
            }
        }
    }
}
