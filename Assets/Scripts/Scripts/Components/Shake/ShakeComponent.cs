using UnityEngine;
using System;


public class ShakeComponent : EntityComponent
{
    [NonSerialized]
    public bool shake;
    private bool shaking;

    [Header("Shaking Values")]
    public float shakeAmount = 0.7f; // Amplitude of the shake. A larger value shakes the camera harder.


    //Vectors
    private Vector3 originalPos;

    //Components
    private Transform trans; // Transform of the camera to shake. Grabs the gameObject's transform if null


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Components
        trans = GetComponent<MappingComponent>().movementTransform;
    }


    public override void UpdateComponent()
    {
        if (shake != shaking)
        {
            shaking = shake;

            if (shaking)
            {
                originalPos = trans.localPosition;
            }
            else
            {
                trans.localPosition = originalPos;
            }
        }

        if (shaking)
        {
            trans.localPosition = originalPos + UnityEngine.Random.insideUnitSphere * shakeAmount;
        }
    }
}
