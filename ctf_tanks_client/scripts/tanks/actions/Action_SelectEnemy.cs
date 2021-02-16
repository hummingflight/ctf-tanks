using Godot;
using System.Collections.Generic;

public class Action_SelectEnemy
  : BehaviorNode
{

  public override NODE_STATUS 
  Update(Actor<KinematicBody> _actor)
  {

    BItem_KinematicActor item =
      _actor.m_blackboard.GetItem<BItem_KinematicActor>(BLACKBOARD_ITEM.kEnemy);

    // Check if an active enemy exists.
    if(item.ACTOR != null)
    {

      if(item.ACTOR.IS_ENABLE)
      {

        return NODE_STATUS.kSuccess;

      }

    }

    // Get visible actors.

    CmpTankVision tankVision
     = _actor.GetComponent<CmpTankVision>(COMPONENT_ID.kTankVision);

    List<KinematicActor> visibleActors = tankVision.GetVisibleBodies();

    if(visibleActors.Count > 0)
    {

      // Take the first actor.

      KinematicActor selected = visibleActors[0];

      item.ACTOR = selected.Actor;

      return NODE_STATUS.kSuccess;

    }

    // No visible actors.
    return NODE_STATUS.kFailure;
  
  }

  public KinematicActor
  SelectActor(List<KinematicActor> _aActor)
  {

    foreach(KinematicActor actor in _aActor)
    {

      if(actor.Actor.IS_ENABLE)
      {

        return actor;

      }

    }

    return null;

  }

}