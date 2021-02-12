using Godot;

public class STT_Reload
: FSM_State<BehaviorNode, Actor<KinematicBody>>
{

  public override NODE_STATUS
  Update(Actor<KinematicBody> _arg)
  {

    // TODO;

    return NODE_STATUS.kFailure;

  }

  public override STATE_ID
  GetID()
  {

    return STATE_ID.kReload;

  }

}