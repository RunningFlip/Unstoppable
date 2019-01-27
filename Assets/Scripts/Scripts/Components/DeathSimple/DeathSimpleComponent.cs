using UnityEngine;


/// <summary>
/// Destroies or disables the entity.
/// </summary>
public class DeathSimpleComponent : EntityComponent
{
    [Header("Delay death")]
    public DeathType deathType = DeathType.Destroy;
    public float delayTime = 0;
    private WaitJob deathWaitJob;


    public override void InitializeComponent()
    {
        if (gameObject.activeSelf)
        {
            deathWaitJob = new WaitJob(delegate
            {
                switch (deathType)
                {
                    case DeathType.Destroy:
                        if (gameObject != null) Destroy(gameObject);
                        break;
                    case DeathType.Disable:
                        gameObject.SetActive(false);
                        break;
                }            
            }, delayTime);
        }
    }


    public override void UpdateComponent() { }


    /// <summary>
    /// Fallback if entity got destroyed before execution.
    /// </summary>
    private void OnDestroy()
    {
        deathWaitJob.CancelJob();
    }


    /// <summary>
    /// Destroies or disables the gameobject.
    /// </summary>
    /// <param name="_entityController">Entity to kill.</param>
    /// <param name="_deathType">Type of the death. (destroy/disable)</param>
    /// <param name="_delay">Possible delay for the death.</param>
    public static void AddDeathSimpleComponent(EntityController _entityController, DeathType _deathType = DeathType.Destroy, float _delay = 0.0f)
    {
        if (_entityController.GetComponent<DeathSimpleComponent>() != null || _entityController.gameObject == null) return;

        DeathSimpleComponent component = _entityController.gameObject.AddComponent<DeathSimpleComponent>();
        component.deathType = _deathType;
        component.delayTime = _delay;
        _entityController.AddComponent(component);
    }

}
