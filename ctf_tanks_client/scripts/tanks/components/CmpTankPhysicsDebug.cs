using Godot;

public class CmpTankPhysicsDebug
: CmpTankPhysics
{

  /**********************************************/
  /* Public                                     */
  /**********************************************/

  public override void 
  _Ready()
  {

    // Ready.

    base._Ready();

    // Get debug manager.

    MasterManager master = MasterManager.GetInstance();
    _m_debugManager = master.GetManager<DebugManager>(MANAGER_KEY.kDebugManager);

    return;

  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="delta"></param>
  public override void
  _PhysicsProcess(float delta)
  {

    // Update.

    base._PhysicsProcess(delta);

    ////////////////////////////////////////////
    // Debugging

    _DebugVelocity();

    _DebugAcceleration();

    _DebugWheelSteering();

    return;

  }

  /**********************************************/
  /* Private                                    */
  /**********************************************/

  private void
  _DebugVelocity()
  {

    Vector3 velocity = new Vector3(_m_v3Velocity);

    velocity /= ENGINE_POWER;

    _m_debugManager.DrawLine
    (
      _m_node.Transform.origin,
      _m_node.Transform.origin + velocity * 15.0f,
      new Color(0.0f, 0.0f, 1.0f, 1.0f),
      2.0f
    );

  }
  
  /// <summary>
  /// Display a debugging line in the screen representing the acceleration vector.
  /// </summary>
  private void
  _DebugAcceleration()
  {

    if (_m_v3Acceleration.Length() > 0)
    {

      _m_debugManager.DrawLine
      (
        _m_node.Transform.origin,
        _m_node.Transform.origin + _m_v3Acceleration.Normalized() * 15.0f,
        new Color(1.0f, 0.0f, 0.0f, 1.0f),
        2.0f
      );

    }

    return;

  }

  /// <summary>
  /// Debug the wheel steering.
  /// </summary>
  /// <param name="_debugManager"></param>
  /// <param name="_physics"></param>
  /// <param name="_value"></param>
  private void
  _DebugWheelSteering()
  {

    if(_m_steeringAngle != 0)
    {

      Vector3 steerVector = DIRECTION.Rotated
      (
        _m_node.Transform.basis.y.Normalized(), _m_steeringAngle
      );

      _m_debugManager.DrawLine
      (
        _m_node.Transform.origin,
        _m_node.Transform.origin + steerVector * 15.0f,
        new Color(1.0f, 0.0f, 1.0f, 1.0f),
        2.0f
      );

    }

    

    return;

  }

  private DebugManager _m_debugManager;

}