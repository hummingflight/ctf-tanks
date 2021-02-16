using Godot;

public class CmpTurretController
: Component<KinematicBody>
{

  public CmpTurretController()
  {

    return;

  }

  public CmpTurretController(Spatial _turret)
  {

    SetTurretNode(_turret);    

    return;

  }  

  public override void 
  _PhysicsProcess(float _delta)
  {

    BItem item_turretRotation =
    _m_actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kTurret_Rotation);
  
    if(item_turretRotation.fValue != 0)
    {

      Vector3 up = _m_turretNode.GlobalTransform.basis.y.Normalized();

      _m_turretNode.RotateObjectLocal(up, 
                                      -_m_rotationSpeed 
                                      * _delta 
                                      * item_turretRotation.fValue);

    }

    return;

  }

  /// <summary>
  /// Check if the target position in the fire zone of the turret. 
  /// 
  /// This method check if the angle between the turret direction and the 
  /// direction to the target is in the fire opening angle.
  /// </summary>
  /// <param name="_position"></param>
  /// <returns></returns>
  public bool
  IsInFireZone(Vector3 _position)
  {

    // Turret position.
    Vector3 turretPosition = _m_turretNode.Transform.origin;

    // Fix target height.
    _position.y = turretPosition.y;

    // Vector to target.
    Vector3 toTarget = _position - _m_turretNode.Transform.origin;

    // Angle between the turret direction and the direction to the target.
    float a = DIRECTION.AngleTo(toTarget);

    return a < _m_fireOpeningAngle * 0.5f;

  }

  /// <summary>
  /// Fire bullet in the direction of the turret node.
  /// </summary>
  public void
  Fire()
  {

    return;
  
  }

  /// <summary>
  /// Set the turret node.
  /// </summary>
  /// <param name="_node">The turret node.</param>
  public void
  SetTurretNode(Spatial _node)
  {

    _m_turretNode = _node;

    SetSpawnPosition(_node.GetNode<Spatial>("spawn_point"));

    return;

  }

  /// <summary>
  /// Set the turret bullet spawn position.
  /// </summary>
  /// <param name="_node"></param>
  public void
  SetSpawnPosition(Spatial _node)
  {

    _m_bulletSpawnPosition = _node;
    return;

  }

  public override COMPONENT_ID 
  GetID()
  {

    return COMPONENT_ID.kTurretController;
  
  }

  /// <summary>
  /// Get the turret direction.
  /// </summary>
  public Vector3
  DIRECTION
  {
    get
    {
      return _m_turretNode.Transform.basis.z;
    }
  }

  public Transform
  GLOBAL_TRANSFORM
  {
    get
    {
      return _m_turretNode.GlobalTransform.Orthonormalized();
    }
  }

  public Vector3
  GLOBAL_DIRECTION
  {
    get
    {
      return _m_turretNode.GlobalTransform.basis.z;
    }
  }

  public Vector3
  POSITION
  {
    get
    {
      return _m_turretNode.Transform.origin;
    }
  }

  public Vector3
  GLOBAL_POSITION
  {
    get
    {
      return _m_turretNode.GlobalTransform.origin;
    }
  }

  /// <summary>
  /// Get the turret node.
  /// </summary>
  public Spatial
  TURRET_NODE
  {
    get
    {
      return _m_turretNode;
    }
  }

  /// <summary>
  /// Get the turret rotation speed ( radians per second ).
  /// </summary>
  public float
  ROTATION_SPEED
  {
    get
    {
      return _m_rotationSpeed;
    }
    set
    {
      _m_rotationSpeed = value;
    }
  }

  /// <summary>
  /// The accepted angle between the turret direction and the vector to the
  /// target to fire the weapon.
  /// </summary>
  public float
  FIRE_OPENING_ANGLE
  {
    get
    {
      return _m_fireOpeningAngle;
    }
  }

  /// <summary>
  /// Get the initial position of the bullet when it is spawned.
  /// </summary>
  public Transform
  BULLET_SPAWN_POSITION
  {
    get
    {
      return _m_bulletSpawnPosition.GlobalTransform.Orthonormalized();
    }
  }

  /// <summary>
  /// Reference to the turret node.
  /// </summary>
  protected Spatial _m_turretNode;

  /// <summary>
  /// The rotation speed of the turret ( radians per second ).
  /// </summary>
  protected float _m_rotationSpeed = 0.5f;

  /// <summary>
  /// The accepted angle between the turret direction and the vector to the
  /// target to fire the weapon.
  /// </summary>
  protected float _m_fireOpeningAngle = 0.1f;

  /// <summary>
  /// The position of the bullet when it is spawned.
  /// </summary>
  protected Spatial _m_bulletSpawnPosition;

}
