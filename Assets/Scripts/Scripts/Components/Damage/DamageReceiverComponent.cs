using UnityEngine;


public class DamageReceiverComponent : EntityComponent
{
    //Entity
    private EntityController entityController;

    //Components
    private CollisionComponent collisionComponent;

    //Event
    public SimpleEvent onDeath = new SimpleEvent();


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
        collisionComponent.onCollision.AddListener(delegate { CheckCollision(); });
    }


    public override void UpdateComponent() {}


    private void CheckCollision()
    {
        EntityLink link = collisionComponent.lastCollisionObject.GetComponent<EntityLink>();

        if (link != null)
        {
            if (link.entityController.GetComponent<DamageEmitterComponent>())
            {
                onDeath.Invoke();
                DeathSimpleComponent.AddDeathSimpleComponent(entityController);
            }
        }
    }
}
