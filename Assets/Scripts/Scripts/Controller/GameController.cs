using UnityEngine;


public class GameController : MonoBehaviour
{
    //Singelton
    public static GameController Instance;

    [Header("Parameter")]
    public GameParameter GameParameter;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
