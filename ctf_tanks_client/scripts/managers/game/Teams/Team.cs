using Godot;
using System.Collections.Generic;

public class Team
{
  
  /**********************************************/
  /* Public                                     */
  /**********************************************/
  
  public Team(TEAM_KEY _key)
  {

    _m_key = _key;
    _m_hMembers = new Dictionary<string, Actor<KinematicBody>>();

    if(_key == TEAM_KEY.kBlue)
    {      

      _m_teamColor = new Color(0, 0, 1);
    }
    else
    {
      _m_teamColor = new Color(1, 0, 0);
    }

    return;

  }
  
  public void
  AddMember(string _name, Actor<KinematicBody> _actor)
  {

    _m_hMembers.Add(_name, _actor);

    return;

  }

  public Actor<KinematicBody>
  GetMember(string _name)
  {

    if(HasMember(_name))
    {

      return _m_hMembers[_name];

    }
    else
    {

      GD.PrintErr("Team does not has a member with name : " + _name);

      return null;

    } 

  }

  public bool
  HasMember(string _name)
  {

    return _m_hMembers.ContainsKey(_name);

  }

  public void
  SetBaseNode(TeamBase _teamBase)
  {

    // Save team base.
    _m_baseNode = _teamBase;

    return;

  }

  public Vector3
  GetBasePosition()
  {

    return _m_baseNode.GlobalTransform.origin;

  }

  /// <summary>
  /// Get the team key.
  /// </summary>
  public TEAM_KEY KEY
  {
    get
    {
      return _m_key;
    }
  }

  /// <summary>
  /// Get the base position.
  /// </summary>
  public Vector3
  BASE_POSITION
  {
    get
    {
      return _m_baseNode.GlobalTransform.origin;
    }
  }

  /// <summary>
  /// Get the team color.
  /// </summary>
  public Color
  TEAM_COLOR
  {
    get
    {
      return _m_teamColor;
    }
  }

  /**********************************************/
  /* Private                                    */
  /**********************************************/
    
  /// <summary>
  /// Team key.
  /// </summary>
  private TEAM_KEY _m_key;

  /// <summary>
  /// Table of members.
  /// </summary>
  private Dictionary<string, Actor<KinematicBody>> _m_hMembers;

  /// <summary>
  /// Position of the team base.
  /// </summary>
  private TeamBase _m_baseNode;

  /// <summary>
  /// The team color.
  /// </summary>
  private Color _m_teamColor;

}