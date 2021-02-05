using Godot;

public class STTCAM_TopOrthogonal
: STTCAM
{

  /**********************************************/
  /* Public                                     */
  /**********************************************/

  public override void 
  OnEnter()
  {

    _m_cameraManager.SetActiveCamera("top_ortho_general");

    return;

  }

  public override void
  OnUpdate()
  {

    return;

  }

  public override void
  OnExit()
  {

    return;

  }

}
