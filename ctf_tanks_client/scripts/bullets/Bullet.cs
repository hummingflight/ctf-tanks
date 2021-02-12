using Godot;

public class Bullet
: KinematicBody
{

  public override void 
  _PhysicsProcess(float delta)
  {

    // Rotation towards direction.
    LookAt(Transform.origin + _m_velocity, Transform.basis.y);

    // Move
    MoveAndSlide(_m_velocity, Transform.basis.y);

    return;
  
  }

  /// <summary>
  /// Enable this bullet.
  /// </summary>
  public void
  Enable(Vector3 _position,
         Vector3 _velocity)
  {

    if(!_m_isEnable)
    {

      // Set bullet position.
      
      Transform newTransform = Transform;
      newTransform.origin = _position;

      Transform = newTransform;

      // Set bullet velocity.
      _m_velocity = _velocity;

      // Enable.

      _m_isEnable = !_m_isEnable;

      SetProcess(_m_isEnable);
      SetPhysicsProcess(_m_isEnable);
      SetProcessInput(_m_isEnable);

      // Add this bullet to the disable list.
      _m_pool.AddToDisable(this);

    }

    return;
  }

  /// <summary>
  /// Disable this bullet.
  /// </summary>
  public void
  Disable()
  {

    if (_m_isEnable)
    {

      _m_isEnable = !_m_isEnable;

      SetProcess(_m_isEnable);
      SetPhysicsProcess(_m_isEnable);
      SetProcessInput(_m_isEnable);

    }

    return;
  }

  /// <summary>
  /// Set the bullet pool.
  /// </summary>
  /// <param name="_pool"></param>
  public void
  SetPool(BulletPool _pool)
  {

    _m_pool = _pool;
    return;

  }

  /// <summary>
  /// Get the bullet direction.
  /// </summary>
  public Vector3
  DIRECTION
  {
    get
    {
      return Transform.basis.z;
    }
  }

  /// <summary>
  /// Get the bullet velocity.
  /// </summary>
  public Vector3
  VELOCITY
  {
    get
    {
      return _m_velocity;
    }
  }

  /// <summary>
  /// Indicates if the bullet is enable or disable.
  /// </summary>
  public bool
  IS_ENABLE
  {
    get
    {
      return _m_isEnable;
    }
  }

  /// <summary>
  /// Bullet's position rate of change.
  /// </summary>
  protected Vector3 _m_velocity = Vector3.Zero;

  /// <summary>
  /// Indicates if the bullet is enable.
  /// </summary>
  protected bool _m_isEnable = true;

  /// <summary>
  /// Reference to the bullet pool.
  /// </summary>
  protected BulletPool _m_pool;

}