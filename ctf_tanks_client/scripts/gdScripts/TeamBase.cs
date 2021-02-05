using Godot;

public class TeamBase
: Spatial
{

  public override void 
  _Ready()
  {

    MasterManager master = MasterManager.GetInstance();

    GameManager gameManager = master.GAME_MANAGER;

    Team team = gameManager.m_teamsManagers.GetTeam(m_teamKey);

    team.SetBaseNode(this);

    return;

  }

  [Export]
  public TEAM_KEY m_teamKey = TEAM_KEY.kRed;

  [Export]
  public string m_name = "Name";

}