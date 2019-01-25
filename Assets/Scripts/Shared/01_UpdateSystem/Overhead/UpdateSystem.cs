using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The core-system of the entity-update-framework.
/// It handles all IUpdateables -> the EntityController and the JobSystem.
/// </summary>
public class UpdateSystem : MonoBehaviour
{
    //Singelton
    private static UpdateSystem _instance;
    public static UpdateSystem instance
    {
        get
        {
            return _instance;
        }
    }

    //Updateable object hashSet
    private static List<IUpdateable> updateList = new List<IUpdateable>();


    /// <summary>
    /// Awake method that initializes the manager singelton.
    /// </summary>
    private void Awake()
    {
        if (instance != null) Destroy(this);
        else _instance = this;

        DontDestroyOnLoad(this);
    }


    /// <summary>
    /// Registers the given object to the updateTable.
    /// </summary>
    /// <param name="_object"></param>
    public static void Register(IUpdateable _object)
    {
        if (_object == null) throw new System.ArgumentNullException();

        updateList.Add(_object);
    }


    /// <summary>
    /// Unregisters the given object from the updateTable.
    /// </summary>
    /// <param name="_object"></param>
    public static void Unregister(IUpdateable _object)
    {
        if (_object == null) throw new System.ArgumentNullException();

        updateList.Remove(_object);
    }



    //------------------Update chunk---START-------------------
    private void Update()
    {
        for (int i = 0; i < updateList.Count; i++)
        {
            updateList[i].Tick();
        }
        //Updates the jobs. (Update)
        JobSystem.Tick(); 
    }
    //------------------Update chunk---END---------------------


    //------------------Fixed Update chunk---START-------------
    private void FixedUpdate()
    {
        for (int i = 0; i < updateList.Count; i++)
        {
            updateList[i].FixedTick();
        }
        //Updates the jobs. (FixedUpdate)
        JobSystem.FixedTick();
    }
    //------------------Fixed Update chunk---END---------------
}
