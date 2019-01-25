using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Is the entity of the object and includes all entitycomponents.
/// It also handles the update-behaviour of the entitycomponents.
/// </summary>
public class EntityController : MonoBehaviour, IUpdateable
{
    //List of components of the entity
    private List<EntityComponent> components = new List<EntityComponent>();
    private List<EntityComponent> updateComponents = new List<EntityComponent>();
    private List<EntityComponent> fixedUpdateComponents = new List<EntityComponent>();


    //Update start
    private void Start()
    {
        // 1. Components-list setup
        components = GetComponents<EntityComponent>().ToList();

        // 2. Initializes and sorts all components
        InitAndSort();      
    }


    /// <summary>
    /// Initializes the components and sorts them to the specific update-list.
    /// </summary>
    private void InitAndSort()
    {
        for (int i = 0; i < components.Count; i++)
        {
            EntityComponent component = components[i];
            component.InitializeComponent();

            //Sorts the components to the speicific updatetype.
            if (component.updateType == UpdateType.Update)              //Update
            {
                updateComponents.Add(component);
            }
            else if (component.updateType == UpdateType.FixedUpdate)    //FixedUpdate
            {
                fixedUpdateComponents.Add(component);
            }
        }
    }


    /// <summary>
    /// Will be exeuted every frame.
    /// </summary>
    public virtual void Tick()
    {
        for (int i = 0; i < updateComponents.Count; i++)
        {
            updateComponents[i].UpdateComponent();
        }
    }


    /// <summary>
    /// Will be exeuted multiple times every frame.
    /// </summary>
    public virtual void FixedTick()
    {
        for (int i = 0; i < fixedUpdateComponents.Count; i++)
        {
            fixedUpdateComponents[i].UpdateComponent();
        }
    }


    /// <summary>
    /// Adds a components.
    /// </summary>
    /// <param name="_entityComponent"></param>
    public void AddComponent(EntityComponent _entityComponent)
    {
        components.Add(_entityComponent);

        if (_entityComponent.updateType == UpdateType.Update)               //Update
        {
            updateComponents.Add(_entityComponent);
        }
        else if (_entityComponent.updateType == UpdateType.FixedUpdate)     //FixedUpdate
        {
            fixedUpdateComponents.Add(_entityComponent);
        }      
        _entityComponent.InitializeComponent();
    }

    /// <summary>
    /// Removes a component.
    /// </summary>
    /// <param name="_entityComponent"></param>
    public void RemoveComponent(EntityComponent _entityComponent)
    {
        if (components.Contains(_entityComponent)) {
            components.Remove(_entityComponent);

            if (_entityComponent.updateType == UpdateType.Update)           //Update
            {
                updateComponents.Remove(_entityComponent);
            }
            else if (_entityComponent.updateType == UpdateType.FixedUpdate) //FixedUpdate
            {
                fixedUpdateComponents.Remove(_entityComponent);
            }
            Destroy(_entityComponent);
        }
    }



    //--------Enable and Disable----------------------
    private void OnEnable()
    {
        Register();
    }
    private void OnDisable()
    {
        Unregister();
    }



    //--------Registration----------------------------
    public virtual void Register()
    {
        UpdateSystem.Register(this);
    }
    public virtual void Unregister()
    {
        UpdateSystem.Unregister(this);
    }
}
