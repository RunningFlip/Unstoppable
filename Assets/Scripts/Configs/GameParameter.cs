using UnityEngine;


[CreateAssetMenu(fileName = "Game Parameter Config", menuName = "Scriptable Objects/Parameter Configs/Game Parameter Config")]
public class GameParameter : ScriptableObject
{
    [Header("Player Health")]
    public float maxHealth;

    [Header("Player Movement")]
    public float moveSpeed;
    public float acceleration;
    public AnimationCurve accelerationCurve;
}
