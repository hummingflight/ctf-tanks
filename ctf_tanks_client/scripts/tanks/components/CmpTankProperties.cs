using Godot;

public class CmpTankProperties
: Component<KinematicBody>
{
  
  public override COMPONENT_ID 
  GetID()
  {

    return COMPONENT_ID.kTankProperties;

  }

  /// <summary>
  /// Set the team that this tank belongs to.
  /// </summary>
  /// <param name="_team"></param>
  public void
  SetTeam(Team _team)
  {

    //Add member.
    _team.AddMember(_m_node.Name, _m_actor);

    //Save team.
    _m_team = _team;

    return;

  }

  public OPERATION_RESULT
  SetTeam(TEAM_KEY _team)
  {

    // Get the teams manager.
    TeamsManager teamsMng = MasterManager
                            .GetInstance()
                            .GAME_MANAGER
                            .TEAMS_MANAGER;

    if(teamsMng.HasTeam(_team))
    {

      // Set team.
      SetTeam(teamsMng.GetTeam(_team));

      return OPERATION_RESULT.kSuccess;

    }

    return OPERATION_RESULT.kFail;

  }

  /// <summary>
  /// Get the team this tank belongs to.
  /// </summary>
  public Team
  TEAM
  {
    get
    {
      return _m_team;
    }
  }

  /// <summary>
  /// Get the key of the team this tank belongs to.
  /// </summary>
  public TEAM_KEY
  TEAM_KEY
  {
    get
    {
      return _m_team.KEY;
    }
  }

  /// <summary>
  /// The team this tank belongs to.
  /// </summary>
  private Team _m_team;

}
