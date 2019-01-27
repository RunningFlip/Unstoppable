﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameController : MonoBehaviour
{
    //Singelton
    public static GameController Instance;

    [Header("Parameter")]
    public GameParameter GameParameter;

    [Header("PlayerCharacter")]
    public Transform spawnPoint;
    public GameObject playerPrefab;
    public EntityController playerEntity;

    [Header("Camera")]
    public CameraComponent cameraComponent;

    [Header("End Video")]
    public GameObject videoPlayer;

    [Header("Enemies")]
    public GameObject enemyPrefab;
    public int minSpawns;
    public int maxSpawns;

    [Header("Targets")]
    public int targets = 4;
    public GameObject lastTarget;
    public List<GameObject> targetList = new List<GameObject>();
    private int currentTargets;

    //Components
    private InputComponent inputComponent;
    private MappingComponent mappingComponent;
    private NavigatonComponent navigatonComponent;
    private StateComponent stateComponent;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }


    public void StartGame()
    {
        Debug.Log("Start");

        cameraComponent = FindObjectOfType<CameraComponent>();

        SpawnPlayer();

        //Setup
        InitTargets();
        inputComponent.readInput = true;
        navigatonComponent.currentTraget = targetList[0].transform;

        //Start
        //SetTargets();
    }


    private void InitTargets()
    {
        currentTargets = targets - 1;

        List<PlanetComponent> planets = FindObjectsOfType<PlanetComponent>().ToList();

        for (int i = 0; i < targets; i++)
        {
            if (i < targets - 1)
            {
                int rdm = Random.Range(0, planets.Count);
                targetList.Add(planets[rdm].gameObject);

                PlanetComponent planet = planets[rdm].GetComponent<PlanetComponent>();
                planet.onDeath.AddListener(delegate 
                {
                    targetList.Remove(planet.gameObject);
                    currentTargets--;

                    if (currentTargets <= 1)
                    {
                        ActivateLastTarget();
                    }
                });

                planets.RemoveAt(rdm);
                planets = planets.ToList();
            }
            else
            {
                int rdm = Random.Range(0, planets.Count);
                lastTarget = planets[rdm].gameObject;

                lastTarget.GetComponent<PlanetComponent>().harvestable = false;

                planets.RemoveAt(rdm);
                planets = planets.ToList();
            }
        }
    }


    private void ActivateLastTarget()
    {
        TargetComponent.AddTargetComponent(lastTarget.GetComponent<EntityController>());
        lastTarget.GetComponent<PlanetComponent>().destroyable = false;

        lastTarget.GetComponent<TargetComponent>().onPlayerCollision.AddListener(delegate
        {
            DamageReceiverComponent receiverComponent = playerEntity.GetComponent<DamageReceiverComponent>();
            receiverComponent.onDeath.RemoveListener(delegate { EndGame(true); });
            EndGame(true);
        });
        navigatonComponent.currentTraget = lastTarget.transform;
    }


    public void EndGame(bool _success = false)
    {
        PlanetComponent[] planets = FindObjectsOfType<PlanetComponent>();
        for (int i = 0; i < planets.Length; i++)
        {
            planets[i].onDeath.Clear();
            planets[i].planetDeathType = PlanetDeathType.None;
            planets[i].gameObject.SetActive(false);
        }

        if (playerEntity != null)
        {
            mappingComponent.rbody.velocity = Vector2.zero;
            stateComponent.SetState(StateType.Everything, false);
            inputComponent.readInput = false;
        }
        else
        {
            DeathSimpleComponent.AddDeathSimpleComponent(playerEntity);
        }

        if (_success)
        {
            videoPlayer.SetActive(true);
            videoPlayer.GetComponent<VideoPlayer>().Play();

            new WaitJob(delegate
            {
                SceneManager.LoadScene(0);
            }, 3.5f);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }


    private void SpawnPlayer()
    {
        if (playerEntity != null) DeathSimpleComponent.AddDeathSimpleComponent(playerEntity);

        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        playerEntity = player.GetComponent<EntityController>();

        DamageReceiverComponent receiverComponent = player.GetComponent<DamageReceiverComponent>();
        receiverComponent.onDeath.AddListener(delegate { EndGame(); });

        //Input
        inputComponent = FindObjectOfType<InputComponent>();
        inputComponent.readInput = false;

        //Components
        mappingComponent = playerEntity.GetComponent<MappingComponent>();
        navigatonComponent = playerEntity.GetComponent<NavigatonComponent>();
        stateComponent = playerEntity.GetComponent<StateComponent>();

        //Camera
        cameraComponent.followedObject = player.transform;
    }
}
