public class MSG_TeamBase
  : IMessage
{

  public MSG_TeamBase(TeamBase _base)
  {

    m_teamBase = _base;
    return;

  }

  public TeamBase m_teamBase;

}