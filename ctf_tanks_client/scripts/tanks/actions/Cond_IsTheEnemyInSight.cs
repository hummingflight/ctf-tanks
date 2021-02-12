using Godot;
using System.Collections.Generic;

public class Cond_IsTheEnemyInSight
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

      return NODE_STATUS.kSuccess;     

    }
    
    return NODE_STATUS.kFailure;
  
  }

}
