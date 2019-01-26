using UnityEngine;
using System;


[Serializable]
public abstract class ItemConfig : ScriptableObject
{
    public virtual void Execute(EntityController _entityController, EntityController _myEntity)
    {
        DeathSimpleComponent.AddDeathSimpleComponent(_myEntity, DeathType.Destroy);
    }
}
