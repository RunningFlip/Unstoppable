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


    //Flag
    private bool start;
    private bool backToMenu;

    //Floats
    private float passedTime = 0;


    public void Awake()
    {
        if (Instance == null) Instance = this;

        //Buttons
        startGameButton.onClick.AddListener(delegate { start = true; });
        quitGameButton.onClick.AddListener(delegate { Application.Quit(); });
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
                branchNodes[i].fillAmount = 0;
            }
        }
    }


    private void Update()
    {
        if (start)
        {
            if (passedTime >= lerpTime)
            {
                GameController.Instance.StartGame();
                start = false;
                passedTime = 0;
                menuGroup.alpha = 0;
                gameplayGroup.alpha = 1;
                menuGroup.gameObject.SetActive(false);
                return;
            }

            menuGroup.alpha = 1 - (1 / lerpTime) * passedTime;
            gameplayGroup.alpha = (1 / lerpTime) * passedTime;

            passedTime += Time.deltaTime;
        } 

        if (backToMenu)
        {
            if (passedTime >= lerpTime)
            {
                backToMenu = false;
                passedTime = 0;
                gameplayGroup.alpha = 0;
                menuGroup.alpha = 1;
                return;
            }

            gameplayGroup.alpha = 1 - (1 / lerpTime) * passedTime;
            menuGroup.alpha = (1 / lerpTime) * passedTime;

            passedTime += Time.deltaTime;
        }
    }


    public void BackToMenu()
    {
        backToMenu = true;
    }
}
