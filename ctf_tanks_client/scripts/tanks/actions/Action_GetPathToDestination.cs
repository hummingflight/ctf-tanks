using Godot;

public class Action_GetPathToDestination
: BehaviorNode
{

  public override NODE_STATUS
  Update(Actor<KinematicBody> _actor)
  {
  
    // Get game manager.
    GameManager gameManager = MasterManager.GetInstance().GAME_MANAGER;

    // Get the Godot navigation node.
    Navigation navigationNode =
      gameManager.m_levelPathfinding.GetLevelNavigation();

    if(navigationNode == null)
    {

      GD.Print("Navigation node not founded.");

      return NODE_STATUS.kFailure;

    }

    // Get start position.
    Vector3 startPosition = _actor.GetNode().Transform.origin;

    // Get final position.
    BItem_Vector3 destination 
      = _actor.m_blackboard.GetItem<BItem_Vector3>(BLACKBOARD_ITEM.kDestination);    

    Vector3[] navigationPath = navigationNode.GetSimplePath( startPosition, 
                                                             destination.v3Value, 
                                                             true );

    // Create path node vector.
    ActiveItemVector<CTF.PathNode> pathNodeVector 
      = new ActiveItemVector<CTF.PathNode>();

    // Add nodes.
    foreach(Vector3 position in navigationPath)
    {

      pathNodeVector.AddAtEnd(new CTF.PathNode(position));

    }

    // Get the path from the navigation node and save it in the actor blackboard.
    BItem_Path actorPath
      = _actor.m_blackboard.GetItem<BItem_Path>(BLACKBOARD_ITEM.kPath);

    actorPath.m_vectorPathNode = pathNodeVector;

    pathNodeVector.Start();

    // Return success.
    return NODE_STATUS.kSuccess;

  }

}