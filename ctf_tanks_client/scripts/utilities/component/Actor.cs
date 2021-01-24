using Godot;

using System.Collections.Generic;

public class Actor<T>
{

  public 
  Actor(T _node)
  {

    _m_node = _node;

    return;

  }

  virtual public void
  _Ready()
  {

    // Call ready method for each component.
    foreach (KeyValuePair<COMPONENT_ID, Component<T>> pair in _m_hComponents)
    {

      pair.Value._Ready();

    }

    return;

  }

  virtual public void
  _Process(float _delta)
  {

    // Update each component.
    foreach(KeyValuePair<COMPONENT_ID, Component<T>> pair in _m_hComponents)
    {

      pair.Value._Process(_delta);

    }

    return;

  }

  virtual public void
  _PhysicsProcess(float _delta)
  {

    // Update each component.
    foreach (KeyValuePair<COMPONENT_ID, Component<T>> pair in _m_hComponents)
    {

      pair.Value._PhysicsProcess(_delta);

    }

    return;

  }

  /// <summary>
  /// Adds a component to this component manager.
  /// </summary>
  /// <param name="_component">Component</param>
  /// <returns></returns>
  public OPERATION_RESULT 
  AddComponent(Component<T> _component)
  {

    COMPONENT_ID componentID = _component.GetID();

    // Check if a component of same type already exists in this Node.
    if(_m_hComponents.ContainsKey(componentID))
    {

      // Log error.
      GD.Print("Component: " + componentID.ToString() + " already exists.");

      // Operation failed.
      return OPERATION_RESULT.kFail;

    }
    else
    {

      // Add component to this node.
      _m_hComponents.Add(componentID, _component);

      // Set self as the component manager of the component.
      _component.SetComponentManager(this);

      // Call OnConnect method.
      _component.OnConnect();

    }

    // Successful operation.
    return OPERATION_RESULT.kSuccess;

  }

  /// <summary>
  /// Removes a component from this component manager. This operation will trigger the 
  /// "OnDiconnected" method of the component. The component will not be destroyed.
  /// </summary>
  /// <param name="_componentID">Component ID.</param>
  /// <returns>The removed node. Null if the node doesn't exists.</returns>
  public Component<T>
  RemoveComponent(COMPONENT_ID _componentID)
  {

    if(_m_hComponents.ContainsKey(_componentID))
    {

      // Get Component.
       Component<T> component = _m_hComponents[_componentID];

      // Call OnDisconnect method.
      component.OnDisconnect();

      // Remove component.
      _m_hComponents.Remove(_componentID);

      // Returns component.
      return component;

    }

    //Component doesn't exists in this node.
    GD.Print("Component of ID: " + _componentID.ToString() + " doesn't exists in this node.");

    return null;

  }

  /// <summary>
  /// Removes and destroys a component from this component manager. This operation will 
  /// trigger the "OnDiconnected" method of the component, then the 
  /// "Destroy" method will be called.
  /// </summary>
  /// <param name="_componentID">Component ID</param>
  /// <returns>Operation result.</returns>
  public OPERATION_RESULT
  RemoveAndDestroyComponent(COMPONENT_ID _componentID)
  {

    // Remove component from this node.
    Component<T> component = RemoveComponent(_componentID);

    if(component == null)
    {

      return OPERATION_RESULT.kFail;

    }
    else
    {

      // Destroy component.
      component.Destroy();

      return OPERATION_RESULT.kSuccess;

    }    

  }

  /// <summary>
  /// Sends a message to each component.
  /// </summary>
  public void
  Broadcast(MESSAGE_ID _messageID, IMessage _message)
  {

    // Send message to each component.
    foreach (KeyValuePair<COMPONENT_ID, Component<T>> pair in _m_hComponents)
    {

      pair.Value.ReceiveMessage(_messageID, _message);

    }

    return;

  }

  /// <summary>
  /// Get the wrapped node.
  /// </summary>
  /// <returns></returns>
  public T
  GetNode()
  {

    return _m_node;

  }

  public void
  Destroy()
  {

    // Send message to each component.
    foreach (KeyValuePair<COMPONENT_ID, Component<T>> pair in _m_hComponents)
    {

      pair.Value.Destroy();

    }

    _m_hComponents.Clear();
    _m_hComponents = null;

    _m_node = default(T);

    return;

  }

  /// <summary>
  /// Map of components in this node.
  /// </summary>
  protected Dictionary<COMPONENT_ID, Component<T>> _m_hComponents;

  /// <summary>
  /// The wrapped node.
  /// </summary>
  protected T _m_node;

}
