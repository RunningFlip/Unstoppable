using System.Collections.Generic;
using UnityEngine;


public class NavigatonComponent : EntityComponent
{
    [Header("Navigiation Value")]
    public List<GameObject> targets = new List<GameObject>();
    public GameObject nearestTarget;
    [SerializeField] const float maxLightIntensity = 5;

    [Header("Components")]
    [SerializeField] Light playerLight;


    //Floats
    public float lightIntensity;

    private float currentDistance;
    private float startDistance;
    private const float goalDistance = 2f;

    //Components
    private MappingComponent mappingComponent;


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Components
        mappingComponent = GetComponent<MappingComponent>();
    }

    public override void UpdateComponent()
    {
        GetNearestTarget();

        //Update distance
        if (nearestTarget != null)
        {
            currentDistance = (nearestTarget.transform.position - mappingComponent.movementTransform.position).magnitude;
            currentDistance -= goalDistance;

            //Light intensity
            float mag = (nearestTarget.transform.position - mappingComponent.movementTransform.position).magnitude;

            lightIntensity = mag / 100;
            lightIntensity = Mathf.Clamp(lightIntensity, 0f, maxLightIntensity);
            lightIntensity = maxLightIntensity - lightIntensity;
            playerLight.intensity = lightIntensity;
        }
    }


    private void GetNearestTarget()
    {
        float nearest = float.MaxValue;

        for (int i = 0; i < targets.Count; i++)
        {
            if (nearest > (targets[i].transform.position - mappingComponent.movementTransform.position).magnitude)
            {
                nearest = (targets[i].transform.position - mappingComponent.movementTransform.position).magnitude;
                nearestTarget = targets[i];
            }
        }
    }

}
