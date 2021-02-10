using Godot;

public class DCMD_DrawCone
  : DCMD
{

  public DCMD_DrawCone(DebugManager _debugManager,
                       Color _color,
                       float _width,
                       Vector3 _position,
                       Vector3 _direction,
                       Vector3 _up,
                       float _radius,
                       float _openingAngle)
  {

    _m_color = _color;

    _m_width = _width;

    int angleSteps = Mathf.FloorToInt(_openingAngle / _ANGLE_STEP);

    int points = 4 + angleSteps;

    _m_aPoints = new Vector2[points];

    // First point
    _m_aPoints[0] = _debugManager.DEBUG_CAMERA.UnprojectPosition(_position);

    // Second point
    Vector3 toPoint = _direction.Rotated(_up, _openingAngle * 0.5f);
    toPoint *= _radius;

    _m_aPoints[1] = _debugManager.DEBUG_CAMERA.UnprojectPosition(_position +
                                                              toPoint);

    // Iterate
    for(int index = 0; index < angleSteps; ++index)
    {

      toPoint = toPoint.Rotated(_up, -_ANGLE_STEP);

      _m_aPoints[2 + index] 
        = _debugManager.DEBUG_CAMERA.UnprojectPosition(_position + toPoint);

    }

    // Penultimate point.
    toPoint = _direction.Rotated(_up, -_openingAngle * 0.5f);
    toPoint *= _radius;

    _m_aPoints[points - 2] 
      = _debugManager.DEBUG_CAMERA.UnprojectPosition(_position + toPoint);

    // Last point.
    _m_aPoints[points - 1] = _m_aPoints[0];

    return;

  }

  public override void 
  Exec(Control _canvas)
  {

    _canvas.DrawPolyline(_m_aPoints, _m_color, _m_width);

    return;

  }

  public float _m_width;

  public Color _m_color;

  public Vector2[] _m_aPoints;

  private static float _ANGLE_STEP = 0.5f;
}
