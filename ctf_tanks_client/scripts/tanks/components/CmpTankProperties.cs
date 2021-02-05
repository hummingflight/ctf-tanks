using Godot;

public class CmpTankProperties
: Component<KinematicBody>
{

  override public void
  _Ready()
  {

    return;

  }

  public override COMPONENT_ID 
  GetID()
  {

    return COMPONENT_ID.kTankProperties;

  }

  public Vector3 m_actualVelocity;

  public float _m_gravity;

  public float _m_enginePower;

  public float _m_reversePower;

  public float m_steerMaxAngleOpening;

  public float _m_friction;

  public float _m_drag;

  public TEAM_KEY m_teamKey;

}
