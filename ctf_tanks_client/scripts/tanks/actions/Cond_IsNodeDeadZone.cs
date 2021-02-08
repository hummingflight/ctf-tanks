﻿using Godot;

public class Cond_IsNodeDeadZone
  : BehaviorNode
{

  public override NODE_STATUS 
  Update(Actor<KinematicBody> _actor)
  {

    // Get path node position.
    BItem_Path item_Path
    = _actor.m_blackboard.GetItem<BItem_Path>(BLACKBOARD_ITEM.kPath);

    CTF.PathNode pathNode = item_Path.m_vectorPathNode.ACTIVE.m_item;

    // Get tank physics
    CmpTankPhysics physics 
      = _actor.GetComponent<CmpTankPhysics>(COMPONENT_ID.kTankPhysics);

    // Check if path node is in the Tank dead zone.
    if(pathNode.IsInSteeringDeadZone(physics, physics.MAX_STEERING_ANGLE_OPENING * 0.5f))
    {

      return NODE_STATUS.kSucess;

    }

    return NODE_STATUS.kFailure;

  }

}
