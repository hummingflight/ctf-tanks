using Godot;
using System;

public class ConnectionManager : Node
{
  // Declare member variables here. Examples:
  // private int a = 2;
  // private string b = "text";

  // Called when the node enters the scene tree for the first time.
  public override void 
  _Ready()
  {

    _m_network = new NetworkedMultiplayerENet();

    _m_serverIP = "127.0.0.1";

    _m_serverPort = 1909;

    ConnectToServer();

    return;

  }

  public void 
  ConnectToServer()
  {

    _m_network.CreateClient(_m_serverIP, _m_serverPort);
    GetTree().NetworkPeer = _m_network;

    // Setup callbacks

    _m_network.Connect("connection_failed", this, "_OnConnectionFailed");
    _m_network.Connect("connection_succeeded", this, "_OnConnectionSucceeded");

    return;

  }


  //  // Called every frame. 'delta' is the elapsed time since the previous frame.
  //  public override void _Process(float delta)
  //  {
  //      
  //  }

  public void
  _OnConnectionFailed()
  {

    GD.Print("Failed to connect.");

    return;

  }

  public void
  _OnConnectionSucceeded()
  {

    GD.Print("Successfully connected.");

    return;

  }


  private string _m_serverIP;

  private int _m_serverPort;

  private NetworkedMultiplayerENet _m_network;

}
