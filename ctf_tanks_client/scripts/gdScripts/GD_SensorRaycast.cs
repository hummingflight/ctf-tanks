using Godot;

public class GD_SensorRaycast
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

    // Add this sensor to the parent contact sensor component.

    // Get parent node.
    TankActorBot parentNode = GetParent<TankActorBot>();

    if(parentNode != null)
    {

      // Component.
      if(parentNode.Actor.HasComponent(COMPONENT_ID.kContactSensors))
      {

        CmpContactSensors contactSensors = parentNode
                                           .Actor
                                           .GetComponent<CmpContactSensors>
                                           (
                                              COMPONENT_ID.kContactSensors
                                           );

        // Add RayCast.
        contactSensors.AddRayCast(Name, this);

      }
      else
      {

        GD.PrintErr("Sensor RayCast: Parent actor does not has Contact Sensor " +
                    "Component.");

      }      

    }
    else
    {

      GD.PrintErr("Sensor RayCast: Parent not founded.");

    }

    return;

  }

  public override void 
  _PhysicsProcess(float delta)
  {

    if(m_debug)
    {

      _UpdateDebug();

    }

    return;

  }

  /// <summary>
  /// Indicates if the RayCast will be drawn in screen.
  /// </summary>
  [Export]
  public bool m_debug = false;

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
  /// Draw RayCast in the screen.
  /// </summary>
  private void 
  _UpdateDebug()
  {

    // Transform ray cast vector with the global rotation.

    Vector3 castTo = new Vector3(CastTo);
    Quat quat = GlobalTransform.basis.Quat();

    castTo = SVector3.RotateByQuaternion(ref castTo, ref quat);

    // Set line color.

    Color lineColor = m_color;

    if (IsColliding())
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
  /// Debug Manager.
  /// </summary>
  private DebugManager _m_debugManager;

  /// <summary>
  /// Cache debug command.
  /// </summary>
  private DCMD_DrawLine _m_debugCommand;

}
