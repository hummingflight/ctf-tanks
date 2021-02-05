using Godot;
using System.Collections.Generic;

public class TeamsManager
{

  public TeamsManager()
  {

    // Create team map.
    _m_hTeams = new Dictionary<TEAM_KEY, Team>();

    // Add red and blue team.
    AddTeam(new Team(TEAM_KEY.kBlue));
    AddTeam(new Team(TEAM_KEY.kRed));

    return;

  }

  public void 
  AddTeam(Team _team)
  {

    if(!HasTeam(_team.KEY))
    {

      _m_hTeams.Add(_team.KEY, _team);

    }

    return;

  }

  public bool
  HasTeam(TEAM_KEY _key)
  {

    return _m_hTeams.ContainsKey(_key);

  }

  public Team
  GetTeam(TEAM_KEY _key)
  {

    if(HasTeam(_key))
    {

      return _m_hTeams[_key];

    }
    else
    {

      GD.PrintErr("Team with key: " + _key.ToString() + " doesn't exists in the team manager.");

    }

    return null;

  }

  
  private Dictionary<TEAM_KEY, Team> _m_hTeams;

}