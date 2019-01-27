using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIController : MonoBehaviour
{
    //Singelton
    public static GUIController Instance;


    [Header("Branch UI")]
    public List<Image> branchNodes = new List<Image>();


    public void Start()
    {
        if (Instance == null) Instance = this;
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
                branchNodes[i].fillAmount = (differnce * 100) / percPerNode;
                break;
            }
        }
    }
}
