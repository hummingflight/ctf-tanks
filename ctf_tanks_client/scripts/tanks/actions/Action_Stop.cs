using Godot;

public class Action_Stop
: BehaviorNode
{

  public override NODE_STATUS 
  Update(Actor<KinematicBody> _actor)
  {

    CmpTankPhysics physics 
      = _actor.GetComponent<CmpTankPhysics>(COMPONENT_ID.kTankPhysics);

    // Tank speed.
    float speed = physics.VELOCITY.Dot(physics.DIRECTION);

    // Acceleration blackboard item.
    BItem accStrength =
       _actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kAcceleration_Strength);

    accStrength.fValue = 0.0f;

    // Reverse blackboard item.
    BItem reverseStrength =
      _actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kReverse_Strength);

    reverseStrength.fValue = 0.0f;


    if (-0.01f < speed && speed < 0.01f)
    {

      return NODE_STATUS.kSuccess;

    }
    else
    {

      if (speed < -0.01f) // True if tank is in reverse.
      {

        accStrength.fValue = Mathf.Min(-speed, 1.0f);

      }
      else // Tank is forward
      {

        reverseStrength.fValue = Mathf.Min(speed, 1.0f);

      }

      return NODE_STATUS.kRunning;

    }
  
  }

  public override void OnTerminate
  (
    Actor<KinematicBody> _actor,
    NODE_STATUS status
  )
  {

    GD.Print("Stop: Terminate");

    return;

  }

}