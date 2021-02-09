using Godot;

public class ParallelNode
  : CompositeNode
{

  public ParallelNode(OPERATION_POLICY _forSuccess, OPERATION_POLICY _forFailure)
  {

    _m_eSuccessPolicy = _forSuccess;

    _m_eFailurePolicy = _forFailure;

    return;

  }

  public override NODE_STATUS
  Update(Actor<KinematicBody> _actor)
  {

    int iSuccessCount = 0;
    int iFailureCount = 0;

    ItemVectorNode<BehaviorNode> node = _m_children.GetFirst();

    while(node != _m_children.END)
    {

      if(!node.m_item.IsTerminated())
      {

        node.m_item.Tick(_actor);

      }

      NODE_STATUS status = node.m_item.GetStatus();

      if(status == NODE_STATUS.kSuccess)
      {

        ++iSuccessCount;

        if(_m_eSuccessPolicy == OPERATION_POLICY.KRequiereOne)
        {

          return NODE_STATUS.kSuccess;

        }

      }
      else if(status == NODE_STATUS.kFailure)
      {

        ++iFailureCount;

        if(_m_eFailurePolicy == OPERATION_POLICY.KRequiereOne)
        {

          return NODE_STATUS.kFailure;

        }

      }

      node = node.GetNext();

    }

    if
      (
        _m_eFailurePolicy == OPERATION_POLICY.kRequireAll && 
        iFailureCount == _m_children.SIZE
       )
    {

      return NODE_STATUS.kFailure;

    }
    else if
      (
        _m_eSuccessPolicy == OPERATION_POLICY.kRequireAll &&
        iSuccessCount == _m_children.SIZE
      )
    {

      return NODE_STATUS.kSuccess;

    }

    return NODE_STATUS.kRunning;

  }

  public override void 
  OnTerminate(Actor<KinematicBody> _actor, NODE_STATUS status)
  {

    ItemVectorNode<BehaviorNode> node = _m_children.GetFirst();

    while(node != _m_children.END)
    {

      if(node.m_item.IsRunning())
      {

        node.m_item.Abort(_actor);

      }

      node = node.GetNext();

    }

    return;

  }

  protected OPERATION_POLICY _m_eSuccessPolicy;

  protected OPERATION_POLICY _m_eFailurePolicy;

}
