using Godot;

public class RepeatNode
  : DecoratorNode
{

  public RepeatNode(BehaviorNode _child)
    : base(_child)
  {

    return;

  }

  public void
  SetCount(int _count)
  {

    _m_iLimit = _count;

    return;

  }

  public override void
  OnInit(Actor<KinematicBody> _actor)
  {

    _m_iCounter = 0;

    return;

  }

  public override NODE_STATUS
  Update(Actor<KinematicBody> _actor)
  {

    for(;;)
    {

      _m_child.Tick(_actor);

      NODE_STATUS status = _m_child.GetStatus();

      if(status == NODE_STATUS.kRunning)
      {

        break;

      } 
      else if (status == NODE_STATUS.kFailure)
      {

        return NODE_STATUS.kFailure;

      }
      else if (++_m_iCounter == _m_iLimit)
      {

        return NODE_STATUS.kSucess;

      }
      else
      {

        _m_child.Reset();

      }

    }

    return NODE_STATUS.kInvalid;

  }

  protected int _m_iLimit;

  protected int _m_iCounter;

}
