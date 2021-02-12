using Godot;
using System.Collections.Generic;

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

  public List<KinematicActor>
  GetVisibleBodies()
  {

    Godot.Collections.Array aBodies = _m_area.GetOverlappingBodies();

    int size = aBodies.Count;
    int index = 0;

    List<KinematicActor> aVisibleBodies = new List<KinematicActor>();

    while(index < size)
    {

      PhysicsBody physicsBody = aBodies[index] as PhysicsBody;

      // Check if this body is a kinematic actor.
      if(physicsBody is KinematicActor kActor)
      {

        // Exclude parent        
        if(!kActor.Equals(_m_node))
        {

          if (IsVisible(kActor))
          {

            aVisibleBodies.Add(kActor);

          }

        }

      }

      ++index;

    }

    return aVisibleBodies;

  }

  /// <summary>
  /// Set the depth of the vision.
  /// </summary>
  /// <param name="_radius"></param>
  public void
  SetVisionRadius(float _radius)
  {

    // Save radius.
    _m_radius = _radius;

    // Set area scale.
    _m_area.Scale = new Vector3(_radius, _m_area.Scale.y, _radius);

    return;

  }

  /// <summary>
  /// Check if the position of the body is visible to the tank range of view.
  /// </summary>
  /// <param name="_body"></param>
  /// <returns></returns>
  public bool
  IsVisible(PhysicsBody _body)
  {

    Vector3 toBody = _body.Transform.origin - _m_node.Transform.origin;

    float angle = _m_physics.DIRECTION.AngleTo(toBody);

    return angle < _m_openingAngle * 0.5f;

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
