using Godot;

public class CmpTankVisionDebug
: CmpTankVision
{

  public override void 
  _PhysicsProcess(float _delta)
  {

    base._PhysicsProcess(_delta);

    DebugVisionRange();

    DebugCollisions();
  
  }

  private void
  DebugVisionRange()
  {

    MasterManager master = MasterManager.GetInstance();

    master.DEBUG_MANAGER.DrawCone(new Color(0, 1, 1, 1),
                                  2,
                                  _m_physics.POSITION,
                                  _m_physics.DIRECTION,
                                  _m_node.Transform.basis.y,
                                  _m_radius,
                                  _m_openingAngle);

    return;

  }

  private void
  DebugCollisions()
  {

    return;

  }

}
