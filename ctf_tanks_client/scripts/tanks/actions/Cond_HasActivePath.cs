using Godot;

public class Cond_HasActivePath
  : BehaviorNode
{

  public override NODE_STATUS
  Update(Actor<KinematicBody> _actor)
  {

    // Check if actor has a Path blackboard item.
    if(_actor.m_blackboard.HasItem(BLACKBOARD_ITEM.kPath))
    {

      // Get path node item
      BItem_Path item_Path
      = _actor.m_blackboard.GetItem<BItem_Path>(BLACKBOARD_ITEM.kPath);

      // Get path
      ActiveItemVector<CTF.PathNode> path = item_Path.m_vectorPathNode;

      // Check if path exists.
      if(path != null)
      {

        // Check if path has nodes.
        return (path.SIZE != 0 ? NODE_STATUS.kSuccess : NODE_STATUS.kFailure);

      }
      else
      {

        return NODE_STATUS.kFailure;

      }

    }
    else
    {

      return NODE_STATUS.kFailure;

    }   

  }

}
