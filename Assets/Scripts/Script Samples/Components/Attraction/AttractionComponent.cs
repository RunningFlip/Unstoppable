﻿using System.Collections.Generic;
using UnityEngine;


public class AttractionComponent : EntityComponent
{
    [Header("Attraction Values")]
    public float attraction;

    [Header("Component")]
    public CircleCollider2D circleCollider;


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
        hittedColliders = Physics2D.OverlapCircleAll(pivot, circleCollider.radius * 60);

        if (hittedColliders.Length > 0)
        {
            for (int i = 0; i < hittedColliders.Length; i++)
            {
                Collider2D col = hittedColliders[i];
                if (hittedColliders[i].CompareTag("Selectable"))
                {
                    Rigidbody2D rbody = col.GetComponent<EntityLink>().entityController.GetComponent<MappingComponent>().rbody;
                    rbody.AddForce((pivot - rbody.transform.position).normalized * attraction);
                }
            }
        }
    }
}
