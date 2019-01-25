using UnityEngine;


/// <summary>
/// Handles all animations of the animator of the entity.
/// </summary>
[RequireComponent(typeof(MappingComponent))]
public class AnimationComponent : EntityComponent
{
    [Header("Animation States")]
    public bool move;
    public bool attack;
    public bool dodge;
    public bool interact;
    public bool death;


    //Flag
    private bool lastMove;

    //Components
    private Animator animator;


    public override void InitializeComponent()
    {
        updateType = UpdateType.Update;

        //Components
        animator = GetComponent<MappingComponent>().animator;
    }


    public override void UpdateComponent()
    {
        //Move
        if (lastMove != move)
        {
            lastMove = move;
            animator.SetBool("Moving", move);
        }

        //Attack
        if (attack)
        {
            attack = false;
            animator.SetTrigger("Attack");
        }

        //Dodge
        if (dodge)
        {
            dodge = false;
            animator.SetTrigger("Dodge");
        }

        //Use
        if (interact)
        {
            interact = false;
            animator.SetTrigger("Interact");
        }

        //Death
        if (death)
        {
            death = false;
            animator.SetBool("Dead", true);
        }
    }

}
