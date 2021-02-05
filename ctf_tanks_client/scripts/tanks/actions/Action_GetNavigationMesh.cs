using Godot;

public class Action_GetNavigationMesh
: BehaviorNode
{

  public override NODE_STATUS 
  Update(Actor<KinematicBody> _actor)
  {

    GameManager gameManager = MasterManager.GetInstance().GAME_MANAGER;

    Navigation navigationNode = 
      gameManager.m_levelPathfinding.GetLevelNavigation();

    BItem_NavigationMesh itemNavMesh =
      _actor.m_blackboard.GetItem<BItem_NavigationMesh>(BLACKBOARD_ITEM.kNavigation);

    itemNavMesh.m_navigationMesh = navigationNode;

    return NODE_STATUS.kSucess;

  }

}
