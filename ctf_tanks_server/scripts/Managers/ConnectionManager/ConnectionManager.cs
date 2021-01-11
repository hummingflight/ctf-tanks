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

    _m_serverPort = 1909;

    _m_maxPlayers = 4;

    ConnectToServer();

    return;

  }

  public void
  ConnectToServer()
  {

    _m_network.CreateServer(_m_serverPort, _m_maxPlayers);
    GetTree().NetworkPeer = _m_network;

    GD.Print("Server Started");

    // Setup callbacks

    _m_network.Connect("peer_connected", this, "_OnPeerConnected");
    _m_network.Connect("peer_disconnected", this, "_OnPeerDisconnected");

    return;

  }

  public void
  _OnPeerConnected(int _playerID)
  {

    GD.Print("Player connected: " + _playerID);

    return;

  }

  public void
  _OnPeerDisconnected(int _playerID)
  {

    GD.Print("Player disconnected: " + _playerID);

    return;

  }


  private string _m_serverIP;

  private int _m_serverPort;

  private int _m_maxPlayers;

  private NetworkedMultiplayerENet _m_network;

}