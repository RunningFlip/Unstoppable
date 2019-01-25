using UnityEngine;


/// <summary>
/// Handles the current states of the entity.
/// </summary>
public class StateComponent : EntityComponent
{
    [Header("States")]
    public int movingEnabled;
    public int interactingEnabled;
    public int damageEnabled;


    //Numbers
    private int stateStep = 1;


    public override void InitializeComponent()
    {
        //Setup
        ResetStates();
    }

    public override void UpdateComponent() { }


    /// <summary>
    /// Resets all states.
    /// </summary>
    public void ResetStates()
    {
        movingEnabled = 0;
        interactingEnabled = 0;
        damageEnabled = 0;
    }


    /// <summary>
    /// Enables or disables a specific state.
    /// </summary>
    /// <param name="_stateType">State to enable.</param>
    /// /// <param name="_enable">Is true if the state will be enabled.</param>
    public void SetState(StateType _stateType, bool _enable)
    {
        int step = (_enable) ? stateStep : -stateStep;

        switch (_stateType)
        {
            case StateType.Everything:
                movingEnabled += step;
                interactingEnabled += step;
                damageEnabled += step;
                break;
            case StateType.Movement:
                movingEnabled += step;
                break;
            case StateType.Interacting:
                interactingEnabled += step;
                break;
            case StateType.Damageable:
                damageEnabled += step;
                break;
        }
    }


    /// <summary>
    /// Return true, if the given state is enabled.
    /// </summary>
    /// <param name="_stateType">State that has to be checked.</param>
    /// <returns></returns>
    public bool StateIsEnabled(StateType _stateType)
    {
        bool enabled = false;

        switch (_stateType)
        {
            case StateType.Everything:
                enabled = (movingEnabled >= 0) && (interactingEnabled >= 0) && (damageEnabled >= 0);
                break;
            case StateType.Movement:
                enabled = (movingEnabled >= 0);
                break;
            case StateType.Interacting:
                enabled = (interactingEnabled >= 0);
                break;
            case StateType.Damageable:
                enabled = (damageEnabled >= 0);
                break;
        }
        return enabled;
    }
}
