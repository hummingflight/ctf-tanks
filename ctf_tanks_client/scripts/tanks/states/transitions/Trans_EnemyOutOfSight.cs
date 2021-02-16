using Godot;

public class Trans_EnemyOutOfSight
: FSM_Transition<BehaviorNode, Actor<KinematicBody>>
{

  public override bool
  IsValid(Actor<KinematicBody> _actor)
  {

    CmpTankVision tankVision =
      _actor.GetComponent<CmpTankVision>(COMPONENT_ID.kTankVision);

    BItem_KinematicActor item_actor =
    _actor.m_blackboard.GetItem<BItem_KinematicActor>(BLACKBOARD_ITEM.kEnemy);

    Actor<KinematicBody> enemy = item_actor.ACTOR;

    // Check if enemy exists.
    if(enemy == null)
    {

      return true;

    }

    // Check if actor is enable.
    if(!enemy.IS_ENABLE)
    {

      return true;

    }

    // Check if enemy is visible.
    if(!tankVision.IsVisible(enemy.GetNode()))
    {
      
      return true;

    }

    return false;

  }

  public override void 
  OnTransition(Actor<KinematicBody> _actor)
  {

    // Set enemy to null.

    BItem_KinematicActor item_actor =
    _actor.m_blackboard.GetItem<BItem_KinematicActor>(BLACKBOARD_ITEM.kEnemy);

    item_actor.ACTOR = null;

    return;
  
  }

  public override STATE_ID
  GetNextState()
  {

    return STATE_ID.kStopShooting;

  }

}