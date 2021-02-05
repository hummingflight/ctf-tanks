using Godot;

public class LevelPathfinding
  : Manager
{

  public LevelPathfinding()
  {

  }

  public void
  SetLevelNavigation(Navigation _navigationNode)
  {

    _m_levelNavigation = _navigationNode;

    return;

  }

  public Navigation
  GetLevelNavigation()
  {

    if(_m_levelNavigation != null)
    {

      return _m_levelNavigation;

    }
    else
    {

      GD.PrintErr("Navigation node is null.");

      return null;

    }    

  }

  protected Navigation _m_levelNavigation;

}
