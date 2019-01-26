using UnityEngine;


/// <summary>
/// Handles the input of the player and triggers different components and functions.
/// </summary>
public class InputComponent : EntityComponent
{
    [Header("Reading Status")]
    public bool readInput = true;


    //Vectors
    private Vector2 newDirection;

    //Actionset of controls
    private DefaultInputActionSet actionSet = null;

    //Components
    private MovementComponent movementComponent;
    private DashComponent dashComponent;
    private InteractComponent interactComponent;


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Setup
        if (actionSet == null) actionSet = new DefaultInputActionSet();

        //Components
        movementComponent = GetComponent<MovementComponent>();
        dashComponent = GetComponent<DashComponent>();
        interactComponent = GetComponent<InteractComponent>();
    }


    public override void UpdateComponent()
    {
        if (!readInput) return;

        //Actions-------------------------------------------------------------
        if (actionSet.dash.WasPressed)
        {
            dashComponent.dash = true;
        }
        if (actionSet.interact.WasPressed)
        {
            Debug.Log("Interact was pressed!");
            interactComponent.interact = true;
        }

        //General-------------------------------------------------------------
        if (actionSet.radar.WasPressed)
        {
            Debug.Log("Context Menu was toggled!");
        }
    }
}