using Godot;

public class BTBCMD_Decorator
  : BTBCMD
{

  public virtual BehaviorNode
  Instantiate(BehaviorNode _child)
  {

    return null;

  }

  public BehaviorNode
  Exec(BTBuilder _BTBuilder)
  {

    BTBCMD command = _BTBuilder.m_aCommands.Dequeue();

    BehaviorNode decorator = null;

    if (command.GetKey() != BTBCMD_KEY.kReturn)
    {

      if (command.GetKey() == BTBCMD_KEY.kEnd)
      {

        GD.PrintErr("BTBuilder SINTAX Error: End of commands reached.");

        return decorator;

      }

      decorator = Instantiate(command.Exec(_BTBuilder));

    }
    else
    {

      GD.PrintErr("BTBuilder SINTAX Error: RETURN command after a decorator " +
        "command. Expected a command that returns a valid behavior.");

      return decorator;

    }

    command = _BTBuilder.m_aCommands.Dequeue();

    if (command.GetKey() != BTBCMD_KEY.kReturn)
    {

      if (command.GetKey() == BTBCMD_KEY.kEnd)
      {

        GD.PrintErr("BTBuilder SINTAX Error: End of commands reached.");

        return decorator;

      }

      GD.PrintErr("BTBuilder SINTAX Error: Expected a RETURN command");

      return decorator;

    }

    return decorator;

  }

  public BTBCMD_KEY
  GetKey()
  {

    return BTBCMD_KEY.kDecorator;
  }

}
