using Godot;

public class GameManager
: Manager
{

  public
  GameManager()
  {

    _m_levelPathfinding = new LevelPathfinding();
    _m_teamsManagers = new TeamsManager();

    return;

  }

  public void
  SetActiveTank(Actor<KinematicBody> _actor)
  {

    _m_activeTank = _actor;

    return;

  }

  public void
  SetActiveTank(string _name, TEAM_KEY _team)
  {

    Team team = _m_teamsManagers.GetTeam(_team);

    if(team.HasMember(_name))
    {

      SetActiveTank(team.GetMember(_name));

      return;

    }
    else
    {

      GD.PrintErr("Team: " + _team.ToString() + " does not has member: " + _name);

    }

    return;

  }

  /// <summary>
  /// Get the active tank.
  /// </summary>
  public Actor<KinematicBody>
  ACTIVE_TANK
  {
    get
    {
      return _m_activeTank;
    }
  }

  /// <summary>
  /// Get the level path finding of this game.
  /// </summary>
  public LevelPathfinding
  LEVEL_PATHFINDING
  {
    get
    {
      return _m_levelPathfinding;
    }
  }

  /// <summary>
  /// Get the teams manager of this game.
  /// </summary>
  public TeamsManager
  TEAMS_MANAGER
  {
    get
    {
      return _m_teamsManagers;
    }
  }

  /// <summary>
  /// Level path finding manager.
  /// </summary>
  private LevelPathfinding _m_levelPathfinding;

  /// <summary>
  /// Team manager.
  /// </summary>
  private TeamsManager _m_teamsManagers;

  /**********************************************/
  /* Private                                    */
  /**********************************************/

  /// <summary>
  /// The selected tank.
  /// </summary>
  private Actor<KinematicBody> _m_activeTank;

}
