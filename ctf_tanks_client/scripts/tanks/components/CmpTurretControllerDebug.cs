using Godot;

public class CmpTurretControllerDebug
: CmpTurretController
{

  public override void 
  _PhysicsProcess(float _delta)
  {

    base._PhysicsProcess(_delta);

    //_DebugDirection();

    return;

  }

  private void
  _DebugDirection()
  {

    MasterManager master = MasterManager.GetInstance();

    master.DEBUG_MANAGER.DrawLine(GLOBAL_POSITION,
                                  GLOBAL_POSITION + GLOBAL_DIRECTION.Normalized() * 15.0f,
                                  new Color(0, 0, 1),
                                  2);

    return;

  }

}
