using Godot;

public class TeamBase
: Area
{

  public override void 
  _Ready()
  {

    // Get flag reference.
    _m_flagNode = GetNode<Spatial>("Flag");

    // Setup team.
    Team team = MasterManager.GetInstance()
                .GAME_MANAGER
                .TEAMS_MANAGER
                .GetTeam(m_teamKey);

    if(team == null)
    {

      GD.Print("Team does not exist: " + team.ToString());

    }

    // Set team.
    SetTeam(team);

    // Signals

    Connect("body_entered", this, "_OnBodyEntered");

    return;

  }

  public void
  SetTeam(Team _team)
  {

    // Set team base node.
    _team.SetBaseNode(this);

    // Get the flag mesh.
    MeshInstance flagMesh = GetNode<MeshInstance>("Flag/MeshInstance");

    if(_team.KEY == TEAM_KEY.kBlue)
    {

      SpatialMaterial material
        = GD.Load<SpatialMaterial>("res://materials/misc/BlueFlag.tres");

      flagMesh.MaterialOverride = material;

    }
    else
    {

      SpatialMaterial material
      = GD.Load<SpatialMaterial>("res://materials/misc/RedFlag.tres");

      flagMesh.MaterialOverride = material;

    }    

  }

  public void
  ShowFlag()
  {

    if(!_m_hasFlag)
    {

      _m_flagNode.Show();

    }

    return;

  }

  public void
  HideFlag()
  {

    if(_m_hasFlag)
    {

      _m_flagNode.Hide();

    }

    return;

  }

  /// <summary>
  /// Check if this team base has the team flag or not.
  /// </summary>
  public bool
  HAS_FLAG
  {
    get
    {
      return _m_hasFlag;
    }
  }

  [Export]
  public TEAM_KEY m_teamKey = TEAM_KEY.kRed;

  [Export]
  public string m_name = "Name";

  /// <summary>
  /// Handle the "body_entered" signal.
  /// </summary>
  /// <param name="_body"></param>
  private void
  _OnBodyEntered( Node _body)
  {


    if(_body is KinematicActor _actor)
    {

      GD.Print("Entered!");

      _actor.Actor.Broadcast(MESSAGE_ID.kEnterTeamBase, new MSG_TeamBase(this));

    }

    return;

  }


  /// <summary>
  /// Reference to the flag node.
  /// </summary>
  private Spatial _m_flagNode;

  /// <summary>
  /// Indicates if this team has its flag.
  /// </summary>
  private bool _m_hasFlag = true;

}