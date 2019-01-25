using System.Collections.Generic;
using UnityEngine;


public class AttractionComponent : EntityComponent
{
    [Header("Component")]
    public CircleCollider2D circleCollider;


    //Attraction
    private float attraction = 0.6f;

    //Vectors
    private Vector3 pivot;

    //Collections
    public Collider2D[] hittedColliders;

    
    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Vectors
        pivot = circleCollider.transform.position;
    }

    public override void UpdateComponent()
    {
        hittedColliders = Physics2D.OverlapCircleAll(pivot, circleCollider.radius);

        if (hittedColliders.Length > 0)
        {
            for (int i = 0; i < hittedColliders.Length; i++)
            {
                Collider2D col = hittedColliders[i];
                if (hittedColliders[i].CompareTag("Selectable"))
                {
                    EntityController entity = col.GetComponent<EntityLink>().entityController;
                    StateComponent stateComponent = entity.GetComponent<StateComponent>();

                    Rigidbody2D rbody = entity.GetComponent<MappingComponent>().rbody;
                    
                    if (stateComponent != null && stateComponent.StateIsEnabled(StateType.ExternalGravity)) 
                    {
                        rbody.AddForce((pivot - rbody.transform.position).normalized * attraction * circleCollider.radius);
                    } 
                }
            }
        }
    }
}
