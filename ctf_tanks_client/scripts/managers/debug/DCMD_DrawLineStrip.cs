using Godot;

public class DCMD_DrawLineStrip
: DCMD
{

  public DCMD_DrawLineStrip
    (
      DebugManager _debugManager,
      Vector3[] _aPositions, 
      Color _color, 
      float _width
    )
  {

    // Get Unprojected positions
    Vector2[] points = new Vector2[_aPositions.Length];

    for (int i = 0; i < _aPositions.Length; ++i)
    {

      points[i] = _debugManager.DEBUG_CAMERA.UnprojectPosition(_aPositions[i]);

    }

    _m_aPositions = points;

    // Save line properties
    _m_color = _color;
    _m_width = _width;

    return;

  }

  public override void
  Exec(Control _canvas)
  {   

    _canvas.DrawPolyline(_m_aPositions, _m_color, _m_width);

    return;

  }

  private Vector2[] _m_aPositions;
  
  private Color _m_color;

  private float _m_width;

}