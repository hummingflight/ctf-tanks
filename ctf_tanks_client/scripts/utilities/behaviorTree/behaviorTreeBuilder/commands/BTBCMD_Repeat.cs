using Godot;

public class BTBCMD_Repeat
: BTBCMD
{

  public BTBCMD_Repeat(int _count)
  {

    _m_count = _count;

    return;

  }

  public BehaviorNode 
  Exec(BTBuilder _BTBuilder)
  {

    BTBCMD command = _BTBuilder.m_aCommands.Dequeue();

    RepeatNode repeatNode = null;

    if(command.GetKey() != BTBCMD_KEY.kReturn)
    {

      if (command.GetKey() == BTBCMD_KEY.kEnd)
      {

        GD.PrintErr("BTBuilder SINTAX Error: End of commands reached.");

        return repeatNode;

      }

      repeatNode = new RepeatNode(command.Exec(_BTBuilder));

    }
    else
    {

      GD.PrintErr("BTBuilder SINTAX Error: RETURN command after a Repeat " +
        "command. Expected a command that returns a valid behavior.");

      return repeatNode;

    }

    command = _BTBuilder.m_aCommands.Dequeue();

    if (command.GetKey() == BTBCMD_KEY.kReturn)
    {

      if (command.GetKey() == BTBCMD_KEY.kEnd)
      {

        GD.PrintErr("BTBuilder SINTAX Error: End of commands reached.");

        return repeatNode;

      }

      GD.PrintErr("BTBuilder SINTAX Error: Expected a RETURN command");

      return repeatNode;

    }

    return repeatNode;

  }

  public BTBCMD_KEY 
  GetKey()
  {

    return BTBCMD_KEY.kRepeat;
  }

  protected int _m_count;
}
