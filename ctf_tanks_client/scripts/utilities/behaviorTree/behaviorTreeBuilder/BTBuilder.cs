using System.Collections.Generic;

public class BTBuilder
{

  public BTBuilder()
  {

    m_aCommands = new Queue<BTBCMD>();

    return;

  }

  public BTBuilder
  Action(BT_ACTION _action)
  {

    m_aCommands.Enqueue(new BTBCMD_Action(_action));

    return this;

  }

  public BTBuilder
  Sequence()
  {

    m_aCommands.Enqueue(new BTBCMD_Sequence());

    return this;

  }

  public BTBuilder
  ActiveSelector()
  {

    m_aCommands.Enqueue(new BTBCMD_ActiveSelector());

    return this;

  }

  public BTBuilder
  Return()
  {

    m_aCommands.Enqueue(new BTBCMD_Return());

    return this;

  }

  public BehaviorTree
  Build()
  {

    return new BehaviorTree(m_aCommands.Dequeue().Exec(this));

  }

  public BehaviorTree m_bt;

  public Queue<BTBCMD> m_aCommands;

}