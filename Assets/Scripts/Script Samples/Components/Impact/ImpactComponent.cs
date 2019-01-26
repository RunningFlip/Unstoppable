using UnityEngine;
using System;


public class ImpactComponent : EntityComponent
{
    [Header("Impact Values")]
    public float minHardBreach;

    //Flag
    //[NonSerialized]
    public bool hardBreachEnabled;

    //Component
    private CollisionComponent collisionComponent;
    private Rigidbody2D rbody;


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Parameters
        minHardBreach = GameController.Instance.GameParameter.minHardBreach;

        //Components
        collisionComponent = GetComponent<CollisionComponent>();
        rbody = GetComponent<MappingComponent>().rbody;

        //Event
        collisionComponent.onCollision.AddListener(delegate 
        {
            if (hardBreachEnabled) PlanetImpact();
        });
    }


    public override void UpdateComponent()
    {
        if (rbody.velocity.magnitude >= minHardBreach)
        {
            hardBreachEnabled = true;
        }
        else
        {
            hardBreachEnabled = false;
        }
    }


    private void PlanetImpact()
    {
        if (collisionComponent.lastCollision.CompareTag("Planet"))
        {
            DeathSimpleComponent.AddDeathSimpleComponent(collisionComponent.lastCollision.GetComponent<EntityController>());
        }
    }

}
