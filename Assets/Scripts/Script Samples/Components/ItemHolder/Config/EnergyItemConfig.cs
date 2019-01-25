using UnityEngine;


[CreateAssetMenu(fileName = "Energy Item Config", menuName = "Scriptable Objects/Item Configs/Energy Item Config")]
public class EnergyItemConfig : ItemConfig
{
    [Header("Energy values")]
    public float energy;


    public override void Execute(EntityController _entityController, EntityController _myEntity)
    {
        EnergyComponent energyComponent = _entityController.GetComponent<EnergyComponent>();
        energyComponent.currentEnergy += energy;

        base.Execute(_entityController, _myEntity);
    }
}
