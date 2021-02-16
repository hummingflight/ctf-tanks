using Godot;

public class TeamBase
: Spatial
{

  public override void 
  _Ready()
  {

    Team team = MasterManager.GetInstance()
                .GAME_MANAGER
                .TEAMS_MANAGER
                .GetTeam(m_teamKey);

    if(team == null)
    {

      GD.Print("Team does not exist: " + team.ToString());

    }

    team.SetBaseNode(this);

    return;

  }

  [Export]
  public TEAM_KEY m_teamKey = TEAM_KEY.kRed;

  [Export]
  public string m_name = "Name";

}