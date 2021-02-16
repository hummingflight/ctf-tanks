using Godot;

public class CmpDestructible
: Component<KinematicBody>
{

  public override void 
  ReceiveMessage(MESSAGE_ID _messageID, IMessage _message)
  {

    switch(_messageID)
    {

      case MESSAGE_ID.kZero_health:
        _m_actor.Broadcast(MESSAGE_ID.kDestroy, null);
        return;

      default:
        return;

    }
    
  }

  public override COMPONENT_ID 
  GetID()
  {

    return COMPONENT_ID.kDestructible;

  }

}
