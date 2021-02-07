using Godot;

public class DCMD_DrawPath
: DCMD
{

  public DCMD_DrawPath
  (
    DebugManager _debugManager,
    ActiveItemVector<CTF.PathNode> _path, 
    Color _color, 
    float _width
  )
  {

    int pathSize = _path.SIZE;

    // Get Unprojected Positions.
    Vector2[] points = new Vector2[pathSize];

    ItemVectorNode<CTF.PathNode> item = _path.GetFirst();
    int i = 0;

    while(item != _path.END)
    {

      points[i] = _debugManager.DEBUG_CAMERA.UnprojectPosition(item.m_item.position);

      item = item.GetNext();
      ++i;

    }

    _m_aPositions = points;

    // Save line properties.
    _m_color = _color;
    _m_width = _width;

    return;

  }

  public override void
  Exec(Control _canvas)
  {    

    _canvas.DrawPolyline(_m_aPositions, _m_color, _m_width); 
    
    foreach(Vector2 point in _m_aPositions)
    {

      _canvas.DrawCircle(point, _m_width * 1.5f, _m_color);

    }

    return;

  }

  private Vector2[] _m_aPositions;

  private Color _m_color;

  private float _m_width;

}