using Godot;

public class MSG_Camera
  : IMessage
{

  /**********************************************/
  /* Public                                     */
  /**********************************************/
  
  public MSG_Camera(Camera _camera)
  {

    camera = _camera;

    return;

  }

  public Camera camera;

}
