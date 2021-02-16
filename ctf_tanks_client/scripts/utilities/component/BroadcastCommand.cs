public class BroadcastCommand
{

  public BroadcastCommand(MESSAGE_ID _messageID, IMessage _message)
  {

    m_messageID = _messageID;
    m_message = _message;
    return;

  }

  public MESSAGE_ID m_messageID;

  public IMessage m_message;

}
