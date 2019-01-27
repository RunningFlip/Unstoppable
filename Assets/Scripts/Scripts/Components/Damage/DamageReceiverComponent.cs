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
        entityController = GetComponent<EntityController>();

        //Components
        collisionComponent = GetComponent<CollisionComponent>();

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
