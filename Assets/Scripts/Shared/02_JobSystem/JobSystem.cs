using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// The system that handles all job actions.
/// </summary>
public class JobSystem : MonoBehaviour
{
    //Singelton
    private static JobSystem _instance;
    public static JobSystem instance
    {
        get
        {
            return _instance;
        }
    }

    //Updateable object hashSet   
    private static List<Job> updateJobList = new List<Job>();
    private static List<Job> fixedUpdateJobList = new List<Job>();
    private static List<Job> jobsToAdd = new List<Job>();



    /// <summary>
    /// Awake method that initializes the manager singelton.
    /// </summary>
    private void Awake()
    {
        if (instance != null) Destroy(this);
        else _instance = this;

        //Callback for scene changes
        SceneManager.activeSceneChanged += CheckDestroyOnLoadJobs;
    }


    /// <summary>
    /// Will be exeuted every frame.
    /// </summary>
    public static void Tick()
    {
        CheckNewJobs(); //Checks if there are new jobs to add.

        for (int i = 0; i < updateJobList.Count; i++)
        {
            updateJobList[i].UpdateJob();
        }
    }


    /// <summary>
    /// Will be exeuted every frame.
    /// </summary>
    public static void FixedTick()
    {
        for (int i = 0; i < fixedUpdateJobList.Count; i++)
        {
            fixedUpdateJobList[i].UpdateJob();
        }
    }


    /// <summary>
    /// Registers the given object to the updateTable.
    /// </summary>
    /// <param name="_job"></param>
    public static void RegisterJob(Job _job)
    {
        if (_job == null) throw new System.ArgumentNullException();
        jobsToAdd.Add(_job);
    }


    /// <summary>
    /// Unregisters the given object from the updateTable.
    /// </summary>
    /// <param name="_job"></param>
    public static void UnregisterJob(Job _job)
    {
        if (_job == null) throw new System.ArgumentNullException();
        if (_job.jobUpdateType == JobUpdateType.Update)
        {
            updateJobList.Remove(_job);
        }
        else if (_job.jobUpdateType == JobUpdateType.FixedUpdate)
        {
            fixedUpdateJobList.Remove(_job);
        }
    }


    /// <summary>
    /// Adds the jobs from the "jobsToAdd"-List to the "jobList", where they will be executed.
    /// </summary>
    private static void CheckNewJobs()
    {
        if (jobsToAdd.Count > 0)
        {
            var item = jobsToAdd.GetEnumerator(); //avoid gc by calling GetEnumerator and iterating manually
            while (item.MoveNext())
            {
                switch (item.Current.jobUpdateType)
                {
                    case JobUpdateType.Update:
                        updateJobList.Add(item.Current);
                        break;
                    case JobUpdateType.FixedUpdate:
                        fixedUpdateJobList.Add(item.Current);
                        break;
                }
            }
            jobsToAdd.Clear();
        }
    }


    /// <summary>
    /// Cancels every job that is tagged with "destroyOnLoad".
    /// </summary>
    /// <param name="_arg0"></param>
    /// <param name="_arg1"></param>
    private void CheckDestroyOnLoadJobs(Scene _arg0, Scene _arg1) {
        for (int i = 0; i < updateJobList.Count; i++)                       //Update
        {
            if (updateJobList[i].destroyOnLoad) updateJobList[i].CancelJob();
        }

        for (int i = 0; i < fixedUpdateJobList.Count; i++)                  //FixedUpdate
        {
            if (fixedUpdateJobList[i].destroyOnLoad) fixedUpdateJobList[i].CancelJob();
        }
    }
}
