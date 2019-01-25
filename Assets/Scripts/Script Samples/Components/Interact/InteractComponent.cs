using UnityEngine;


/// <summary>
/// Handles the interaction of the entity with objects.
/// </summary>
[RequireComponent(typeof(StateComponent))]
public class InteractComponent : EntityComponent
{
    [Header("Status")]
    public bool interact;


    //Components
    private StateComponent stateComponent;
    private AnimationComponent animationComponent;


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Components
        stateComponent = GetComponent<StateComponent>();
        animationComponent = GetComponent<AnimationComponent>();
    }


    public override void UpdateComponent()
    {
        if (!stateComponent.StateIsEnabled(StateType.Interacting)) return;

        if (interact)
        {
            interact = false;
            animationComponent.interact = true; //Animation

            Interact();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void Interact()
    {

    }
}
