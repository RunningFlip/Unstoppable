using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFollowComponent : EntityComponent {

    Transform trans;
    [SerializeField] GameObject followObject;
    [SerializeField] float followSpeed = 1f;

    public override void InitializeComponent()
    {
        updateType = UpdateType.FixedUpdate;
        trans = GetComponent<MappingComponent>().movementTransform;
    }

    public override void UpdateComponent()
    {
        trans.position = Vector3.Lerp(trans.position, followObject.transform.position, followSpeed);
        //trans.up = followObject.transform.up;
    }
}
