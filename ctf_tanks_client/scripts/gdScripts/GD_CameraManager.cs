using Godot;

public class GD_CameraManager
: Node
{

  public override void 
  _Ready()
  {

    MasterManager master = MasterManager.GetInstance();

    _m_cameraManager 
       = master.GetManager<CameraManager>(MANAGER_KEY.kCameraManager);

    _m_cameraManager.SetActiveState(initState);

    return;
    
  }

  public override void
  _Process(float _delta)
  {

    _m_cameraManager.Update();

    return;

  }

  [Export]
  public CAMERA_MANAGER_STATE initState = CAMERA_MANAGER_STATE.KTop_orthographic;

  /**********************************************/
  /* Private                                    */
  /**********************************************/

  /// <summary>
  /// Camera Manager.
  /// </summary>
  private CameraManager _m_cameraManager;

}
