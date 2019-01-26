using System.Collections.Generic;
using UnityEngine;


public class AttractionComponent : EntityComponent
{
    [Header("Attraction values")]
    public CircleCollider2D circleCollider;
    public LayerMask attractionMask;


    //Attraction
    private float attraction = 0.6f;

    //Vectors
    private Vector3 pivot;

    //Collections
    public Collider2D[] collidersInOrbit;

    
    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Vectors
        pivot = circleCollider.transform.position;
    }


    public override void UpdateComponent()
    {
        collidersInOrbit = Physics2D.OverlapCircleAll(pivot, circleCollider.radius, attractionMask);

        for (int i = 0; i < collidersInOrbit.Length; i++)
        {
            AttractEntity(ref collidersInOrbit[i].GetComponent<EntityLink>().entityController);
        }
    }


    /// <summary>
    /// Attracts the entity to the pivot.
    /// </summary>
    /// <param name="_entity"></param>
    private void AttractEntity(ref EntityController _entity)
    {
        StateComponent stateComponent = _entity.GetComponent<StateComponent>();
        Rigidbody2D rbody = _entity.GetComponent<MappingComponent>().rbody;

        if (stateComponent != null && stateComponent.StateIsEnabled(StateType.ExternalGravity))
        {
            rbody.AddForce((pivot - rbody.transform.position).normalized * attraction * circleCollider.radius);
        }
    }
}
