using Godot;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class MasterManager
{

  /**********************************************/
  /* Public                                     */
  /**********************************************/

  /// <summary>
  /// Create the master manager singleton.
  /// </summary>
  public static void
  Prepare()
  {

    if (_INSTANCE == null)
    {

      _INSTANCE = new MasterManager();

      _INSTANCE.OnPrepare();

    }
    else
    {

      GD.Print("MasterManager is already created.");

    }

    return;

  }

  /// <summary>
  /// Destroy the master manager singleton.
  /// </summary>
  public static void
  Shutdown()
  {

    if (_INSTANCE != null)
    {

      _INSTANCE.OnShutdown();

      _INSTANCE = null;

    }
    else
    {

      GD.Print("MasterManager is not created.");

    }

    return;

  }

  /// <summary>
  /// Get the master manager singleton.
  /// </summary>
  /// <returns>Master manager singleton.</returns>
  public static MasterManager
  GetInstance()
  {

    return _INSTANCE;

  }

  /// <summary>
  /// Get a manager.
  /// </summary>
  /// <typeparam name="T">Manager class type.</typeparam>
  /// <param name="_key">Manager key.</param>
  /// <returns>Manager.</returns>
  public T
  GetManager<T>(MANAGER_KEY _key)
  where T : Manager
  {

    if (_m_hManagers.ContainsKey(_key))
    {

      return _m_hManagers[_key] as T;

    }
    else
    {

      GD.PrintErr("Manager with key: " + _key.ToString() + " doesn't found.");

      return null;

    }

  }

  /// <summary>
  /// Broadcast a message to all managers.
  /// </summary>
  /// <param name="_id">Message id.</param>
  /// <param name="_msg">Message.</param>
  public void
  Broadcast(MESSAGE_ID _id, IMessage _msg)
  {

    foreach (KeyValuePair<MANAGER_KEY, Manager> item in _m_hManagers)
    {

      item.Value.Receive(_id, _msg);

    }

    return;

  }

  public bool
  HasManager(MANAGER_KEY _key)
  {

    return _m_hManagers.ContainsKey(_key);

  }

  /**********************************************/
  /* Private                                    */
  /**********************************************/

  /// <summary>
  /// Private constructor.
  /// </summary>
  private MasterManager()
  {

    return;

  }

  /// <summary>
  /// 
  /// </summary>
  private void
  OnPrepare()
  {

    _m_hManagers = new Dictionary<MANAGER_KEY, Manager>();

    ////////////////////////////////////////////
    // Managers

    _m_gameManager = new GameManager();
    AddManager(MANAGER_KEY.kGameManager, _m_gameManager);

    _m_debugManager = new DebugManager();
    AddManager(MANAGER_KEY.kDebugManager, _m_debugManager);

    AddManager(MANAGER_KEY.kCameraManager, new CameraManager());

    return;

  }

  /// <summary>
  /// 
  /// </summary>
  private void
  OnShutdown()
  {

    foreach (KeyValuePair<MANAGER_KEY, Manager> item in _m_hManagers)
    {

      item.Value.Destroy();

    }

    _m_hManagers.Clear();
    _m_hManagers = null;

    return;

  }

  private void
  AddManager(MANAGER_KEY _key, Manager _manager)
  {

    _m_hManagers.Add(_key, _manager);

    _manager.SetMasterManager(this);

    return;

  }

  /// <summary>
  /// Get the game manager.
  /// </summary>
  public GameManager 
  GAME_MANAGER
  {
    get
    {
      return _m_gameManager;
    }
  }

  /// <summary>
  /// Get the debug manager.
  /// </summary>
  public DebugManager
  DEBUG_MANAGER
  {
    get
    {
      return _m_debugManager;
    }
  }

  /// <summary>
  /// Singleton.
  /// </summary>
  private static MasterManager _INSTANCE;

  /// <summary>
  /// Game manager.
  /// </summary>
  private GameManager _m_gameManager;

  /// <summary>
  /// Debug manager.
  /// </summary>
  private DebugManager _m_debugManager;

  /// <summary>
  /// Table of managers.
  /// </summary>
  private Dictionary<MANAGER_KEY, Manager> _m_hManagers;

}