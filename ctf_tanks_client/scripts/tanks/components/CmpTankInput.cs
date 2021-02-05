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

    _UpdateInputSteering();

    _UpdateInputAcceleration();

    _UpdateInputReverse();

    _UpdateInputFire();

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

  private void
  _UpdateInputFire()
  {

    BItem firebutton =
      _m_actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kFire_Button);

    firebutton.iValue = (Input.IsActionPressed(_m_fireKey) ? 1 : 0);

    return;

  }

  private void
  _UpdateInputAcceleration()
  {

    // Get the acceleration value

    BItem accelerationItem =
      _m_actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kAcceleration_Strength);

    accelerationItem.fValue = Input.GetActionStrength(_m_accelerationKey);

    return;

  }

  private void
  _UpdateInputSteering()
  {

    BItem tankSteeringItem =
      _m_actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kTank_Steering);

    tankSteeringItem.fValue = Input.GetActionStrength(_m_steerRightKey)
                            - Input.GetActionStrength(_m_steerLeftKey);

    return;

  }

  private void
  _UpdateInputReverse()
  {

    // Get the reverse value

    float reverse = Input.GetActionStrength(_m_reverseKey);

    BItem breakItem =
      _m_actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kReverse_Strength);

    breakItem.fValue = reverse;

    return;

  }

  private string _m_steerRightKey = "steer_right";

  private string _m_steerLeftKey = "steer_left";

  private string _m_accelerationKey = "accelerate";

  private string _m_reverseKey = "reverse";

  private string _m_fireKey = "fire";

}
