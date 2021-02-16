using Godot;
using System.Collections.Generic;

public class Actor<T>
  where T : Node
{

  /// <summary>
  /// Create an actor and wrap a node.
  /// </summary>
  /// <param name="_node">Wrapped node.</param>
  public 
  Actor(T _node)
  {

    _m_node = _node;

    m_blackboard = new Blackboard();

    _m_aBroadcastCmd = new List<BroadcastCommand>();

    _m_hComponents = new Dictionary<COMPONENT_ID, Component<T>>();

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

  public U 
  GetComponent<U>(COMPONENT_ID _componentID) where U : Component<T>
  {

    if(HasComponent(_componentID))
    {

      return _m_hComponents[_componentID] as U;

    }
    else
    {

      GD.Print("Component: " + _componentID.ToString() + " doesn't exists.");

      return default;

    }    

  }

  public bool
  HasComponent(COMPONENT_ID _componentID)
  {

    return _m_hComponents.ContainsKey(_componentID);

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
    if(HasComponent(componentID))
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

      // Set self as the actor of the component.
      _component.SetActor(this);

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

    if(HasComponent(_componentID))
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

    if(!_m_toDestroy)
    {

      // Add broadcast command to list.
      _m_aBroadcastCmd.Add(new BroadcastCommand(_messageID, _message));

      if (!_m_isBroadcasting) // if not running, run broadcast
      {

        _RunBroadcast();

      }

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

  /// <summary>
  /// Safely destroys this actor. The "Destroy" method of each children will be
  /// called.
  /// </summary>
  public void
  Destroy()
  {

    // Prepare to destroy.
    _m_toDestroy = true;

    // Remove any message.
    _m_aBroadcastCmd.Clear();

    // Destroy each component.
    foreach (KeyValuePair<COMPONENT_ID, Component<T>> pair in _m_hComponents)
    {

      pair.Value.Destroy();

    }

    _m_hComponents.Clear();
    _m_hComponents = null;

    // Destroy node.
    _m_node.QueueFree();
    _m_node = default;

    return;

  }

  /// <summary>
  /// Common components data.
  /// </summary>
  public Blackboard m_blackboard;

  protected void
  _RunBroadcast()
  {

    _m_isBroadcasting = true;

    while(_m_aBroadcastCmd.Count > 0)
    {

      // Get command.
      BroadcastCommand command = _m_aBroadcastCmd[0];

      // Remove command from list.
      _m_aBroadcastCmd.RemoveAt(0);

      // Send message to each component.
      foreach (KeyValuePair<COMPONENT_ID, Component<T>> pair in _m_hComponents)
      {

        pair.Value.ReceiveMessage(command.m_messageID, command.m_message);

      }

      // Check for destroying.
      if(command.m_messageID == MESSAGE_ID.kDestroy)
      {

        Destroy();

      }

    }

    // update broadcasting status.
    _m_isBroadcasting = false;

    return;

  }

  /// <summary>
  /// Indicates if this actor is enable.
  /// </summary>
  public bool
  IS_ENABLE
  {
    get
    {
      return _m_isEnable;
    }
    set
    {

      _m_isEnable = value;

      if (value)
      {

        // Send message to each component.
        foreach (KeyValuePair<COMPONENT_ID, Component<T>> pair in _m_hComponents)
        {

          pair.Value.OnEnable();

        }

      }
      else
      {

        // Send message to each component.
        foreach (KeyValuePair<COMPONENT_ID, Component<T>> pair in _m_hComponents)
        {

          pair.Value.OnEnable();

        }

      }

      return;
    }
  }

  /// <summary>
  /// Map of components in this node.
  /// </summary>
  protected Dictionary<COMPONENT_ID, Component<T>> _m_hComponents;

  /// <summary>
  /// The wrapped node.
  /// </summary>
  protected T _m_node;

  /// <summary>
  /// Indicates if this actor is enable.
  /// </summary>
  protected bool _m_isEnable = true;

  /// <summary>
  /// Indicates if the object is ready to be destroy
  /// </summary>
  protected bool _m_toDestroy = false;

  /// <summary>
  /// Indicates if the actor is currently broadcasting a message.
  /// </summary>
  protected bool _m_isBroadcasting = false;

  /// <summary>
  /// List of broadcast commands.
  /// </summary>
  protected List<BroadcastCommand> _m_aBroadcastCmd;

}
