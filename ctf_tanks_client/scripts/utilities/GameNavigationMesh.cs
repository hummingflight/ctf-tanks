using Godot;
using System;

public class GameNavigationMesh 
  : Navigation
{
    
  public override void 
  _Ready()
  {

    // Get Master Manager.
    MasterManager master = MasterManager.GetInstance();

    // Set Level Pathfinding.
    master.GAME_MANAGER.m_levelPathfinding.SetLevelNavigation(this);
    
  }

}
