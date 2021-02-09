using Godot;

public class Action_ReverseAlignment
  : BehaviorNode
{

  public override NODE_STATUS 
  Update(Actor<KinematicBody> _actor)
  {

    BItem_Path pathItem = _actor
                         .m_blackboard
                         .GetItem<BItem_Path>(BLACKBOARD_ITEM.kPath);    

    ActiveItemVector<CTF.PathNode> path = pathItem.m_vectorPathNode;

    // Check failure conditions.
    if (path == null)
    {

      // No path at all.
      return NODE_STATUS.kFailure;

    }
    else if (path.SIZE == 0)
    {

      // Invalid path.
      return NODE_STATUS.kFailure;

    }
    else if (path.ACTIVE == path.END)
    {

      // Index out of range. Return Failed.
      return NODE_STATUS.kFailure;

    }

    // Get vector item.
    ItemVectorNode<CTF.PathNode> node = path.ACTIVE;

    if (node == path.END || node == path.BEGIN || node == null)
    {

      // No Active node
      return NODE_STATUS.kFailure;

    }

    // Check if node is in safe zone.

    CmpTankPhysics physics 
      = _actor.GetComponent<CmpTankPhysics>(COMPONENT_ID.kTankPhysics);

    CTF.PathNode pathNode = node.m_item;

    if (pathNode.IsInSteeringSafeZone(physics, physics.MAX_STEERING_ANGLE_OPENING * 0.25f))
    {

      return NODE_STATUS.kSuccess;

    }

    // Desire direction.

    Vector3 nodePosition = pathNode.position;

    Vector3 tankPosition = physics.POSITION;

    // Fix node position height.

    nodePosition.y = tankPosition.y;

    Vector3 desireDirection = tankPosition.DirectionTo(nodePosition);

    // Update steering.

    _UpdateSteeringStrenght(_actor, physics, desireDirection);

    // Update

    BItem reverseStrength = _actor
                            .m_blackboard
                            .GetItem<BItem>(BLACKBOARD_ITEM.kReverse_Strength);

    reverseStrength.fValue = 1.0f;

    return NODE_STATUS.kRunning;

  }

  public override void OnTerminate
  (
    Actor<KinematicBody> _actor, 
    NODE_STATUS status
  )
  {

    GD.Print("Reverse Alignment terminated.");

    BItem reverseStrength = _actor
                            .m_blackboard
                            .GetItem<BItem>(BLACKBOARD_ITEM.kReverse_Strength);

    reverseStrength.fValue = 0.0f;

    BItem accStrength = _actor
                       .m_blackboard
                       .GetItem<BItem>(BLACKBOARD_ITEM.kAcceleration_Strength);

    accStrength.fValue = 0.0f;

    BItem steerStrength = _actor
                          .m_blackboard
                          .GetItem<BItem>(BLACKBOARD_ITEM.kTank_Steering);

    steerStrength.fValue = 0.0f;

    return;

  }

  /**********************************************/
  /* Private                                    */
  /**********************************************/

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

    itemWheelSteering.fValue = -steeringStrength; // Multiplied by -1 for a reverse alignment.

    return;

  }

}