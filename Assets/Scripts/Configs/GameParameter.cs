using UnityEngine;


[CreateAssetMenu(fileName = "Game Parameter Config", menuName = "Scriptable Objects/Parameter Configs/Game Parameter Config")]
public class GameParameter : ScriptableObject
{
    [Header("Player Energy")]
    public int maxEnergy;

    [Header("Player Movement")]
    public float forceMultiplier;
    public float forceMagnitudeCap;
    public float linearDrag;
    public float angularDrag;

    [Header("Player Circle")]
    public float circleRotateSpeed;

    [Header("Player Harvest")]
    public float harvestInterval;

    [Header("Player Dash")]
    public float dashForce;
    public float dashCoolDown;
    public int dashRequiredEnergy;
    public float dashGravityForbiddenTime;
    public float dashMovementForbiddenTime;

    [Header("Player Impacat")]
    public float minHardBreach;
    public int energyMalus;
    public float spawnMagnitude;

    [Header("Death Type Prefabs")]
    public GameObject deathParticlePrefab;
    public GameObject[] deathStarPrefabs;
    public GameObject blackHolePrefab;
}
