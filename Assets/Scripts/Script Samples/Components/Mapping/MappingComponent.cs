using UnityEngine;


/// <summary>
/// Stores multiple components of the entity to provide a fast and performant access.
/// </summary>
public class MappingComponent : EntityComponent
{
    [Header("Movement")]
    public Transform movementTransform;

    [Header("Sprite Renderer")]
    public SpriteRenderer[] spriteRenderer;

    [Header("Animator")]
    public Animator animator;

    [Header("Hitbox")]
    public Collider2D hitbox;

    [Header("Physics")]
    public Rigidbody2D rbody;
    public Collider2D[] colliders;


    public override void InitializeComponent() { }
    public override void UpdateComponent() { }

}
