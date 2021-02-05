using Godot;

public class DCMD_DrawLine
: DCMD
{

  /**********************************************/
  /* Public                                     */
  /**********************************************/
  
  public DCMD_DrawLine()
  {

    _m_start = new Vector2();
    _m_end = new Vector2();
    _m_color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    _m_width = 1.0f;

    return;

  }

  public DCMD_DrawLine
    (
      DebugManager _debugManager,
      Vector3 _start,
      Vector3 _end,
      Color _color,
      float _width
    )
  {

    Set
    (
      _debugManager,
      _start,
      _end,
      _color,
      _width
    );

    return;

  }

  public override void
  Exec(Control _canvas)
  {

    _canvas.DrawLine(_m_start, _m_end, _m_color, _m_width);

    return;

  }

  public void
  Set
  (
    DebugManager _debugManager,
    Vector3 _start,
    Vector3 _end,
    Color _color,
    float _width
  )
  {

    _m_start = _debugManager.DEBUG_CAMERA.UnprojectPosition(_start);

    _m_end = _debugManager.DEBUG_CAMERA.UnprojectPosition(_end);

    _m_color = _color;

    _m_width = _width;

    return;

  }

  private Vector2 _m_start;

  private Vector2 _m_end;

  private Color _m_color;

  private float _m_width;

}