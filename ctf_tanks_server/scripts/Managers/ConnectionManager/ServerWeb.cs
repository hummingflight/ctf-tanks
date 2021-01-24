using Godot;

public class ServerWeb
  : ServerManager
{

  public ServerWeb()
  {

    _m_port = 8080;

    _m_maxPlayers = -1;

    _m_connection = null;

    return;

  }

  override public void
  Prepare()
  {

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

  public override void
  Update()
  {

    if(_m_connection != null)
    {

      if(_m_connection.IsListening())
      {

        _m_connection.Poll();

      }
      else
      {

        GD.Print("Not listening yet!");

      }      

    }

    return;

  }

  public override void
  CreateServer(SceneTree _scene, int _port, int _maxPlayers)
  {

    if (_m_connection == null)
    {

      // Create the network

      _m_connection = new WebSocketServer();

      string[] protocols = new string[] { };

      _m_connection.Listen(_port, protocols, true);

      // Setup network peer to the scene.

      _scene.NetworkPeer = _m_connection;

      // Setup listeners

      _m_connection.Connect("client_connected", this, "_OnClientConnected");
      _m_connection.Connect("client_disconnected", this, "_OnClientDisconnected");

      // Save properties

      _m_port = _port;
      _m_maxPlayers = _maxPlayers;

      GD.Print("Web Socket Server Created.");

    }
    else
    {

      GD.Print("Server already created.");

    }

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
  _OnClientConnected(int _peerID, string _protocol)
  {

    GD.Print("Player connected: " + _peerID.ToString());

    return;

  }

  public void
  _OnClientDisconnected(int _peerID, bool _wasCleanClose)
  {

    GD.Print("Player disconnected: " + _peerID.ToString());

    return;

  }

  private WebSocketServer _m_connection;

  /// <summary>
  /// Server port number.
  /// </summary>
  private int _m_port;

  /// <summary>
  /// Server Maximum number of players.
  /// </summary>
  private int _m_maxPlayers;

  private Master _m_master;

}
