
/// <summary>
/// The interface for updateable classes, that is required for the entity-update-framework.
/// </summary>
public interface IUpdateable
{
    void Tick();
    void FixedTick();
    void Register();
    void Unregister();
}



