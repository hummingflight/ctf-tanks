using Godot;

public class BTBCMD_Sequence
: BTBCMD
{

  public BehaviorNode
  Exec(BTBuilder _BTBuilder)
  {

    SequenceNode seq = new SequenceNode();

    BTBCMD command = _BTBuilder.m_aCommands.Dequeue();

    while(command.GetKey() != BTBCMD_KEY.kReturn)
    {

      if (command.GetKey() == BTBCMD_KEY.kEnd)
      {

        GD.PrintErr("BTBuilder SINTAX Error: End of commands reached.");

        return seq;

      }

      seq.AddChild(command.Exec(_BTBuilder));

      command = _BTBuilder.m_aCommands.Dequeue();

    }

    return seq;

  }

  public BTBCMD_KEY
  GetKey()
  {

    return BTBCMD_KEY.kSequence;

  }

}
