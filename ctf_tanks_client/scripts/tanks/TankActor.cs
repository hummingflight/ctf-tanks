using Godot;

public class TankActor
  : KinematicBody
{

  override public void
  _Ready()
  {

    // Create Actor

    _m_actor = new Actor<KinematicBody>(this);

    // Create Blackboard Items

    Blackboard blackboard = _m_actor.m_blackboard;

    blackboard.AddItem(BLACKBOARD_ITEM.kTank_Steering, new BlackboardItem());
    blackboard.AddItem(BLACKBOARD_ITEM.kAcceleration_Strength, new BlackboardItem());
    blackboard.AddItem(BLACKBOARD_ITEM.kReverse_Strength, new BlackboardItem());

    // Create Component

    _m_actor.AddComponent(new CmpTankInput());
    _m_actor.AddComponent(new CmpTankPhysics());

    // Actor ready.
    
    _m_actor._Ready();

    return;

  }

  override public void
  _Process(float _delta)
  {

    _m_actor._Process(_delta);

    return;

  }

  override public void
  _PhysicsProcess(float _delta)
  {

    _m_actor._PhysicsProcess(_delta);

    return;

  }

  protected Actor<KinematicBody> _m_actor;

}