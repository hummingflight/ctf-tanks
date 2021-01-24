using Godot;

public class ServerENet
  : ServerManager
{

  public ServerENet()
  {

    _m_port = 8080;

    _m_maxPlayers = -1;

    _m_connection = null;

    return;

  }

  override public void
  Init(Master _master)
  {

    _m_master = _master;

    // Create Server

    CreateServer(_m_master.GetTree(), 1909, 4);

    return;

  }

  override public void
  CreateServer(SceneTree _scene, int _port, int _maxPlayers)
  {

    if(_m_connection == null)
    {

      // Create the network

      _m_connection = new NetworkedMultiplayerENet();

      _m_connection.CreateServer(_port, _maxPlayers);

      // Setup network peer to the scene.

      _scene.NetworkPeer = _m_connection;

      // Setup listeners

      _m_connection.Connect("peer_connected", this, "_OnPeerConnected");
      _m_connection.Connect("peer_disconnected", this, "_OnPeerDisconnected");

      // Save properties

      _m_port = _port;
      _m_maxPlayers = _maxPlayers;

      GD.Print("Networked Multi-player ENet Server created.");

    }
    else
    {

      GD.Print("Server already created.");

    }

    return;

  }

  override public void 
  Prepare()
  {

    return;

  }

  override public int
  GetPort()
  {

    return _m_port;

  }

  override public int
  GetMaxPlayers()
  {

    return _m_maxPlayers;

  }

  public void
  _OnPeerConnected(int _peerID)
  {

    GD.Print("Player connected: " + _peerID.ToString());

    return;

  }

  public void
  _OnPeerDisconnected(int _peerID)
  {

    GD.Print("Player disconnected: " + _peerID.ToString());

    return;

  }

  private NetworkedMultiplayerENet _m_connection;

  /// <summary>
  /// Server port number.
  /// </summary>
  private int _m_port;

  /// <summary>
  /// Server Maximum number of players.
  /// </summary>
  private int _m_maxPlayers;

  /// <summary>
  /// Master Manager.
  /// </summary>
  private Master _m_master;

}
