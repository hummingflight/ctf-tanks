using Godot;

public class DecoratorNode
  : BehaviorNode
{

  public DecoratorNode(BehaviorNode _child)
  {

    _m_child = _child;

    return;

  }

  protected BehaviorNode _m_child;

}
