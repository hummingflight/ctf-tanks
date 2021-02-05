using Godot;

public class BTBCMD_Selector
: BTBCMD
{

  public BehaviorNode
  Exec(BTBuilder _BTBuilder)
  {

    SelectorNode selector = new SelectorNode();

    BTBCMD command = _BTBuilder.m_aCommands.Dequeue();

    while (command.GetKey() != BTBCMD_KEY.kReturn)
    {

      if (command.GetKey() == BTBCMD_KEY.kEnd)
      {

        GD.PrintErr("BTBuilder SINTAX Error: End of commands reached.");

        return selector;

      }

      selector.AddChild(command.Exec(_BTBuilder));

      command = _BTBuilder.m_aCommands.Dequeue();

    }

    return selector;

  }

  public BTBCMD_KEY
  GetKey()
  {

    return BTBCMD_KEY.kSelector;

  }

}
