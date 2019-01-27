using UnityEngine;
using System;


[Serializable]
public abstract class ItemConfig : ScriptableObject
{
    [Header("Audio")]
    public AudioClip audioClip;

    public virtual void Execute(EntityController _entityController, EntityController _myEntity)
    {
        AudioSource.PlayClipAtPoint(audioClip, _myEntity.transform.position);
        DeathSimpleComponent.AddDeathSimpleComponent(_myEntity, DeathType.Destroy);
    }
}
