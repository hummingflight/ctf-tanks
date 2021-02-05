using Godot;

public class DCMD_DrawCircle
: DCMD
{

  public DCMD_DrawCircle
  (
    DebugManager _debugManager,
    Vector3 _position,
    float _radius,
    Color _color
  )
  {

    _m_position = _debugManager.DEBUG_CAMERA.UnprojectPosition(_position);

    _m_radius = _radius;

    _m_color = _color;

    return;

  }

  public override void
  Exec(Control _canvas)
  {

    _canvas.DrawCircle(_m_position, _m_radius, _m_color);

    return;

  }

  private Vector2 _m_position;

  private float _m_radius;

  private Color _m_color;

}
