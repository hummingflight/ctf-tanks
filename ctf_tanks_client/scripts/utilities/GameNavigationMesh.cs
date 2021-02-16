using Godot;
using System;

public class GameNavigationMesh 
  : Navigation
{
    
  public override void 
  _Ready()
  {

    MasterManager.GetInstance()
                 .GAME_MANAGER
                 .LEVEL_PATHFINDING
                 .SetLevelNavigation(this);

    return;
    
  }

}
