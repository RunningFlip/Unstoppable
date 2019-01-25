using UnityEngine;


[CreateAssetMenu(fileName = "Game Parameter Config", menuName = "Scriptable Objects/Parameter Configs/Game Parameter Config")]
public class GameParameter : ScriptableObject
{
    [Header("Player Energy")]
    public float maxEnergy;

    [Header("Player Movement")]
    public float forceMultiplier;
    public float forceMagnitudeCap;
    public float linearDrag;
    public float angularDrag;
}
