using UnityEngine;
using System;


/// <summary>
/// Config for a defined attack-pattern.
/// </summary>
[Serializable]
public abstract class AttackConfig : ScriptableObject
{
    [Header("Attack values")]
    public float damage;
    public float cooldown;

    [Header("Audio")]
    public float volume;
    public AudioClip audioClip;


    //Flag
    protected bool inAttack;

    //Time
    protected float lastAttack = -float.MaxValue;

    //Entity reference
    protected EntityController entityController;

    //Components
    protected MappingComponent mappingComponent;


    /// <summary>
    /// Initializes the config and sets the entityController.
    /// </summary>
    /// <param name="_entityController"></param>
    public void InitializeConfig(EntityController _entityController)
    {
        //Entity reference
        entityController = _entityController;

        //Components
        mappingComponent = entityController.GetComponent<MappingComponent>();

        //Seupt
        SetupConfig();
    }


    /// <summary>
    /// Tries to executes the attack if the cooldown is ready, returns true if ready.
    /// </summary>
    /// <returns>Returns true if the attack is is cooldown is ready.</returns>
    public bool TryAttack()
    {
        if (!inAttack && lastAttack + cooldown <= Time.time)
        {
            inAttack = true;
            Attack();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Setups the config, defines variables and inits functions.
    /// </summary>
    protected abstract void SetupConfig();


    /// <summary>
    /// Executes only once when the attack is called and calls its functionality.
    /// </summary>
    protected abstract void Attack();


    /// <summary>
    /// Plays the audio of the attack config on the current position of the movement-transform.
    /// </summary>
    protected void PlayAudio()
    {
        if (audioClip == null) return;
        AudioSource.PlayClipAtPoint(audioClip, mappingComponent.movementTransform.position, volume);
    }
}
