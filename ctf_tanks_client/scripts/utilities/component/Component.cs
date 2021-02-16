﻿using Godot;

public class Component<T>
  where T : Node
{ 

  /// <summary>
  /// 
  /// </summary>
  /// <param name="_messageID"></param>
  /// <param name="_message"></param>
  virtual public void
  ReceiveMessage(MESSAGE_ID _messageID, IMessage _message)
  {

    return;

  }

  /// <summary>
  /// Called during the processing step of the main loop. Processing happens at 
  /// every frame and as fast as possible, so the delta time since the previous 
  /// frame is not constant.  /// </summary>
  /// <param name="_delta"></param>
  virtual public void
  _Process(float _delta)
  {

    return;

  }

  /// <summary>
  ///Called during the physics processing step of the main loop. Physics 
  ///processing means that the frame rate is synced to the physics, i.e. the 
  ///delta variable should be constant.
  /// </summary>
  /// <param name="_delta"></param>
  virtual public void
  _PhysicsProcess(float _delta)
  {

    return;

  }

  /// <summary>
  /// Called by the component manager when the node is "ready", i.e. when both the 
  /// node and its children have entered the scene tree. Take account that this
  /// method only will be called by the compound node only if this component is
  /// added before.
  /// </summary>
  virtual public void
  _Ready()
  {

    return;

  }

  /// <summary>
  /// Called once by the attached component manager, after this component is added.
  /// </summary>
  virtual public void
  OnConnect()
  {

    return;

  }

  /// <summary>
  /// Called once by the attached component manager once, before this component is removed.
  /// </summary>
  virtual public void
  OnDisconnect()
  {

    return;

  }

  /// <summary>
  /// Trigger when the actor is enable.
  /// </summary>
  virtual public void
  OnEnable()
  {

    return;

  }

  /// <summary>
  /// Trigger when the actor is disable.
  /// </summary>
  virtual public  void
  OnDisable()
  {

    return;

  }

  /// <summary>
  /// Get the component identifier.
  /// </summary>
  /// <returns></returns>
  virtual public COMPONENT_ID
  GetID()
  {

    return COMPONENT_ID.kUndefined;

  }

  /// <summary>
  /// Safely destroys this component.
  /// </summary>
  virtual public void
  Destroy()
  {

    return;

  }

  /// <summary>
  /// Set the composite node of this component.
  /// </summary>
  /// <param name="_actor"></param>
  public void
  SetActor(Actor<T> _actor)
  {

    // Set the composite node.
    _m_actor = _actor;

    // Set the node.
    SetNode(_m_actor.GetNode());

    return;

  }

  public void
  SetNode(T _node)
  {

    _m_node = _node;

    return;

  }

  /// <summary>
  /// The composite node of this component.
  /// </summary>
  protected Actor<T> _m_actor;

  /// <summary>
  /// The wrapped node in the composite node.
  /// </summary>
  protected T _m_node;

}
