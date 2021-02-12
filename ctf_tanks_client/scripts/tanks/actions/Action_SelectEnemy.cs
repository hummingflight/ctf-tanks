using Godot;
using System.Collections.Generic;

public class Action_SelectEnemy
  : BehaviorNode
{

  public override NODE_STATUS 
  Update(Actor<KinematicBody> _actor)
  {

    CmpTankVision tankVision
     = _actor.GetComponent<CmpTankVision>(COMPONENT_ID.kTankVision);

    List<KinematicActor> visibleActors = tankVision.GetVisibleBodies();

    if(visibleActors.Count > 0)
    {     

      BItem_KinematicActor item =
      _actor.m_blackboard.GetItem<BItem_KinematicActor>(BLACKBOARD_ITEM.kEnemy);

      if(item.ACTOR == null)
      {

        // Get First Actor
        Actor<KinematicBody> actor = visibleActors[0].Actor;

        // Set Enemy.
        item.ACTOR = actor;

      }

      return NODE_STATUS.kSuccess;

    }

    // No visible actors.
    return NODE_STATUS.kFailure;
  
  }

}