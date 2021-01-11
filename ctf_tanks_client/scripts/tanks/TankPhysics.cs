using Godot;
using System;

public class TankPhysics : KinematicBody
{
  // Declare member variables here. Examples:
  // private int a = 2;
  // private string b = "text";

  // Called when the node enters the scene tree for the first time.
  public override void
  _Ready()
  {

    // Initialize variables

    m_acceleration = Vector3.Zero;

    m_velocity = Vector3.Zero;

    m_frontWheelPosition = Vector3.Zero;

    m_rearWheelPosition = Vector3.Zero;

    m_frictionForce = Vector3.Zero;

    m_dragForce = Vector3.Zero;

    m_breakForce = Vector3.Zero;

    m_gravityForce = Vector3.Zero;

    m_steerAngle = 0.0f;

    // Get Properties

    _m_frontRayCast = GetNode<RayCast>("FrontRayCast");

    _m_rearRayCast = GetNode<RayCast>("RearRayCast");

    _m_torret = GetNode<Spatial>("TankRoot/Cartoon_TL_Base/Turret");

    return;

  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="delta"></param>
  public override void
  _PhysicsProcess(float delta)
  {   

    if(IsOnFloor())
    {

      // Update acceleration vector.

      _UpdateAcceleration(delta);

      // Update break force

      _UpdateBreak(delta);

      // Apply Acceleration and Break

      m_velocity += (m_acceleration + m_breakForce) * delta;

      // Update friction

      _UpdateFriction();

      // Update Drag

      _UpdateDragForce();

      // Apply Friction and Drag

      m_velocity += (m_dragForce + m_frictionForce) * delta;

      // Update steering vector.

      _UpdateVelocity(delta);

    }

    m_gravityForce.y = m_gravity * delta;

    m_velocity += m_gravityForce;

    // Move Tank

    m_velocity = MoveAndSlideWithSnap(m_velocity, -Transform.basis.y, Vector3.Up, true);

    // Ramp

    if(_m_frontRayCast.IsColliding() || _m_rearRayCast.IsColliding())
    {

      Vector3 nf;
      Vector3 nr;

      if(_m_frontRayCast.IsColliding())
      {

        nf = _m_frontRayCast.GetCollisionNormal();

      }
      else
      {

        nf = Vector3.Up;

      }

      if(_m_rearRayCast.IsColliding())
      {

        nr = _m_rearRayCast.GetCollisionNormal();

      }
      else
      {

        nr = Vector3.Up;

      }

      
      Vector3 n = ((nr + nf) * 0.5f);

      if(n.Length() == 0.0f)
      {

        n = Vector3.Up;

      }
      else
      {

        n = n.Normalized();

      }

      Transform xForm = _AlignWithY(GlobalTransform, n);

      GlobalTransform = GlobalTransform.InterpolateWith(xForm, 0.1f);

    }

    return;

  }

  public override void
  _Process(float _delta)
  {

    float steerValue = Input.GetActionStrength("steer_left")
                     - Input.GetActionStrength("steer_right");

    Steer(steerValue);

    return;

  }

  /// <summary>
  /// Steer the vehicle, with a value from [-1.0 , 1.0] proportional to the 
  /// steering limit.
  /// </summary>
  /// <param name="_value">value from [-1.0, 1.0]</param>
  public void    
  Steer(float _value)
  {

    m_steerAngle = _value * m_steeringLimit;   

    return;

  }


  [Export] float m_gravity = 20.0f;

  [Export] float m_wheelBase = 0.6f;

  [Export] float m_steeringLimit = 10.0f;

  [Export] float m_enginePower = 6.0f;

  [Export] float m_bracking = -9.0f;

  [Export] float m_friction = -2.0f;

  [Export] float m_drag = -2.0f;

  [Export] float m_maxSpeedReverse = 3.0f;

  [Export] float m_turretOpeningAngle = 1.57f;

  Vector3 m_acceleration;

  Vector3 m_breakForce;

  Vector3 m_frictionForce;

  Vector3 m_gravityForce;

  Vector3 m_dragForce;

  Vector3 m_velocity;

  Vector3 m_rearWheelPosition;

  Vector3 m_frontWheelPosition;

  float m_steerAngle;


  /// <summary>
  /// Calculates the steering of the car.
  /// </summary>
  /// <param name="_delta">delta time.</param>
  private void 
  _UpdateVelocity(float _delta)
  {

    // Get Wheels position.

    Vector3 direction = -Transform.basis.z;
    float halfWheelBase = m_wheelBase * 0.5f;

    m_rearWheelPosition = Transform.origin - direction * halfWheelBase;
    m_frontWheelPosition = Transform.origin + direction * halfWheelBase;

    // Move Wheels

    m_rearWheelPosition += m_velocity * _delta;
    m_frontWheelPosition += m_velocity.Rotated(Transform.basis.y.Normalized(), m_steerAngle) * _delta;

    // Calculate the new direction

    direction = m_rearWheelPosition.DirectionTo(m_frontWheelPosition);

    // Calculate the new velocity

    if(direction.Dot(m_velocity) > 0)
    {

      m_velocity = direction * m_velocity.Length();

    }
    else
    {

      m_velocity = direction * -m_velocity.Length();

    }

    

    // Rotate

    LookAt(Transform.origin + direction, Transform.basis.y);

    return;

  }

  private void
  _UpdateAcceleration(float _deltaTime)
  {

    float accMultiplier = Input.GetActionStrength("accelerate");

    m_acceleration = -Transform.basis.z * m_enginePower * accMultiplier;

    return;

  }

  private void
  _UpdateBreak(float _delta)
  {

    float breakMultiplier = Input.GetActionStrength("break");

    m_breakForce = -Transform.basis.z * breakMultiplier * m_bracking;

    return;

  }

  private void
  _UpdateFriction()
  {

    m_frictionForce.x = m_velocity.x * m_friction;
    m_frictionForce.y = m_velocity.y * m_friction;
    m_frictionForce.z = m_velocity.z * m_friction;

    return;

  }

  private void
  _UpdateDragForce()
  {

    float speed = m_velocity.Length();
    float dragMultiplier = speed * m_drag;

    m_dragForce.x = m_velocity.x * dragMultiplier;
    m_dragForce.y = m_velocity.y * dragMultiplier;
    m_dragForce.z = m_velocity.z * dragMultiplier;

    return;

  }

  

  private Transform
  _AlignWithY(Transform _transform, Vector3 _AxisY)
  {

    _transform.basis.y = _AxisY;
    _transform.basis.x = -_transform.basis.z.Cross(_AxisY);
    _transform.basis = _transform.basis.Orthonormalized();

    return _transform;

  }

  private RayCast _m_frontRayCast;

  private RayCast _m_rearRayCast;

  private Spatial _m_torret;

}
