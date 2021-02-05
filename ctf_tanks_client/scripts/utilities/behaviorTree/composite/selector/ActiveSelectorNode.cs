using Godot;

public class ActiveSelectorNode
  : SelectorNode
{

  public override void
  OnInit(Actor<KinematicBody> _actor)
  {

    _m_current = _m_children.END;

    return;

  }

  public override NODE_STATUS
  Update(Actor<KinematicBody> _actor)
  {

    ItemVectorNode<BehaviorNode> previous = _m_current;

    base.OnInit(_actor);

    NODE_STATUS status = base.Update(_actor);

    if(previous != _m_children.END && _m_current != previous)
    {

      previous.m_item.OnTerminate(_actor, NODE_STATUS.kAborted);

    }

    return status;

  }

}