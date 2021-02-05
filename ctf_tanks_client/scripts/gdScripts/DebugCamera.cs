using Godot;

public class DebugCamera 
: Camera
{
  /**********************************************/
  /* Public                                     */
  /**********************************************/
  
  public override void 
  _Ready()
  {

    MasterManager master = MasterManager.GetInstance();

    CameraManager cameraManager
      = master.GetManager<CameraManager>(MANAGER_KEY.kCameraManager);

    cameraManager.AddCamera(cameraKey, this);

  }

  [Export]
  public string cameraKey = "camera_name";

}
