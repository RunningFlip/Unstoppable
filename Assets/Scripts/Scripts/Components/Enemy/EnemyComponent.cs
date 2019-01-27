using UnityEngine;
using System;


public class EnemyComponent : EntityComponent
{
    [Header("Track Values")]
    public bool track = true;    
    public CircleCollider2D tracker;

    [Header("Movement Values")]
    public float moveSpeed;


    //Flag
    private bool enemyDetected;

    //Floats
    private float trackingDistance;

    //Vectors
    private Vector3 distancerVec;

    //Player
    [NonSerialized]
    public Transform playerTrans;

    //Components
    private Transform trans;
    private StateComponent stateComponent;


    public override void InitializeComponent()
    {
        updateType = UpdateType.FixedUpdate;

        //Floats
        trackingDistance = tracker.radius;

        //Components
        trans = GetComponent<MappingComponent>().movementTransform;
        stateComponent = GetComponent<StateComponent>();
    }


    public override void UpdateComponent()
    {
        if (playerTrans == null)
        {
            TryToFindPlayerTrans();
            return;
        }

        if (!stateComponent.StateIsEnabled(StateType.Movement)) return;

        if (track) TriggerStalking();

        if (enemyDetected)
        {
            MoveEnemy();
            FacePlayer();
        }
    }

    private void TryToFindPlayerTrans()
    {
        if (GameController.Instance.playerEntity != null)
        {
            //Player
            playerTrans = GameController.Instance.playerEntity.GetComponent<MappingComponent>().movementTransform;
        }
    }


    /// <summary>
    /// Tries to start stalking the player.
    /// </summary>
    private void TriggerStalking()
    {
        distancerVec = (playerTrans.position - trans.position);

        if (distancerVec.magnitude <= trackingDistance)
        {
            track = false;
            enemyDetected = true;
        }
    }


    /// <summary>
    /// Moves the entity into the direction of the player.
    /// </summary>
    private void MoveEnemy()
    {
        distancerVec = (playerTrans.position - trans.position);
        trans.position += distancerVec.normalized * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Faces the enemy to the player.
    /// </summary>
    private void FacePlayer()
    {
        trans.up = (playerTrans.position - trans.position).normalized;
    }
}