using Godot;

public class RigidBodyActor
  : RigidBody
{

  override public void
  _Ready()
  {



    m_actor._Ready();

    return;

  }

  override public void
  _Process(float _delta)
  {

    m_actor._Process(_delta);

    return;

  }

  override public void
  _PhysicsProcess(float _delta)
  {

    m_actor._PhysicsProcess(_delta);

    return;

  }

  public Actor<RigidBody> m_actor;

  [Export]
  public Script[] m_aComponents;

}
