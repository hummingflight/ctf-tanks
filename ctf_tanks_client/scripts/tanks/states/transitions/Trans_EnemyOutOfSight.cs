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

    if(item_actor.ACTOR == null)
    {

      return true;

    }

    if(!tankVision.IsVisible(item_actor.ACTOR.GetNode()))
    {

      item_actor.ACTOR = null;
      return true;

    }

    return false;

  }

  public override STATE_ID
  GetNextState()
  {

    return STATE_ID.kStopShooting;

  }

}