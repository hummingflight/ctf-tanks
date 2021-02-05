using Godot;
using System.Collections.Generic;

public class DebugManager
: Manager
{

  public DebugManager()
  {

    _m_commands = new Queue<DCMD>();

    return;

  }

  public override void 
  Receive(MESSAGE_ID _id, IMessage _msg)
  {

    switch(_id)
    {

      case MESSAGE_ID.KActive_Camera:

        SetDebugCamera((_msg as MSG_Camera).camera);
        return;

      default:
        return;

    }

  }

  public void
  SetDebugCamera(Camera _camera)
  {

    _m_debugCamera = _camera;

    return;

  }

  public void
  SetCanvasLayer(Control _debugLayer)
  {

    _m_debugLayer = _debugLayer;

    return;

  }

  public void
  AddCommand(DCMD _debugCommand)  
  {

    if(_m_commands.Count < _MAX_COMMANDS)
    {

      _m_commands.Enqueue((_debugCommand));

    }
    else
    {

      GD.Print("Maximum number of commands in the same frame reached. " +
              "Command will be ignored.");

    }

    return;

  }

  public void
  DrawCircle
  (
    Vector3 _position,
    float _radius,
    Color _color
  )
  {

    AddCommand(new DCMD_DrawCircle(this, _position, _radius, _color));

    return;

  }

  public void
  DrawLine
  (
    Vector3 _start,
    Vector3 _end,
    Color _color,
    float _width
  )
  {

    AddCommand(new DCMD_DrawLine(this, _start, _end, _color, _width));

    return;

  }

  public void
  DrawPath
  (
    Vector3[] _aPositions, 
    Color _color, 
    float _width
  )
  {

    AddCommand(new DCMD_DrawPath(this, _aPositions, _color, _width));

    return;

  }

  public void
  DrawLineStrip
  (
    Vector3[] _aPositions, 
    Color _color, 
    float _width
  )
  {

    AddCommand(new DCMD_DrawLineStrip(this, _aPositions, _color, _width));

    return;

  }

  public bool
  IsReady()
  {

    return _m_debugLayer != null && _m_debugCamera != null;

  }

  public void
  _Draw()
  {

    if(IsReady())
    {

      while (_m_commands.Count > 0)
      {

        DCMD command = _m_commands.Dequeue();

        command.Exec(_m_debugLayer);

      }

    }
    else
    {

      GD.Print("DebugManager is not ready.");

      Clear();

    }

    return;

  }

  public void
  Clear()
  {

    _m_commands.Clear();

    return;

  }

  public Control DEBUG_LAYER
  {
    get
    {
      return _m_debugLayer;
    }
  }

  public Camera DEBUG_CAMERA
  {
    get
    {
      return _m_debugCamera;
    }
  }

  /**********************************************/
  /* Private                                    */
  /**********************************************/

  private static int _MAX_COMMANDS = 15;

  private Control _m_debugLayer;

  private Camera _m_debugCamera;

  private Queue<DCMD> _m_commands;

}