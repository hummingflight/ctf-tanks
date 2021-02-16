using Godot;

public class TankActor
  : KinematicActor
{

  override public void
  _Ready()
  {

    // Create Actor

    _m_actor = new Actor<KinematicBody>(this);

    // Create Blackboard Items

    Blackboard blackboard = _m_actor.m_blackboard;

    blackboard.AddItem(BLACKBOARD_ITEM.kTank_Steering, new BItem());
    blackboard.AddItem(BLACKBOARD_ITEM.kAcceleration_Strength, new BItem());
    blackboard.AddItem(BLACKBOARD_ITEM.kReverse_Strength, new BItem());
    blackboard.AddItem(BLACKBOARD_ITEM.kFire_Button, new BItem());

    // Create Component

    CmpTankProperties properties = new CmpTankProperties();

    _m_actor.AddComponent(properties);
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

  [Export]
  public TEAM_KEY m_team;

}