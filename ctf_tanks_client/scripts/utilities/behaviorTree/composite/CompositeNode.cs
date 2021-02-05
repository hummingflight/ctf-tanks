using Godot;

public class CompositeNode
: BehaviorNode
{

  public CompositeNode()
  {

    _m_children = new ItemVector<BehaviorNode>();

    return;

  }

  public void
  AddChild(BehaviorNode _behavior)
  {

    _m_children.AddAtEnd(_behavior);

    return;

  }

  public void
  RemoveChild(BehaviorNode _behavior)
  {

    _m_children.Remove(_behavior);

    return;

  }

  public void
  ClearChildren()
  {

    _m_children.Clear();

    return;

  }

  protected ItemVector<BehaviorNode> _m_children;

}
