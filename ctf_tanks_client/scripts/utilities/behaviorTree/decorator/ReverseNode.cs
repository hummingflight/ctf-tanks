using Godot;

/// <summary>
/// This node reverse the "Failure" signal by "Success" signal, and vice versa.
/// Other type of signal will be keep the same.
/// </summary>
public class ReverseNode
: DecoratorNode
{

  public ReverseNode(BehaviorNode _child)
    : base(_child)
  { }

  public override NODE_STATUS 
  Update(Actor<KinematicBody> _actor)
  {

    NODE_STATUS status = _m_child.Tick(_actor);

    if(status == NODE_STATUS.kSuccess)
    {

      return NODE_STATUS.kFailure;

    }
    else if(status == NODE_STATUS.kFailure)
    {

      return NODE_STATUS.kSuccess;

    }
    else
    {

      return status;

    }
  
  }

}
