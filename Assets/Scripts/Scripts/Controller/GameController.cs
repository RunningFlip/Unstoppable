using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    //Singelton
    public static GameController Instance;

    [Header("Parameter")]
    public GameParameter GameParameter;

    [Header("PlayerCharacter")]
    public EntityController playerEntity;

    [Header("Targets")]
    public List<GameObject> targetList = new List<GameObject>();

    //Components
    private InputComponent inputComponent;
    private MappingComponent mappingComponent;
    private NavigatonComponent navigatonComponent;
    private StateComponent stateComponent;


    private void Awake()
    {
        if (Instance == null) Instance = this;

        //Input
        inputComponent = FindObjectOfType<InputComponent>();
        inputComponent.readInput = false;      

        //Player entity
        playerEntity = inputComponent.GetComponent<EntityController>();

        //Components
        mappingComponent = playerEntity.GetComponent<MappingComponent>();
        navigatonComponent = playerEntity.GetComponent<NavigatonComponent>();
        stateComponent = playerEntity.GetComponent<StateComponent>();

        //Start
        StartGame();
    }


    private void StartGame()
    {
        Debug.Log("Start");

        inputComponent.readInput = true;
        navigatonComponent.currentTraget = targetList[0].transform;

        SetNewTarget(true);
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

        mappingComponent.rbody.velocity = Vector2.zero;
        stateComponent.SetState(StateType.Everything, false);
        inputComponent.readInput = false;
    }
}
