using Godot;
using System;

public class ServerManager
  : GameManager
{

  virtual public void
  CreateServer(SceneTree _scene, int _port, int _maxPlayers)
  {

    return;

  }

  virtual public int
  GetPort()
  {

    return -1;

  }

  virtual public int
  GetMaxPlayers()
  {

    return -1;

  }

}