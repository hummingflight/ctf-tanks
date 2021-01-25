using Godot;

public class CmpTankInput
  : Component<KinematicBody>
{

  override public void
  _Ready()
  {

    return;

  }

  override public void
  _Process(float _delta)
  {

    // Get the steer value

    float steerValue = Input.GetActionStrength(_m_steerRightKey)
                     - Input.GetActionStrength(_m_steerLeftKey);

    BlackboardItem tankSteeringItem =
      _m_actor.m_blackboard.GetItem(BLACKBOARD_ITEM.kTank_Steering);

    tankSteeringItem.fValue = steerValue;

    // Get the acceleration value

    float acceleration = Input.GetActionStrength(_m_accelerationKey);

    BlackboardItem accelerationItem =
      _m_actor.m_blackboard.GetItem(BLACKBOARD_ITEM.kAcceleration_Strength);

    accelerationItem.fValue = acceleration;

    // Get the reverse value

    float reverse = Input.GetActionStrength(_m_reverseKey);

    BlackboardItem breakItem =
      _m_actor.m_blackboard.GetItem(BLACKBOARD_ITEM.kReverse_Strength);

    breakItem.fValue = reverse;

    return;
  
  }

  /// <summary>
  /// Get the component identifier.
  /// </summary>
  /// <returns></returns>
  override public COMPONENT_ID
  GetID()
  {

    return COMPONENT_ID.kTankInput;

  }

  private string _m_steerRightKey = "steer_right";

  private string _m_steerLeftKey = "steer_left";

  private string _m_accelerationKey = "accelerate";

  private string _m_reverseKey = "reverse";

}
