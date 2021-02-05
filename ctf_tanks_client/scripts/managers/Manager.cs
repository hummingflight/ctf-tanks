/// <summary>
/// Base interface of any manager.
/// </summary>
public class Manager
{
  
  /// <summary>
  /// Override this method to handle incoming message from master.
  /// </summary>
  /// <param name="_id">Message id.</param>
  /// <param name="_msg">Message.</param>
  public virtual void
  Receive(MESSAGE_ID _id, IMessage _msg)
  {

    return;

  }
  

  /// <summary>
  /// Override this method to safely destroy the manager.
  /// </summary>
  public virtual void
  Destroy()
  {

    return;

  }

  /// <summary>
  /// Set the master manager of this manager.
  /// </summary>
  /// <param name="_master">Master Manager</param>
  public void
  SetMasterManager(MasterManager _master)
  {

    _m_masterManager = _master;

    return;

  }

  /// <summary>
  /// Master manager.
  /// </summary>
  protected MasterManager _m_masterManager;

}
