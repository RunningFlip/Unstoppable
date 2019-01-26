using UnityEngine;


public class PlanetComponent : EntityComponent
{
    [Header("Planet Attributes")]
    public bool destroyable;
    public bool dangerous;
    public CircleCollider2D orbitCollider;


    //Events
    public SimpleEvent onDeath = new SimpleEvent();


    public override void InitializeComponent() { }

    private void OnDestroy()
    {
        onDeath.Invoke();
    }

    public override void UpdateComponent() { }
}

