using UnityEngine;


public class ShieldComponent : EntityComponent
{
    //Components
    private CollisionComponent collisionComponent;


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Components
        collisionComponent = GetComponent<CollisionComponent>();

        //Event
        collisionComponent.onCollision.AddListener(delegate { CheckCollision(); });
    }


    public override void UpdateComponent()
    {
        //TODO
    }


    /// <summary>
    /// Checks if the entity collides with an planet and decreases the energy value.
    /// </summary>
    private void CheckCollision()
    {
        if (collisionComponent.lastCollisionObject.CompareTag("Planet"))
        {
            PlanetComponent planet = collisionComponent.lastCollisionObject.GetComponent<EntityLink>().entityController.GetComponent<PlanetComponent>();
            if (planet.dangerous)
            {
                DeathSimpleComponent.AddDeathSimpleComponent(GetComponent<EntityController>());
            }
        }
    }
}
