using Godot;
using System.Collections.Generic;

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

    _m_v3SteerForce = new Vector3();

    return;

  }

  public override void 
  OnInit(Actor<KinematicBody> _actor)
  {

    return;

  }

  public override NODE_STATUS 
  Update(Actor<KinematicBody> _actor)
  {

    BItem_Path pathItem = _actor
                          .m_blackboard
                          .GetItem<BItem_Path>(BLACKBOARD_ITEM.kPath);

    ActiveItemVector<CTF.PathNode> path = pathItem.m_vectorPathNode;

    // Check failure conditions.
    if(path == null)
    {

      // No path at all.
      return NODE_STATUS.kFailure;

    }
    else if(path.SIZE == 0)
    {

      // Invalid path.
      return NODE_STATUS.kFailure;

    }
    else if(path.ACTIVE == path.END )
    {

      // Index out of range. Return Failed.
      return NODE_STATUS.kFailure;

    }

    // Reset steer force.
    _m_v3SteerForce.x = 0;
    _m_v3SteerForce.y = 0;
    _m_v3SteerForce.z = 0;

    // Get actor position
    Vector3 position = _actor.GetNode().Transform.origin;

    // Get destination position.
    Vector3 destination = path.ACTIVE.m_item.position;
    
    // Fix destination height.
    destination.y = position.y;

    // Calculate vector to destination.
    SVector3.Substract(ref position, ref destination, ref _m_v3ToDestination);

    // Calculate distance to destination.
    float distanceToDestination = _m_v3ToDestination.Length();

    while(distanceToDestination < 5.0f)
    {

      // Next node.
      path.Next();

      if(path.ACTIVE != path.END)
      {

        // Get new destination.
        destination = path.ACTIVE.m_item.position;

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
        return NODE_STATUS.kSuccess;

      }

    }

    // Get Physics Tank component
    CmpTankPhysics physics 
      = _actor.GetComponent<CmpTankPhysics>(COMPONENT_ID.kTankPhysics);

    ////////////////////////////////////////////
    // Seek to destination force

    // Calculate Seek Force
    /*
    _m_v3SteerForce += SteerForce.Seek(physics.VELOCITY,
                                        physics.ENGINE_POWER,
                                        destination,
                                        physics.POSITION,
                                        100.0f);
    */

    _m_v3SteerForce += SteerForce.Arrive(physics.VELOCITY,
                                        destination,
                                        physics.POSITION,
                                        30.0f,
                                        100.0f);

    ////////////////////////////////////////////
    // Obstacle avoidance force.
    _m_v3SteerForce += _GetCollisionAvoidanceForce(_actor,
                                                   physics,
                                                   100.0f);

    // Calculate desire velocity.

    _m_v3DesireVelocity = _m_v3SteerForce + physics.VELOCITY;

    _m_v3DesireVelocity = SVector3.MaxLengthLimit(ref _m_v3DesireVelocity, 
                                                  physics.ENGINE_POWER);

    // Debug desire velocity.

    MasterManager master = MasterManager.GetInstance();

    master.DEBUG_MANAGER.DrawLine
    (
      _actor.GetNode().Transform.origin,
      _actor.GetNode().Transform.origin + _m_v3DesireVelocity,
      new Color(1, 1, 0),
      2
    );    

    // Update engine power

    _UpdateEnginePower(_actor, physics, _m_v3SteerForce);

    // Update reverse power

    //_UpdateReversePower(_actor, physics, _m_v3DesireVelocity);

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

    itemAccStrength.fValue = 1.0f;

    return;

  }

  private void
  _UpdateReversePower
  (
    Actor<KinematicBody> _actor,
    CmpTankPhysics _physics,
    Vector3 _desireVelocity
  )
  {

    float actualSpeed = _physics.VELOCITY.Length();
    float desireSpeed = _desireVelocity.Length();

    float reverseStrength = 0;

    if(actualSpeed > 0.001f)
    {

      reverseStrength = Mathf.Clamp(1.0f - (desireSpeed / actualSpeed), 0.0f, 1.0f);

    }

    BItem itemReverseStrength =
     _actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kReverse_Strength);

    itemReverseStrength.fValue = reverseStrength;

    return;

  }

  /// <summary>
  /// 
  /// </summary>
  private Vector3
  _GetCollisionAvoidanceForce
  (
    Actor<KinematicBody> _actor, 
    CmpTankPhysics _physics,
    float _force
  )
  {

    Vector3 vAvoidanceForce = new Vector3();

    if (_actor.HasComponent(COMPONENT_ID.kContactSensors))
    {

      CmpContactSensors contactSensor
      = _actor.GetComponent<CmpContactSensors>(COMPONENT_ID.kContactSensors);

      List<Vector3> aCollidersPosition = contactSensor.GetCollidersPosition();

      foreach(Vector3 position in aCollidersPosition)
      {

        Vector3 vToTarget = position - _physics.POSITION;

        Vector3 vProj = _physics.DIRECTION * (vToTarget.Dot(_physics.DIRECTION));

        vAvoidanceForce += vToTarget.DirectionTo(vProj) * _force;

      }

    }

    return vAvoidanceForce;

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

  /**********************************************/
  /* Physics                                    */
  /**********************************************/

  /// <summary>
  /// Desire velocity vector.
  /// </summary>
  private Vector3 _m_v3DesireVelocity;

  /// <summary>
  /// Vector from tank position to destination.
  /// </summary>
  private Vector3 _m_v3ToDestination;

  /// <summary>
  /// 
  /// </summary>
  private Vector3 _m_v3SteerForce;

}