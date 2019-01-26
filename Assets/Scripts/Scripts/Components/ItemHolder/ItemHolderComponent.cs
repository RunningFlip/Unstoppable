using UnityEngine;
using System;


public class ItemHolderComponent : EntityComponent
{
    [Header("Config")]
    public ItemConfig itemConfig;


    //Entity
    [NonSerialized]
    public EntityController targetEntity;
    [NonSerialized]
    public EntityController myEntity;

    //Event
    [NonSerialized]
    public SimpleEvent executeEvent = new SimpleEvent();

 
    public override void InitializeComponent()
    {
        //Entity
        myEntity = GetComponent<EntityController>();

        //Event
        executeEvent.AddListener(delegate
        {
            if (itemConfig != null) itemConfig.Execute(targetEntity, myEntity);
        });
    }


    public override void UpdateComponent() { }
}
