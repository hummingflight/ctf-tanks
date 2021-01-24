using Godot;
using System.Collections.Generic;

public class Master : Node
{

  public override void 
  _Ready()
  {

    Prepare();

    return;
    
  }

  public void
  Prepare()
  {

    _m_hManagers = new Dictionary<GAME_MANAGER, GameManager>();

    _m_hManagers.Add(GAME_MANAGER.kServerManager, new ServerWeb());

    // Prepare Managers

    foreach( KeyValuePair<GAME_MANAGER, GameManager> pair in _m_hManagers)
    {

      pair.Value.Prepare();

    }

    // Init Managers

    foreach (KeyValuePair<GAME_MANAGER, GameManager> pair in _m_hManagers)
    {

      pair.Value.Init(this);

    }

    return;

  }

  public override void 
  _Process(float delta)
  {

    foreach (KeyValuePair<GAME_MANAGER, GameManager> pair in _m_hManagers)
    {

      pair.Value.Update();

    }

    return;

  }

  private Dictionary<GAME_MANAGER, GameManager> _m_hManagers;

}
