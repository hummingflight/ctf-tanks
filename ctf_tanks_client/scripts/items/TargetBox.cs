using Godot;

public class TargetBox
  : KinematicActor
{

  public override void 
  _Ready()
  {

    Actor.AddComponent(new CmpHealth(m_initialHealth));
    Actor.AddComponent(new CmpReusable());

    return;

  }

  [Export]
  public int m_initialHealth;

}
