using Godot;

public class BehaviorTree
{

  public BehaviorTree(BehaviorNode _root)
  {

    _m_root = _root;

    return;

  }

  public NODE_STATUS
  Update(Actor<KinematicBody> _actor)
  {

    return _m_root.Tick(_actor);

  }

  public BehaviorNode START
  {

    get
    {

      return _m_root;

    }

  }

  protected BehaviorNode _m_root;

}