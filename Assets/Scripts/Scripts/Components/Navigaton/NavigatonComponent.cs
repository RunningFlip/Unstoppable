using UnityEngine;


public class NavigatonComponent : EntityComponent
{
    [Header("Navigiation Value")]
    public Transform currentTraget;
    private Transform lastTraget;


    //Floats
    public float lightIntensity;
    private const float maxLightIntensity = 3f;

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
        //New target
        if (currentTraget != lastTraget)
        {
            startDistance = (currentTraget.position - mappingComponent.movementTransform.position).magnitude;
        }

        //Update distance
        if (currentTraget != null)
        {
            currentDistance = (currentTraget.position - mappingComponent.movementTransform.position).magnitude;
            currentDistance -= goalDistance;

            //Light intensity
            lightIntensity = maxLightIntensity / (goalDistance / currentDistance);
            lightIntensity = Mathf.Clamp(lightIntensity, 0f, maxLightIntensity);
        }
    }

}
