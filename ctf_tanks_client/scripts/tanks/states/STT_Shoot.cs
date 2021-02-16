using Godot;

public class STT_Shoot
: FSM_State<BehaviorNode, Actor<KinematicBody>>
{

  public STT_Shoot()
  : base()
  {

    _m_aTransitions.Add(new Trans_EnemyOutOfSight());

    return;

  }

  public override void 
  OnEnter(Actor<KinematicBody> _actor)
  {

    BItem shootSignal =
      _actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kShootSignal);

    shootSignal.iValue = 1;

    return;

  }

  public override NODE_STATUS
  Update(Actor<KinematicBody> _actor)
  { 

    return NODE_STATUS.kRunning;

  }

  public override void 
  OnExit(Actor<KinematicBody> _actor)
  {

    BItem shootSignal =
      _actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kShootSignal);

    shootSignal.iValue = 0;

    return;

  }

  public override STATE_ID
  GetID()
  {

    return STATE_ID.kShoot;

  }

}
