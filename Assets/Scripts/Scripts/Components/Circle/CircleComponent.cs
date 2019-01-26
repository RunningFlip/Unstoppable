using UnityEngine;
using System;


public class CircleComponent : EntityComponent
{
    [NonSerialized]
    public bool tryCircle;
    [NonSerialized]
    public bool reset;

    [Header("Circle Values")]
    public float rotateSpeed = 5f;
    public LayerMask planetLayer;
    

    //Flag
    private bool inCircle;

    //Vectors
    private Vector3 pivot;

    //Components
    private MappingComponent mappingComponent;
    private StateComponent stateComponent;
    private DashComponent dashComponent;
    private Transform trans;
    private Rigidbody2D rbody;

    //Other components
    private PlanetComponent currentPlanet;

    //Camera
    private Camera mainCamera;


    public override void InitializeComponent()
    {
        updateType = UpdateType.FixedUpdate;

        //Parameters
        rotateSpeed = GameController.Instance.GameParameter.circleRotateSpeed;

        //Components
        mappingComponent = GetComponent<MappingComponent>();
        stateComponent = GetComponent<StateComponent>();
        dashComponent = GetComponent<DashComponent>();
        trans = mappingComponent.movementTransform;
        rbody = mappingComponent.rbody;

        //Camera
        mainCamera = Camera.main;
    }


    public override void UpdateComponent()
    {
        //Reset
        if (reset)
        {
            reset = false;
            ResetParameter();
        }

        //Init
        if (tryCircle)
        {
            tryCircle = false;         
            TryToCircle();
        }

        //Circle
        if (inCircle)
        {
            CircleAround();
        }
    }


    /// <summary>
    /// Initializes the values radius and pivot, if the selected object is valid.
    /// </summary>
    private void TryToCircle()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1f, planetLayer);

        if (hit.collider != null && hit.collider.CompareTag("Planet"))
        {
            MappingComponent mappings = hit.collider.GetComponent<EntityLink>().entityController.GetComponent<MappingComponent>();

            float rad = mappings.GetComponent<PlanetComponent>().orbitCollider.radius;
            
            if (DistanceIsValid(trans.position, mappings.movementTransform.position, rad))
            {
                inCircle = true;
                currentPlanet = mappings.GetComponent<PlanetComponent>();;

                currentPlanet.onDeath.AddListener(ResetParameter);
                SetVariables(ref mappings);
            }
            else
            {
                inCircle = false;
            }
        }
    }


    /// <summary>
    /// Sets the variables which are needed to circle arounnd the planet.
    /// </summary>
    private void SetVariables(ref MappingComponent _mappings)
    {
        //State
        stateComponent.SetState(StateType.ExternalGravity, false);
        stateComponent.SetState(StateType.Movement, false);

        //Flag -> dash for free
        dashComponent.freeDash = true;

        //Parameters
        pivot = _mappings.movementTransform.position;
    }


    /// <summary>
    /// Returns true if the player is in a legit distance to the orbit.
    /// </summary>
    /// <returns></returns>
    private bool DistanceIsValid(Vector2 _myPos, Vector2 _planetPos, float _distance)
    {
        return (_myPos - _planetPos).magnitude <= _distance;
    }


    /// <summary>
    /// Returns a rotation vector around a center.
    /// </summary>
    /// <param name="_point"></param>
    /// <param name="_pivot"></param>
    /// <param name="_angle"></param>
    /// <returns></returns>
    private Vector3 RotatePointAroundPivot(Vector3 _point, Vector3 _pivot, Quaternion _angle)
    {
        return _angle * (_point - _pivot) + _pivot;
    }


    /// <summary>
    /// Circles around the planet.
    /// </summary>
    private void CircleAround()
    {
        trans.position = RotatePointAroundPivot(trans.position, 
            pivot, 
            Quaternion.Euler(0, 0, rotateSpeed * Time.deltaTime));
    }


    /// <summary>
    /// Resets the variables.
    /// </summary>
    private void ResetParameter()
    {
        if (inCircle)
        {
            stateComponent.SetState(StateType.ExternalGravity, true);
            stateComponent.SetState(StateType.Movement, true);
        }

        currentPlanet.onDeath.RemoveListener(ResetParameter);
        currentPlanet = null;

        inCircle = false;
    }
}
