using Godot;

public class DebugCanvas : Control
{
  /// <summary>
  /// Setup this canvas layer as the debugging canvas.
  /// </summary>
  public override void 
  _Ready()
  {

    MasterManager master = MasterManager.GetInstance();

    DebugManager debugManager 
      = master.GetManager<DebugManager>(MANAGER_KEY.kDebugManager);

    debugManager.SetCanvasLayer(this);

    _m_debugManager = debugManager;

    return;
        
  }

  // Call the update process.
  public override void
  _Process(float _delta)
  {

    Update();

    return;

  }

  /// <summary>
  /// Execute all the drawing commands.
  /// </summary>
  public override void 
  _Draw()
  {

    _m_debugManager._Draw();

    return;

  }

  private DebugManager _m_debugManager;
}
