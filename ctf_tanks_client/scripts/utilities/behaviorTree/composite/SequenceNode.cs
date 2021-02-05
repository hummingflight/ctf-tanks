using Godot;

public class SequenceNode
: CompositeNode
{

  public SequenceNode()
  {

    _m_children = new ItemVector<BehaviorNode>();

    _m_currentNode = _m_children.GetFirst();

    return;

  }

  public override void
  OnInit(Actor<KinematicBody> _actor)
  {

    _m_currentNode = _m_children.GetFirst();

    return;

  } 

  public override NODE_STATUS
  Update(Actor<KinematicBody> _actor)
  {

    for(;;)
    {

      NODE_STATUS status = _m_currentNode.m_item.Tick(_actor);

      if(status != NODE_STATUS.kSucess)
      {

        return status;

      }

      _m_currentNode = _m_currentNode.GetNext();

      if(_m_currentNode == _m_children.END)
      {

        return NODE_STATUS.kSucess;

      }

    }

  }

  protected ItemVectorNode<BehaviorNode> _m_currentNode;

}