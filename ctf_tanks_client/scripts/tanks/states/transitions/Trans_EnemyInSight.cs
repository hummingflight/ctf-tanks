using Godot;

public class Trans_EnemyInSight
: FSM_Transition<BehaviorNode, Actor<KinematicBody>>
{

  public override bool 
  IsValid(Actor<KinematicBody> _args)
  {

    BItem_KinematicActor item =
    _args.m_blackboard.GetItem<BItem_KinematicActor>(BLACKBOARD_ITEM.kEnemy);

    if(item.ACTOR != null)
    {

      return item.ACTOR.IS_ENABLE;

    }

    return false;
  }

  public override STATE_ID 
  GetNextState()
  {

    return STATE_ID.kTargetTheEnemy;
  
  }

}
