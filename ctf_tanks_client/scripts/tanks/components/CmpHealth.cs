using Godot;

public class CmpHealth
: Component<KinematicBody>
{

  public CmpHealth()
  {

    _m_health = 1;
    return;

  }

  public CmpHealth(int _initHealth)
  {

    HEALT = _initHealth;
    return;

  }

  public override void 
  ReceiveMessage(MESSAGE_ID _messageID, IMessage _message)
  {

    switch(_messageID)
    {

      case MESSAGE_ID.kReceive_Damage:

        ReceiveDamage(_message as MSG_Damage);
        return;

      default:
        return;

    }
  
  }

  public void
  ReceiveDamage(MSG_Damage _damage)
  {

    _m_health -= _damage.m_damagePoints;

    if(_m_health <= 0)
    {

      _m_actor.Broadcast(MESSAGE_ID.kZero_health, null);
      return;

    }

    return;

  }

  public override COMPONENT_ID
  GetID()
  {

    return COMPONENT_ID.kHealth;
  
  }

  /// <summary>
  /// Health points.
  /// </summary>
  public int
  HEALT
  {
    get
    {
      return _m_health;
    }
    set
    {
      _m_health = value;
    }
  }

  /// <summary>
  /// Health points.
  /// </summary>
  protected int _m_health;

}