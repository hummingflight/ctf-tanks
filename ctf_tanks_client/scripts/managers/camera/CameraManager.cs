using Godot;
using System.Collections.Generic;

public class CameraManager
: Manager
{
  
  /**********************************************/
  /* Public                                     */
  /**********************************************/
  
  public CameraManager()
  {

    _m_hCameras = new Dictionary<string, Camera>();

    _m_hStates = new Dictionary<CAMERA_MANAGER_STATE, STTCAM>();

    // Add states.

    _AddState
    (
      CAMERA_MANAGER_STATE.KTop_orthographic, 
      new STTCAM_TopOrthogonal()
    );

    _AddState
    (
      CAMERA_MANAGER_STATE.kBehind_active_tank,
      new STTCAM_BehindTank()
    );

    _m_activeState = new STTCAM();

    return;

  }

  /// <summary>
  /// Update the active state.
  /// </summary>
  public void
  Update()
  {

    _m_activeState.OnUpdate();

    return;

  }

  /// <summary>
  /// Set the active camera manager state.
  /// </summary>
  /// <param name="_state">Camera manager state.</param>
  public void
  SetActiveState(CAMERA_MANAGER_STATE _state)
  {

    _m_activeState.OnExit();

    _m_activeState = _m_hStates[_state];

    _m_activeState.OnEnter();

    return;

  }

  public void
  AddCamera(string _key, Camera _camera)
  {

    _m_hCameras.Add(_key, _camera);

    return;

  }

  /// <summary>
  /// Set the active camera by key.
  /// </summary>
  /// <param name="_key">Camera key.</param>
  public void
  SetActiveCamera(string _key)
  {

    Camera camera = GetCamera(_key);

    if(camera != null)
    {

      SetActiveCamera(camera);

    }

    return;

  }

  /// <summary>
  /// Set the active camera.
  /// </summary>
  /// <param name="_activeCamera"></param>
  public void
  SetActiveCamera(Camera _activeCamera)
  {

    // Disable current camera.
    if(_m_activeCamera != null)
    {

      _m_activeCamera.Current = false;

    }

    _m_activeCamera = _activeCamera;

    _m_activeCamera.Current = true;

    _m_masterManager.Broadcast
    (
      MESSAGE_ID.KActive_Camera, 
      new MSG_Camera(_activeCamera)
    );

    return;

  }

  public Camera
  GetCamera(string _key)
  {

    if(HasCamera(_key))
    {

      return _m_hCameras[_key];

    }

    GD.PrintErr("Camera: " + _key + " doesn't exists.");

    return null;

  }

  public bool
  HasCamera(string _key)
  {

    return _m_hCameras.ContainsKey(_key);

  }

  /**********************************************/
  /* Private                                    */
  /**********************************************/

  private void
  _AddState(CAMERA_MANAGER_STATE _key, STTCAM _state)
  {

    _m_hStates.Add(_key, _state);

    _state.SetCameraManager(this);

    return;

  }

  /// <summary>
  /// Active state.
  /// </summary>
  private STTCAM _m_activeState;

  /// <summary>
  /// Table of states.
  /// </summary>
  private Dictionary<CAMERA_MANAGER_STATE, STTCAM> _m_hStates;

  /// <summary>
  /// Active camera.
  /// </summary>
  private Camera _m_activeCamera;

  /// <summary>
  /// Table of cameras.
  /// </summary>
  private Dictionary<string, Camera> _m_hCameras;

}
