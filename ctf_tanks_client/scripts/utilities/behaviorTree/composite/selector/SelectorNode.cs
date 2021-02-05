using Godot;

public class SelectorNode
: CompositeNode
{

  public override void
  OnInit(Actor<KinematicBody> _actor)
  {

    _m_current = _m_children.GetFirst();

    return;

  }

  public override NODE_STATUS
  Update(Actor<KinematicBody> _actor)
  {

    for(; ; )
    {

      NODE_STATUS status = _m_current.m_item.Tick(_actor);

      if(status != NODE_STATUS.kFailure)
      {

        return status;

      }

      _m_current = _m_current.GetNext();

      if(_m_current == _m_children.END)
      {

        return NODE_STATUS.kFailure;

      }

    }

  }

  protected ItemVectorNode<BehaviorNode> _m_current;

}