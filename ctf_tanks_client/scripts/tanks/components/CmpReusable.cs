using Godot;

public class CmpReusable
: Component<KinematicBody>
{

  public override void 
  ReceiveMessage(MESSAGE_ID _messageID, IMessage _message)
  {

    switch(_messageID)
    {
      case MESSAGE_ID.kZero_health:
        
        Disable();

        return;
    }

    return;

  }

  public void
  Enable()
  {

    if(!_m_actor.IS_ENABLE)
    {

      _m_actor.IS_ENABLE = !_m_actor.IS_ENABLE;

      _m_node.SetProcess(_m_actor.IS_ENABLE);
      _m_node.SetPhysicsProcess(_m_actor.IS_ENABLE);
      _m_node.SetProcessInput(_m_actor.IS_ENABLE);
      _m_node.Show();

    }

    return;

  }

  public void
  Disable()
  {

    if (_m_actor.IS_ENABLE)
    {

      _m_actor.IS_ENABLE = !_m_actor.IS_ENABLE;

      _m_node.SetProcess(_m_actor.IS_ENABLE);
      _m_node.SetPhysicsProcess(_m_actor.IS_ENABLE);
      _m_node.SetProcessInput(_m_actor.IS_ENABLE);
      _m_node.Hide();

    }

    return;

  }

}