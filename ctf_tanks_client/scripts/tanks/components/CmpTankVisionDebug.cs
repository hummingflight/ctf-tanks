using Godot;
using System.Collections.Generic;

public class CmpTankVisionDebug
: CmpTankVision
{

  public override void 
  _PhysicsProcess(float _delta)
  {

    base._PhysicsProcess(_delta);

    DebugVisionRange();

    DebugVisibleObjects();
  
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
  DebugVisibleObjects()
  {

    MasterManager master = MasterManager.GetInstance();

    List<KinematicActor> visibleObjects = GetVisibleBodies();

    foreach(KinematicActor body in visibleObjects)
    {

      master.DEBUG_MANAGER.DrawCircle(body.Transform.origin,
                                      3.0f,
                                      new Color(1, 1, 0));

    }

    return;

  }

}
