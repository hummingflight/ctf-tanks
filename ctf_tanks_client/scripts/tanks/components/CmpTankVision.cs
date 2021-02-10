using Godot;

/// <summary>
/// Components needed:
/// 
/// - CmpTankPhysics
/// 
/// </summary>
public class CmpTankVision
  : Component<KinematicBody>
{

  public override void 
  _Ready()
  {

    // Create the Area node
    PackedScene scnResource = GD.Load<PackedScene>("res://prefabs/misc/CylinderArea.tscn");

    // Create 
    Area visionArea = scnResource.Instance() as Area;

    // Add area to parent.
    _m_node.AddChild(visionArea);

    // Save Area.
    _m_area = visionArea;

    // Scale area.
    SetVisionRadius(_m_radius);

    // Get Tank Physics.
    _m_physics = _m_actor.GetComponent<CmpTankPhysics>(COMPONENT_ID.kTankPhysics);

    return;
  
  }

  public void
  SetVisionRadius(float _radius)
  {

    // Save radius.
    _m_radius = _radius;

    // Set area scale.
    _m_area.Scale = new Vector3(_radius, _m_area.Scale.y, _radius);

    return;

  }

  public override COMPONENT_ID 
  GetID()
  {

    return COMPONENT_ID.kTankVision;

  }

  /// <summary>
  /// Get the vision radius of the tank.
  /// </summary>
  public float 
  VISION_RADIUS
  {
    get
    {
      return _m_radius;
    }
  }

  /// <summary>
  /// Get the vision opening angle of the tank.
  /// </summary>
  public float
  OPENING_ANGLE
  {
    get
    {
      return _m_openingAngle;
    }
  }

  /// <summary>
  /// Area that detect Collision nodes.
  /// </summary>
  public Area
  AREA
  {
    get
    {
      return _m_area;
    }
  }

  /// <summary>
  /// Area used to detect collision nodes in the tank radius.
  /// </summary>
  protected Area _m_area;

  /// <summary>
  /// Opening Angle of vision.
  /// </summary>
  protected float _m_openingAngle = 3.14159f;

  /// <summary>
  /// Vision radius.
  /// </summary>
  protected float _m_radius = 25.0f;

  /// <summary>
  /// Reference to the tank physics.
  /// </summary>
  protected CmpTankPhysics _m_physics;

}
