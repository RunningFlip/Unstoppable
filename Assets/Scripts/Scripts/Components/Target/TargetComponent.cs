using UnityEngine;


public class TargetComponent : EntityComponent
{
    //Target
    private Transform target;

    //Components
    private CollisionComponent collisionComponent;

    //Events
    public SimpleEvent onPlayerCollision = new SimpleEvent();


    public override void InitializeComponent()
    {
        //Components
        collisionComponent = GetComponent<CollisionComponent>();

        //Events
        collisionComponent.onCollision.AddListener(delegate { CheckCollision(); });
    }


    public override void UpdateComponent() { } 


    private void CheckCollision()
    {
        if (collisionComponent.lastCollisionObject.CompareTag("Player"))
        {
            onPlayerCollision.Invoke();
        }
    }


    public static void AddTargetComponent(EntityController _entityController, Transform _target)
    {
        if (_entityController.GetComponent<TargetComponent>() != null || _entityController.gameObject == null) return;

        TargetComponent component = _entityController.gameObject.AddComponent<TargetComponent>();
        component.target = _target;
        _entityController.AddComponent(component);
    }

}
