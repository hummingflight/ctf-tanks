using Godot;

public class GD_DebugRayCast
: RayCast
{

  public override void
  _Ready()
  {

    // Get Debug manager.
    MasterManager master = MasterManager.GetInstance();    

    _m_debugManager = master.GetManager<DebugManager>(MANAGER_KEY.kDebugManager);

    // Create Debug command.
    _m_debugCommand = new DCMD_DrawLine();

    return;

  }

  public override void 
  _PhysicsProcess(float delta)
  {

    // Transform ray cast vector with the global rotation.

    Vector3 castTo = new Vector3(CastTo);
    Quat quat = GlobalTransform.basis.Quat();

    castTo = SVector3.RotateByQuaternion(ref castTo, ref quat);

    // Set line color.

    Color lineColor = m_color;

    if(IsColliding())
    {

      lineColor = m_hitColor;

    }

    // Set debug command.

    _m_debugCommand.Set
    (
      _m_debugManager,
      GlobalTransform.origin,
      GlobalTransform.origin + castTo,
      lineColor,
      m_width
    );

    _m_debugManager.AddCommand(_m_debugCommand);

    return;

  }

  /// <summary>
  /// Debug line color.
  /// </summary>
  [Export]
  public Color m_color = new Color(0.0f, 1.0f, 0.0f, 1.0f);

  /// <summary>
  /// The color of the line when the ray cast hits a body.
  /// </summary>
  [Export]
  public Color m_hitColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

  /// <summary>
  /// Debug line width.
  /// </summary>
  [Export]
  public float m_width = 2.0f;

  /**********************************************/
  /* Private                                    */
  /**********************************************/

  /// <summary>
  /// Debug Manager.
  /// </summary>
  private DebugManager _m_debugManager;

  /// <summary>
  /// Cache debug command.
  /// </summary>
  private DCMD_DrawLine _m_debugCommand;

}
