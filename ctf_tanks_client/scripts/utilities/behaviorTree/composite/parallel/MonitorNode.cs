using Godot;

public class MonitorNode
  : ParallelNode
{

  public MonitorNode(OPERATION_POLICY _forSuccess, OPERATION_POLICY _forFailure)
    : base(_forSuccess, _forFailure)
  {

    return;

  }

  public void 
  AddCondition(BehaviorNode _condition)
  {

    _m_children.AddToStart(_condition);

    return;

  }

  public void
  AddAction(BehaviorNode _action)
  {

    _m_children.AddAtEnd(_action);

    return;

  }

}
