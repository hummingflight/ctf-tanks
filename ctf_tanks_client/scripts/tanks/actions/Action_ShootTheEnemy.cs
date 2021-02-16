using Godot;

public class Action_ShootTheEnemy
: BehaviorNode
{

  public Action_ShootTheEnemy()
  {

    // Create FSM
    _m_fsm = new FSM<BehaviorNode, Actor<KinematicBody>>(this);

    // Add States
    _m_fsm.Add(new STT_Shoot());
    _m_fsm.Add(new STT_StopShooting());
    _m_fsm.Add(new STT_TargetTheEnemy());

    return;

  }

  public override void 
  OnInit(Actor<KinematicBody> _actor)
  {

    if(_m_fsm.ACTIVE_STATE.GetID() != STATE_ID.kStopShooting)
    {

      _m_fsm.SetActive(STATE_ID.kStopShooting, _actor);

    }
    
    return;

  }

  public override NODE_STATUS 
  Update(Actor<KinematicBody> _actor)
  {    

    // Update active state.
    return _m_fsm.Update(_actor);

  }

  public override void 
  OnTerminate(Actor<KinematicBody> _actor, NODE_STATUS status)
  {

    if (_m_fsm.ACTIVE_STATE.GetID() != STATE_ID.kStopShooting)
    {

      _m_fsm.SetActive(STATE_ID.kStopShooting, _actor);

    }

    return;

  }

  /// <summary>
  /// Action Finite State Machine.
  /// </summary>
  private FSM<BehaviorNode, Actor<KinematicBody>> _m_fsm;

}