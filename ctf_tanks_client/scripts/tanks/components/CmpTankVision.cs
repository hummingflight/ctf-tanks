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

    _m_visibleActors = new List<KinematicActor>();

    return;
  
  }


  public override void 
  _PhysicsProcess(float _delta)
  {

    // Clear previous list.
    _m_visibleActors.Clear();

    // Get overlapping bodies.
    Godot.Collections.Array aBodies = _m_area.GetOverlappingBodies();

    int size = aBodies.Count;
    int index = 0;

    // Iterate over overlapping bodies.
    while (index < size)
    {

      PhysicsBody physicsBody = aBodies[index] as PhysicsBody;

      // Check if this body is a kinematic actor.
      if (physicsBody is KinematicActor kActor)
      {

        // Exclude parent        
        if (!kActor.Equals(_m_node))
        {

          // Check if actor is enable.
          if (kActor.Actor.IS_ENABLE)
          {            

            // Check if actor is visible for the tank.
            if(IsVisible(kActor))
            {

              _m_visibleActors.Add(kActor);
            
            }           

          }

        }

      }

      ++index;

    }

    return;
  
  }

  public List<KinematicActor>
  GetVisibleBodies()
  {

    return _m_visibleActors;

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
  /// Because this method need the physic space state, it must be called during
  /// the physics process.
  /// </summary>
  /// <param name="_body"></param>
  /// <returns></returns>
  public bool
  IsVisible(PhysicsBody _body)
  {

    Vector3 selfPosition = _m_node.GlobalTransform.origin;

    selfPosition += _m_node.GlobalTransform.basis.y.Normalized() * 3.0f;

    Vector3 toBody = _body.GlobalTransform.origin - selfPosition;    
    
    float angle = _m_physics.DIRECTION.AngleTo(toBody);

    // Check if body is in the vision area.
    if(angle < _m_openingAngle * 0.5f)
    {

      // Get the physics space state.
      PhysicsDirectSpaceState spaceState = _m_node.GetWorld().DirectSpaceState;


      // Vector to agent with the vision length.
      toBody = toBody.Normalized() * _m_radius;

      // Check if it is behind another object.
      Godot.Collections.Dictionary result = spaceState.IntersectRay
      (
        selfPosition,
        selfPosition + toBody,
        new Godot.Collections.Array() { _m_node }
      );

     if(result.Count > 0)
     {

        PhysicsBody collider = result["collider"] as PhysicsBody;

        if(collider != null)
        {

          // Check if the first collider is the same as the body. If not, something
          // is between the position of the tank an the body.
          return collider.GetInstanceId() == _body.GetInstanceId();

        }

     }

    }

    return false;

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
  protected float _m_openingAngle = 2.14159f;

  /// <summary>
  /// Vision radius.
  /// </summary>
  protected float _m_radius = 25.0f;

  /// <summary>
  /// Reference to the tank physics.
  /// </summary>
  protected CmpTankPhysics _m_physics;

  /// <summary>
  /// List of visible actors.
  /// </summary>
  protected List<KinematicActor> _m_visibleActors;

}
