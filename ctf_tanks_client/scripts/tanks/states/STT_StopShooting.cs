using Godot;

public class STT_StopShooting
  : FSM_State<BehaviorNode, Actor<KinematicBody>>
{

  public STT_StopShooting()
    : base()
  {   

    _m_aTransitions.Add(new Trans_EnemyInSight());

    return;

  }

  public override NODE_STATUS 
  Update(Actor<KinematicBody> _arg)
  {

    return NODE_STATUS.kSuccess;
  
  }

  public override STATE_ID 
  GetID()
  {

    return STATE_ID.kStopShooting;
    
  }

}
