using UnityEngine;


[CreateAssetMenu(fileName = "Default Attack Config", menuName = "Scriptable Objects/Attack Configs/Default Attack Config")]
public class DefaultAttackConfig : AttackConfig
{

    protected override void SetupConfig()
    {
        Debug.Log("Setup");                     //Setup  
    }


    protected override void Attack()
    {
        Debug.Log("AttackConfig triggered!");   //Space for the attack functionality

        PlayAudio();                            //Plays audio

        lastAttack = Time.time;                 //Sets a timestamp for the cooldown check

        inAttack = false;                       //Allows config to use attack again
    }
}
