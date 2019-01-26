using System;
using UnityEngine;


public class HarvestComponent : EntityComponent
{
    [NonSerialized]
    public bool inHarvest;

    [Header("Harvest")]
    public float harvestInterval;

    //Flag
    private bool lastStatus;

    //Time
    private float passedTime = 0;

    //Components
    private EnergyComponent energyComponent;
    private CircleComponent circleComponent;

    //Other components
    private PlanetComponent planetComponent;
    

    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Parameter
        harvestInterval = GameController.Instance.GameParameter.harvestInterval;

        //Components
        energyComponent = GetComponent<EnergyComponent>();
        circleComponent = GetComponent<CircleComponent>();
    }


    public override void UpdateComponent()
    {
        if (inHarvest != lastStatus)
        {
            lastStatus = inHarvest;

            if (inHarvest)
            {
                planetComponent = circleComponent.currentPlanet;
                planetComponent.onDeath.AddListener(delegate { ResetVariables(); });
                passedTime = 0;
            }
            else
            {
                ResetVariables();
                planetComponent = null;
            }
        }

        if (inHarvest)
        {
            if (passedTime >= harvestInterval)
            {
                passedTime = 0;
                HarvestStep();
            }     
            passedTime += Time.deltaTime;
        }
    }


    private void HarvestStep()
    {
        if (planetComponent.energyStorage > 0)
        {
            planetComponent.energyStorage -= 1;
            energyComponent.currentEnergy += 1;

            if (planetComponent.planetCollider.transform.localScale.x > 0)
            {
                float step =  1f / (float)planetComponent.energyStorageStart;
                planetComponent.planetCollider.transform.localScale -= new Vector3(step, step, step);
            }
        }
        else
        {
            inHarvest = false;
            KillPlanet();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void KillPlanet()
    {
        energyComponent.maxEnergy += planetComponent.energyBonus;
        energyComponent.currentEnergy = energyComponent.maxEnergy;

        ResetVariables();

        DeathSimpleComponent.AddDeathSimpleComponent(planetComponent.GetComponent<EntityController>());
        planetComponent = null;
    }


    /// <summary>
    /// 
    /// </summary>
    private void ResetVariables()
    {
        if (planetComponent != null)
        {
            planetComponent.onDeath.RemoveListener(delegate { ResetVariables(); });
        }
        
        passedTime = 0;
        inHarvest = false;
    }

}
