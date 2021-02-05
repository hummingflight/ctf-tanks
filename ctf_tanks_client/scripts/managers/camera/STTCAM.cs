public class STTCAM
{
  /**********************************************/
  /* Public                                     */
  /**********************************************/
  
  public virtual void
  OnEnter()
  {

    return;

  }

  public virtual void
  OnUpdate()
  {

    return;

  }

  public virtual void
  OnExit()
  {

    return;

  }

  public void
  SetCameraManager(CameraManager _cameraManager)
  {

    _m_cameraManager = _cameraManager;
    
    return;

  }

  /**********************************************/
  /* Protected                                  */
  /**********************************************/

  /// <summary>
  /// Reference to the camera manager.
  /// </summary>
  protected CameraManager _m_cameraManager;

}