using Godot;

public class BTBCMD_Action
: BTBCMD
{

  public BTBCMD_Action(BT_ACTION _action)
  {

    _m_action = _action;

    return;

  }

  public BehaviorNode
  Exec(BTBuilder _BTBuilder)
  {

    switch(_m_action)
    {

      case BT_ACTION.kCourse_Towards_Enemy_Base:
        return new Action_CourseTowardsEnemyBase();

      case BT_ACTION.kGet_Path_To_Destination:
        return new Action_GetPathToDestination();

      case BT_ACTION.kMove_To:
        return new Action_MoveTo();

      case BT_ACTION.kIs_Path_Node_In_DeadZone:
        return new Cond_IsNodeDeadZone();

      case BT_ACTION.kReverse_Alignment:
        return new Action_ReverseAlignment();

      case BT_ACTION.kHas_Active_Path:
        return new Cond_HasActivePath();

      case BT_ACTION.kStop:
        return new Action_Stop();

      default:
        GD.PrintErr("Action: " + _m_action.ToString() + "is not implemented.");
        return null;

    }

  }

  public BTBCMD_KEY
  GetKey()
  {

    return BTBCMD_KEY.kAction;

  }

  private BT_ACTION _m_action;

}