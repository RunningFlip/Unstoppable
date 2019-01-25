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
    private AttackComponent attackComponent;
    private InteractComponent interactComponent;


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Setup
        if (actionSet == null) actionSet = new DefaultInputActionSet();

        //Components
        movementComponent = GetComponent<MovementComponent>();
        attackComponent = GetComponent<AttackComponent>();
        interactComponent = GetComponent<InteractComponent>();
    }


    public override void UpdateComponent()
    {
        if (!readInput) return;


        //Player movement-----------------------------------------------------
        /*
        if (actionSet.movement.IsPressed)                                           //Movement
        {
            newDirection = new Vector2(actionSet.movement.X, actionSet.movement.Y);
            newDirection = newDirection.normalized;
            movementComponent.currentDirection = newDirection;
        }
        */

        //Actions-------------------------------------------------------------
        if (actionSet.attack.WasPressed)
        {
            attackComponent.attack = true;
        }
        if (actionSet.dodge.WasPressed)
        {
            Debug.Log("Dodge was pressed!");
        }
        if (actionSet.interact.WasPressed)
        {
            Debug.Log("Interact was pressed!");
            interactComponent.interact = true;
        }

        //General-------------------------------------------------------------
        if (actionSet.contextMenu.WasPressed)
        {
            Debug.Log("Context Menu was toggled!");
        }
    }
}