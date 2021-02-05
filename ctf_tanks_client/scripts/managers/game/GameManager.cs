using Godot;

public class GameManager
: Manager
{

  public
  GameManager()
  {

    m_levelPathfinding = new LevelPathfinding();
    m_teamsManagers = new TeamsManager();

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

    Team team = m_teamsManagers.GetTeam(_team);

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
  /// Level path finding manager.
  /// </summary>
  public LevelPathfinding m_levelPathfinding;

  /// <summary>
  /// Team manager.
  /// </summary>
  public TeamsManager m_teamsManagers;

  /**********************************************/
  /* Private                                    */
  /**********************************************/

  /// <summary>
  /// The selected tank.
  /// </summary>
  private Actor<KinematicBody> _m_activeTank;

}
