using Godot;

public class Action_GetPathToDestination
: BehaviorNode
{

  public override NODE_STATUS
  Update(Actor<KinematicBody> _actor)
  {

    Vector3 actorPosition = _actor.GetNode().Transform.origin;

    BItem_Vector3 destination 
      = _actor.m_blackboard.GetItem<BItem_Vector3>(BLACKBOARD_ITEM.kDestination);

    BItem_NavigationMesh navMesh
      = _actor.m_blackboard.GetItem<BItem_NavigationMesh>(BLACKBOARD_ITEM.kNavigation);

    BItem_Path actorPath
      = _actor.m_blackboard.GetItem<BItem_Path>(BLACKBOARD_ITEM.kPath);

    actorPath.m_path
      = navMesh.m_navigationMesh.GetSimplePath(actorPosition, destination.v3Value);

    return NODE_STATUS.kSucess;

  }

}