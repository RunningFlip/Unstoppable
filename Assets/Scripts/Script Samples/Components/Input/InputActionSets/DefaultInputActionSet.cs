using InControl;


public class DefaultInputActionSet : PlayerActionSet
{
    //Player movement
    public PlayerAction moveUp;
    public PlayerAction moveDown;
    public PlayerAction moveLeft;
    public PlayerAction moveRight;
    public PlayerTwoAxisAction movement;

    //Actions
    public PlayerAction dash;
    public PlayerAction interact;

    //General
    public PlayerAction radar;



    /// <summary>
    /// Default Input Actionset setup.
    /// </summary>
    public DefaultInputActionSet()
    {
        //Camera movement
        moveUp = CreatePlayerAction("Move Up");
        moveDown = CreatePlayerAction("Move Down");
        moveLeft = CreatePlayerAction("Move Left");
        moveRight = CreatePlayerAction("Move Right");
        movement = CreateTwoAxisPlayerAction(moveLeft, moveRight, moveDown, moveUp);

        //Actions
        dash = CreatePlayerAction("Dash");
        interact = CreatePlayerAction("Interact");

        //General
        radar = CreatePlayerAction("Radar");

        //Add bindings
        AddDefaultKeyboardAndMouseBindings();
    }


    /// <summary>
    /// Binds the specific input possibilities to the playeractions.
    /// </summary>
    public void AddDefaultKeyboardAndMouseBindings()
    {
        //Camera movement
        moveUp.AddDefaultBinding(Key.W);
        moveDown.AddDefaultBinding(Key.S);
        moveLeft.AddDefaultBinding(Key.A);
        moveRight.AddDefaultBinding(Key.D);

        //Actions
        dash.AddDefaultBinding(Mouse.LeftButton);
        interact.AddDefaultBinding(Key.F);

        //General
        radar.AddDefaultBinding(Key.E);
    }

}
