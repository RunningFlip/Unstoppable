using UnityEngine;
using System;


/// <summary>
/// An EntityComponent includes a specific single thematic functionality.
/// It is based on the concepts of data-orientation and should be used accordingly.
/// </summary>
[RequireComponent(typeof (EntityController))]
public abstract class EntityComponent : MonoBehaviour
{
    /// <summary>
    /// Defines if the components has to be updated every frame.
    /// </summary>
    [NonSerialized]
    public UpdateType updateType = UpdateType.None;


    /// <summary>
    /// Function to add this type of the entity-component to the entity-controller object.
    /// </summary>
    /// <param name="entityController"></param>
    public abstract void InitializeComponent();


    /// <summary>
    /// Update call
    /// </summary>
    public abstract void UpdateComponent();

}
