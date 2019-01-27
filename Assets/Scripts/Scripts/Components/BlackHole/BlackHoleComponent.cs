using UnityEngine;


public class BlackHoleComponent : EntityComponent
{

    //Components
    private CollisionComponent collisionComponent;


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Components
        collisionComponent = GetComponent<CollisionComponent>();
    }


    public override void UpdateComponent()
    {
        if (collisionComponent.lastCollisionObject != null)
        {
            GameObject gameObj = collisionComponent.lastCollisionObject;
            if (gameObj.CompareTag("Player") || gameObj.CompareTag("Item") || gameObj.CompareTag("Enemy"))
            {
                DeathSimpleComponent.AddDeathSimpleComponent(gameObj.GetComponent<EntityController>());
            }
        }
    }
}
