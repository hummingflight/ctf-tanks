using Godot;

public class Action_MoveTo
: BehaviorNode
{

  /**********************************************/
  /* Public                                     */
  /**********************************************/

  public Action_MoveTo()
  {

    _m_v3DesireVelocity = new Vector3();

    _m_v3ToDestination = new Vector3();

    return;

  }

  public override void 
  OnInit(Actor<KinematicBody> _actor)
  {

    _m_nodeIndex = 0;

    return;

  }

  public override NODE_STATUS 
  Update(Actor<KinematicBody> _actor)
  {

    BItem_Path pathItem = _actor
                          .m_blackboard
                          .GetItem<BItem_Path>(BLACKBOARD_ITEM.kPath);

    // Check failure conditions.
    if(pathItem.m_path == null)
    {

      // No path at all.
      return NODE_STATUS.kFailure;

    }
    else if(pathItem.m_path.Length == 0)
    {

      // Invalid path.
      return NODE_STATUS.kFailure;

    }
    else if(!IsSafeIndex(_m_nodeIndex, pathItem.m_path))
    {

      // Index out of range. Return Failed.
      return NODE_STATUS.kFailure;

    }    

    // Get actor position
    Vector3 position = _actor.GetNode().Transform.origin;

    // Get destination position.
    Vector3 destination = pathItem.m_path[_m_nodeIndex];
    
    // Fix destination height.
    destination.y = position.y;

    // Calculate vector to destination.
    SVector3.Substract(ref position, ref destination, ref _m_v3ToDestination);

    // Calculate distance to destination.
    float distanceToDestination = _m_v3ToDestination.Length();

    while(distanceToDestination < 5.0f)
    {

      // Next node.
      if(IsSafeIndex(++_m_nodeIndex, pathItem.m_path))
      {

        GD.Print("Active Node Index: " + _m_nodeIndex);

        // Get new destination.
        destination = pathItem.m_path[_m_nodeIndex];

        // Fix destination height.
        destination.y = position.y;

        // Calculate vector to destination.
        SVector3.Substract(ref position, ref destination, ref _m_v3ToDestination);

        // Calculate distance to destination.
        distanceToDestination = _m_v3ToDestination.Length();

      }
      else
      {

        // Last position reached. Return success.
        return NODE_STATUS.kSucess;

      }

    }

    // Get Physics Tank component
    CmpTankPhysics physics 
      = _actor.GetComponent<CmpTankPhysics>(COMPONENT_ID.kTankPhysics);

    // Calculate Seek Force
    Vector3 seekForce = SteerForce.Seek(physics.VELOCITY,
                                        physics.ENGINE_POWER,
                                        destination,
                                        physics.POSITION,
                                        100.0f);

    Vector3 steerForce = new Vector3(seekForce);

    // Calculate desire velocity.

    _m_v3DesireVelocity = steerForce + physics.VELOCITY;

    _m_v3DesireVelocity = SVector3.MaxLengthLimit(ref _m_v3DesireVelocity, 
                                                  physics.ENGINE_POWER);

    // Debug desire velocity.

    MasterManager master = MasterManager.GetInstance();

    master.DEBUG_MANAGER.DrawLine
    (
      _actor.GetNode().Transform.origin,
      _actor.GetNode().Transform.origin + _m_v3DesireVelocity * 0.5f,
      new Color(1, 1, 0),
      2
    );

    // Update engine power

    _UpdateEnginePower(_actor, physics, steerForce);

    // Update steering strength and direction.

    _UpdateSteeringStrenght(_actor, physics, _m_v3DesireVelocity.Normalized());

    return NODE_STATUS.kRunning;

  }

  public override void 
  OnTerminate(Actor<KinematicBody> _actor, NODE_STATUS status)
  {

    return;
  
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="_m_nodeIndex"></param>
  /// <param name="_path"></param>
  /// <returns></returns>
  public bool
  IsSafeIndex(uint _m_nodeIndex, Vector3[] _path)
  {

    return _m_nodeIndex < _path.Length;

  }

  /**********************************************/
  /* Private                                    */
  /**********************************************/  

  /// <summary>
  /// 
  /// </summary>
  /// <param name="_actor"></param>
  /// <param name="_physics"></param>
  /// <param name="_steerForce"></param>
  private void
  _UpdateEnginePower
  (
    Actor<KinematicBody> _actor,
    CmpTankPhysics _physics,
    Vector3 _steerForce
  )
  {

    float engineStrength = _steerForce.Length() / _physics.ENGINE_POWER;

    BItem itemAccStrength =
      _actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kAcceleration_Strength);

    itemAccStrength.fValue = engineStrength;

    return;

  }

  /// <summary>
  /// 
  /// </summary>
  private void
  _UpdateCollisionAvoidance
  ()
  {

    return;

  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="_actor"></param>
  /// <param name="_physics"></param>
  /// <param name="_desireDirection"></param>
  private void
  _UpdateSteeringStrenght
  (
    Actor<KinematicBody> _actor,
    CmpTankPhysics _physics, 
    Vector3 _desireDirection
  )
  {

    Vector3 direction = _physics.DIRECTION;
    Vector3 nZ = _physics.NORMAL_DIRECTION;

    // Get vector angles.

    float a = _physics.MAX_STEERING_ANGLE_OPENING * 0.5f;
    float b = _desireDirection.AngleTo(direction);

    // Steering strength

    float steeringStrength = Mathf.Min(b / a, 1.0f);    

    // Steering direction

    steeringStrength *= -Mathf.Sign(nZ.Dot(_desireDirection));

    // Apply steering strength.

    BItem itemWheelSteering =
    _actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kTank_Steering);

    itemWheelSteering.fValue = steeringStrength;

    return;

  }

  private uint _m_nodeIndex;

  /******************************************/
  /*                                        */
  /******************************************/

  /// <summary>
  /// Desire velocity vector.
  /// </summary>
  private Vector3 _m_v3DesireVelocity;

  /// <summary>
  /// Vector from tank position to destination.
  /// </summary>
  private Vector3 _m_v3ToDestination;

}