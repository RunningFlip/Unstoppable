using UnityEngine;


public class PlanetComponent : EntityComponent
{
    [Header("Planet Attributes")]
    public CircleCollider2D orbitCollider;


    public override void InitializeComponent() { }
    public override void UpdateComponent() { }
}
