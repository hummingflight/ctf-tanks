using Godot;

public class Action_CourseTowardsEnemyBase
: BehaviorNode
{

  public override NODE_STATUS
  Update(Actor<KinematicBody> _actor)
  { 

    // Enemy team    
    CmpTankProperties properties =
    _actor.GetComponent<CmpTankProperties>(COMPONENT_ID.kTankProperties);

    TEAM_KEY enemyTeamKey = (properties.m_teamKey == TEAM_KEY.kBlue ? TEAM_KEY.kRed : TEAM_KEY.kBlue);

    MasterManager master = MasterManager.GetInstance();

    GameManager gameManager = master.GAME_MANAGER;

    // Get Enemy Base Position.
    Vector3 enemyBasePosition = 
    gameManager.m_teamsManagers.GetTeam(enemyTeamKey).GetBasePosition();    

    // Set destination.
    BItem_Vector3 itemDestination =
    _actor.m_blackboard.GetItem<BItem_Vector3>(BLACKBOARD_ITEM.kDestination);

    itemDestination.v3Value = enemyBasePosition;

    return NODE_STATUS.kSucess;

  }

}
