using System.Collections.Generic;
using UnityEngine;


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

    [Header("Enemies")]
    public GameObject enemyPrefab;
    public int minSpawns;
    public int maxSpawns;

    [Header("Targets")]
    public int targets = 4;
    public List<GameObject> targetList = new List<GameObject>();

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
        SetNewTarget(true);
    }


    private void InitTargets()
    {
        PlanetComponent[] planets = FindObjectsOfType<PlanetComponent>();


    }

    private void SetNewTarget(bool _first)
    {
        if (!_first) targetList.RemoveAt(0);

        if (targetList.Count > 0)
        {
            Debug.Log("Get new target");

            PlanetComponent planet = targetList[0].GetComponent<PlanetComponent>();
            planet.onDeath.AddListener(delegate { SetNewTarget(false); });

            navigatonComponent.currentTraget = targetList[0].transform;
        }
        else
        {
            EndGame();
        }
    }


    private void EndGame()
    {
        Debug.Log("End");

        if (playerEntity != null)
        {
            mappingComponent.rbody.velocity = Vector2.zero;
            stateComponent.SetState(StateType.Everything, false);
            inputComponent.readInput = false;
        }
    }


    private void SpawnPlayer()
    {
        if (playerEntity != null) DeathSimpleComponent.AddDeathSimpleComponent(playerEntity);

        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        playerEntity = player.GetComponent<EntityController>();

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
