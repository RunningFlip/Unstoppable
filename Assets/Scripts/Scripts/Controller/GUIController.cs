using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIController : MonoBehaviour
{
    //Singelton
    public static GUIController Instance;

    [Header("Canvas Group")]
    public float lerpTime = 1f;
    public CanvasGroup menuGroup;
    public CanvasGroup gameplayGroup;

    [Header("Menu Buttons")]
    public Button startGameButton;
    public Button quitGameButton;

    [Header("Branch UI")]
    public List<Image> branchNodes = new List<Image>();


    //Job
    private DefaultJob lerpJob;


    public void Awake()
    {
        if (Instance == null) Instance = this;

        //Buttons
        //startGameButton.onClick.AddListener(delegate { StartGame(); });
        //quitGameButton.onClick.AddListener(delegate { Application.Quit(); });

        //StartGame(); //TODO HAS TO BE REMOVED!
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_currentEnergy"></param>
    /// <param name="_maxEnergy"></param>
    public void UpdateBranchNodes(float _currentEnergy, float _maxEnergy)
    {
        float percPerNode = _maxEnergy / branchNodes.Count;
        float backLog = 0;

        for (int i = 0; i < branchNodes.Count; i++)
        {
            backLog += percPerNode;

            if (backLog <= _currentEnergy)
            {
                branchNodes[i].fillAmount = 1;
            }
            else
            {
                float differnce = _currentEnergy - backLog - percPerNode;
                branchNodes[i].fillAmount = (differnce * 100) / percPerNode; ;
            }
        }
    }


    private void StartGame()
    {
        if (lerpJob != null && lerpJob.isActive) lerpJob.CancelJob();

        float passedTime = 0;

        lerpJob = new DefaultJob(delegate 
        {
            if (passedTime >= lerpTime)
            {
                GameController.Instance.StartGame();
                lerpJob.CancelJob();
            }

            menuGroup.alpha = 1 - (1 / lerpTime) * passedTime;
            gameplayGroup.alpha = (1 / lerpTime) * passedTime;

            passedTime += Time.deltaTime;
        });
    }
}
