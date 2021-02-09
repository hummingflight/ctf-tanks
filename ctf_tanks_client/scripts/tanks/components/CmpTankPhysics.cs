using Godot;

/// <summary>
/// Manage the group of motion forces that affects the tank velocity and position.
/// </summary>
public class CmpTankPhysics
  : Component<KinematicBody>
{

  // Called when the node enters the scene tree for the first time.
  public override void
  _Ready()
  {

    // Initialize variables

    _m_v3Acceleration = Vector3.Zero;

    _m_v3Velocity = Vector3.Zero;

    _m_v3FrontWheelPosition = Vector3.Zero;

    _m_v3RearWheelPosition = Vector3.Zero;

    _m_v3GravityForce = Vector3.Zero;

    _m_steeringAngle = 0.0f;

    // Get Properties

    _m_frontRayCast = _m_node.GetNode<RayCast>("FrontRayCast");

    _m_rearRayCast = _m_node.GetNode<RayCast>("RearRayCast");

    return;

  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="delta"></param>
  public override void
  _PhysicsProcess(float delta)
  {

    // Update steer angle.
    _UpdateSteer();

    // Set acceleration to zero.

    _m_v3Acceleration.x = 0.0f;
    _m_v3Acceleration.y = 0.0f;
    _m_v3Acceleration.z = 0.0f;   

    // Floor forces.

    bool isOnFloor = _m_node.IsOnFloor();

    if (isOnFloor)
    {

      _UpdateEngineForce();

      _UpdateReverse();

      _UpdateFriction();

      _m_v3Acceleration.y += _m_gravity;

    }

    // Wind Force.

    _UpdateDragForce();

    // Gravity.

    _UpdateGravity();

    // Update velocity and position.

    _UpdateVelocity(delta);

    if(isOnFloor)
    {

      _UpdateTankSteering(delta);

    }

    _UpdatePosition(delta);

    // Ramp
    /*

    if (_m_frontRayCast.IsColliding() || _m_rearRayCast.IsColliding())
    {

      Vector3 nf;
      Vector3 nr;

      if (_m_frontRayCast.IsColliding())
      {

        nf = _m_frontRayCast.GetCollisionNormal();

      }
      else
      {

        nf = Vector3.Up;

      }

      if (_m_rearRayCast.IsColliding())
      {

        nr = _m_rearRayCast.GetCollisionNormal();

      }
      else
      {

        nr = Vector3.Up;

      }


      Vector3 n = ((nr + nf) * 0.5f);

      if (n.Length() == 0.0f)
      {

        n = Vector3.Up;

      }
      else
      {

        n = n.Normalized();

      }

      Transform xForm = _AlignWithY(_m_node.GlobalTransform, n);

      _m_node.GlobalTransform = _m_node.GlobalTransform.InterpolateWith(xForm, 0.1f);

    }*/

    return;

  }

  public override COMPONENT_ID 
  GetID()
  {
    return COMPONENT_ID.kTankPhysics;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="_delta"></param>
  private void
  _UpdateTankSteering(float _delta)
  {

    // Get Wheels position.

    Vector3 direction = -_m_node.Transform.basis.z;

    float halfWheelBase = _m_wheelBase * 0.5f;

    _m_v3RearWheelPosition = _m_node.Transform.origin - direction * halfWheelBase;
    _m_v3FrontWheelPosition = _m_node.Transform.origin + direction * halfWheelBase;

    _m_v3RearWheelPosition += _m_v3Velocity * _delta;
    _m_v3FrontWheelPosition += _m_v3Velocity.Rotated
    (
      _m_node.Transform.basis.y.Normalized(), _m_steeringAngle
    )
    * _delta;

    // Calculate the new direction

    direction = _m_v3RearWheelPosition.DirectionTo(_m_v3FrontWheelPosition);

    // Calculate the new velocity

    if (direction.Dot(_m_v3Velocity) > 0)
    {

      float velocityMg = _m_v3Velocity.Length();

      _m_v3Velocity.x = direction.x * velocityMg;
      _m_v3Velocity.y = direction.y * velocityMg;
      _m_v3Velocity.z = direction.z * velocityMg;

    }
    else
    {

      float velocityMg = -_m_v3Velocity.Length();

      _m_v3Velocity.x = direction.x * velocityMg;
      _m_v3Velocity.y = direction.y * velocityMg;
      _m_v3Velocity.z = direction.z * velocityMg;

    }

    // Rotate

    _m_node.LookAt
    (
      _m_node.Transform.origin + direction,
      _m_node.Transform.basis.y
    );

    return;

  }

  /// <summary>
  /// 
  /// </summary>
  private void
  _UpdateSteer()
  {

    BItem steerItem = _m_actor.m_blackboard.GetItem<BItem>
      (
        BLACKBOARD_ITEM.kTank_Steering
      );

    _m_steeringAngle = steerItem.fValue * _m_maxSteeringAngleOpening * 0.5f;

    return;

  }

  /// <summary>
  /// Update position of the tank. The position is updated according to the 
  /// front wheel steering.
  /// </summary>
  /// <param name="_delta"></param>
  private void
  _UpdatePosition(float _delta)
  {   

    // Move Tank

    _m_v3Velocity = _m_node.MoveAndSlideWithSnap
    (
      _m_v3Velocity,
      -_m_node.Transform.basis.y,
      Vector3.Up,
      true
    );

    /*
    _m_v3Velocity = _m_node.MoveAndSlide
    (
      _m_v3Velocity,
      _m_node.Transform.basis.y
    );
    */
    return;

  }

  /// <summary>
  /// Calculates the steering of the car.
  /// </summary>
  /// <param name="_delta">delta time.</param>
  private void
  _UpdateVelocity(float _delta)
  {

    _m_v3Velocity += _m_v3Acceleration * _delta;    

    return;

  }

  /// <summary>
  /// Add engine force to the tank's acceleration.
  /// </summary>
  private void
  _UpdateEngineForce()
  {

    // Get acceleration strength from blackboard.
    BItem accelerationStrength = _m_actor.m_blackboard.GetItem<BItem>
      (
        BLACKBOARD_ITEM.kAcceleration_Strength
      );

    // Set acceleration vector.
    _m_v3Acceleration -= _m_node.Transform.basis.z *
                       _m_enginePower *
                       accelerationStrength.fValue;

    return;

  }

  /// <summary>
  /// Add reverse force to the tank's acceleration.
  /// </summary>
  private void
  _UpdateReverse()
  {

    // Get acceleration strength from blackboard.
    BItem reverseStrength = _m_actor.m_blackboard.GetItem<BItem>
      (
        BLACKBOARD_ITEM.kReverse_Strength
      );

    // Add reverse force to the acceleration.
    _m_v3Acceleration += _m_node.Transform.basis.z 
                        * reverseStrength.fValue 
                        * _m_reversePower;

    return;

  }

  /// <summary>
  /// Add gravity force to the tank's acceleration.
  /// </summary>
  private void
  _UpdateGravity()
  {

    _m_v3GravityForce.y = -_m_gravity;
    _m_v3Acceleration += _m_v3GravityForce;

    return;

  }

  /// <summary>
  /// Add friction force to the tank's acceleration.
  /// </summary>
  private void
  _UpdateFriction()
  {

    _m_v3Acceleration.x -= _m_v3Velocity.x * _m_friction;
    _m_v3Acceleration.y -= _m_v3Velocity.y * _m_friction;
    _m_v3Acceleration.z -= _m_v3Velocity.z * _m_friction;

    return;

  }

  /// <summary>
  /// Add Drag to the tank's acceleration.
  /// </summary>
  private void
  _UpdateDragForce()
  {

    float speed = _m_v3Velocity.Length();
    float dragMultiplier = speed * _m_drag;

    _m_v3Acceleration.x -= _m_v3Velocity.x * dragMultiplier;
    _m_v3Acceleration.y -= _m_v3Velocity.y * dragMultiplier;
    _m_v3Acceleration.z -= _m_v3Velocity.z * dragMultiplier;

    return;

  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="_transform"></param>
  /// <param name="_AxisY"></param>
  /// <returns></returns>
  private Transform
  _AlignWithY(Transform _transform, Vector3 _AxisY)
  {

    _transform.basis.y = _AxisY;
    _transform.basis.x = -_transform.basis.z.Cross(_AxisY);
    _transform.basis = _transform.basis.Orthonormalized();

    return _transform;

  }

  public Vector3
  POSITION
  {
    get
    {
      return _m_node.Transform.origin;
    }
  }

  /// <summary>
  /// Get the velocity of the tank.
  /// </summary>
  public Vector3
  VELOCITY
  {
    get
    {
      return _m_v3Velocity;
    }
  }

  /// <summary>
  /// Get the direction of the tank.
  /// </summary>
  public Vector3
  DIRECTION
  {
    get
    {
      return -_m_node.Transform.basis.z;
    }
  }

  /// <summary>
  /// Get the Normal vector of the Direction vector.
  /// </summary>
  public Vector3
  NORMAL_DIRECTION
  {
    get
    {
      return _m_node.Transform.basis.x;
    }
  }

  /// <summary>
  /// Get the maximum engine power.
  /// </summary>
  public float
  ENGINE_POWER
  {
    get
    {
      return _m_enginePower;
    }
  }

  /// <summary>
  /// Get the maximum reverse power.
  /// </summary>
  public float
  REVERSE_POWER
  {
    get 
    {
      return _m_reversePower;
    }
  }

  /// <summary>
  /// Get the friction coefficient.
  /// </summary>
  public float
  FRICTION_COEFFICIENT
  {
    get
    {
      return _m_friction;
    }
  }

  /// <summary>
  /// Get the drag coefficient.
  /// </summary>
  public float
  DRAG_COEFFICIENT
  {
    get
    {
      return _m_drag;
    }
  }

  /// <summary>
  /// Get the actual steering angle of the wheels.
  /// </summary>
  public float
  STEERING_ANGLE
  {
    get
    {
      return _m_steeringAngle;
    }
  }

  /// <summary>
  /// Get the maximum steering angle opening of the wheels.
  /// </summary>
  public float
  MAX_STEERING_ANGLE_OPENING
  {
    get
    {
      return _m_maxSteeringAngleOpening;
    }
  }

  /// <summary>
  /// Rate of change of the tank position.
  /// </summary>
  protected Vector3 _m_v3Velocity;

  /// <summary>
  /// Rate of change of the tank velocity.
  /// </summary>
  protected Vector3 _m_v3Acceleration;

  /******************************************/
  /* Forces                                 */
  /******************************************/

  /// <summary>
  /// Gravity force magnitude.
  /// </summary>
  protected float _m_gravity = 20.0f;

  /// <summary>
  /// Engine force maximum magnitude.
  /// </summary>
  protected float _m_enginePower = 15.0f;

  /// <summary>
  /// Reverse force maximum magnitude.
  /// </summary>
  protected float _m_reversePower = 15.0f;

  /// <summary>
  /// Friction coefficient.
  /// </summary>
  protected float _m_friction = 0.5f;

  /// <summary>
  /// Drag coefficient.
  /// </summary>
  protected float _m_drag = 0.5f;

  /// <summary>
  /// Gravity force.
  /// </summary>
  protected Vector3 _m_v3GravityForce;

  /******************************************/
  /* Wheel Position                         */
  /******************************************/

  /// <summary>
  /// Distance between the front and rear wheel.
  /// </summary>
  protected float _m_wheelBase = 5.0f;

  /// <summary>
  /// Rear wheel position.
  /// </summary>
  protected Vector3 _m_v3RearWheelPosition;

  /// <summary>
  /// Front wheel position.
  /// </summary>
  protected Vector3 _m_v3FrontWheelPosition;

  /******************************************/
  /* Wheel Steering                         */
  /******************************************/

  /// <summary>
  /// Maximum steering angle opening.
  /// </summary>
  protected float _m_maxSteeringAngleOpening = 1.5f;

  /// <summary>
  /// Actual steering angle.
  /// </summary>
  protected float _m_steeringAngle;

  /// <summary>
  /// 
  /// </summary>
  protected RayCast _m_frontRayCast;

  /// <summary>
  /// 
  /// </summary>
  protected RayCast _m_rearRayCast;

}
