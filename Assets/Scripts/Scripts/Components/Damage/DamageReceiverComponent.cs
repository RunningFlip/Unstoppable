using UnityEngine;


public class DamageReceiverComponent : EntityComponent
{
    //Entity
    private EntityController entityController;

    //Components
    private CollisionComponent collisionComponent;


    public override void InitializeComponent()
    {
        //Entity
        EntityLink link = GetComponent<EntityLink>();
        if (link == null)
        {
            entityController = GetComponent<EntityController>();
        }
        else
        {
            entityController = link.entityController;
        }

        //Components
        collisionComponent = entityController.GetComponent<CollisionComponent>();

        //Event
        collisionComponent.onCollision.AddListener(CheckCollision);
    }


    public override void UpdateComponent() {}


    private void CheckCollision()
    {
        if (collisionComponent.lastCollisionObject.GetComponent<DamageEmitterComponent>())
        {
            DeathSimpleComponent.AddDeathSimpleComponent(entityController);
        }
    }
}
