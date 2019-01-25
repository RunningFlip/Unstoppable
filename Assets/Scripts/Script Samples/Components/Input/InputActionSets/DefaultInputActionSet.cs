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
    public PlayerAction attack;
    public PlayerAction dodge;
    public PlayerAction interact;

    //General
    public PlayerAction contextMenu;



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
        attack = CreatePlayerAction("Attack");
        dodge = CreatePlayerAction("Dodge");
        interact = CreatePlayerAction("Interact");

        //General
        contextMenu = CreatePlayerAction("Context Menu");

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
        attack.AddDefaultBinding(Mouse.LeftButton);
        dodge.AddDefaultBinding(Key.Space);
        interact.AddDefaultBinding(Key.F);

        //General
        contextMenu.AddDefaultBinding(Key.Escape);
    }

}
