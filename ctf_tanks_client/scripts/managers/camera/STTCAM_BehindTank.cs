using Godot;

public class STTCAM_BehindTank
: STTCAM
{

  /**********************************************/
  /* Public                                     */
  /**********************************************/

  public STTCAM_BehindTank()
  {

    // Setup default camera translation.
    _m_v3Translation = new Vector3(0, 18, 10);

    _m_rotationRate = 1.5f;

  }

  public override void 
  OnEnter()
  {

    _m_cameraManager.SetActiveCamera("camera_behindTank");

    _m_ready = false;

    ////////////////////////////////////////////
    // Get Camera

    Camera camera = _m_cameraManager.GetCamera("camera_behindTank");

    if(camera != null)
    {

      _m_camera = camera;

      _m_ready = true;

    }
    else
    {

      GD.PrintErr("Camera state error: Camera not found");

      _m_ready = false;

    }

    ////////////////////////////////////////////
    // Get Tank

    MasterManager master = MasterManager.GetInstance();

    _m_tank = master.GAME_MANAGER.ACTIVE_TANK;

    if(_m_tank == null)
    {

      GD.PrintErr("No Active Tank");

      _m_ready = false;

    }

    return;

  }

  public override void
  OnUpdate()
  {

    if(_m_ready)
    {

      // Process Input.
      _Input(0.01f);

      // Get tank position.
      Vector3 tankPosition = _m_tank.GetNode().Transform.origin;

      // Define camera position.
      Vector3 cameraPosition = tankPosition + _m_v3Translation;

      // Target camera.
      _m_camera.LookAtFromPosition
      (
        cameraPosition,
        tankPosition,
        Vector3.Up
      );

    }

    return;

  }

  public override void
  OnExit()
  {

    _m_camera = null;
    _m_tank = null;
    _m_ready = false;

    return;

  }

  /**********************************************/
  /* Private                                    */
  /**********************************************/

  private void
  _Input(float _delta)
  {

    if(Input.IsActionPressed("ui_left"))
    {

      _m_v3Translation = _m_v3Translation.Rotated(Vector3.Up, -_m_rotationRate * _delta);

    }
    else if(Input.IsActionPressed("ui_right"))
    {

      _m_v3Translation = _m_v3Translation.Rotated(Vector3.Up, _m_rotationRate * _delta);

    }

    return;

  }

  /// <summary>
  /// Indicates if the state is ready to update.
  /// </summary>
  private bool _m_ready;

  /// <summary>
  /// Camera.
  /// </summary>
  private Camera _m_camera;

  /// <summary>
  /// Target tank.
  /// </summary>
  private Actor<KinematicBody> _m_tank;

  /// <summary>
  /// The X translation of the camera.
  /// </summary>
  private Vector3 _m_v3Translation;

  /// <summary>
  /// Rotation rate of change per second.
  /// </summary>
  private float _m_rotationRate;

}
