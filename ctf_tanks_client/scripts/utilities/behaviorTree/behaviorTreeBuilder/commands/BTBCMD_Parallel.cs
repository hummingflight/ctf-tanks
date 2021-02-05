using Godot;

public class BTBCMD_Parallel
: BTBCMD
{

  public BTBCMD_Parallel(OPERATION_POLICY _forSuccess, OPERATION_POLICY _forFailure)
  {

    _m_eSuccessPolicy = _forSuccess;

    _m_eFailurePolicy = _forFailure;

    return;

  }

  public BehaviorNode
  Exec(BTBuilder _BTBuilder)
  {

    ParallelNode parallel = new ParallelNode(_m_eSuccessPolicy, _m_eFailurePolicy);

    BTBCMD command = _BTBuilder.m_aCommands.Dequeue();

    while (command.GetKey() != BTBCMD_KEY.kReturn)
    {

      if (command.GetKey() == BTBCMD_KEY.kEnd)
      {

        GD.PrintErr("BTBuilder SINTAX Error: End of commands reached.");

        return parallel;

      }

      parallel.AddChild(command.Exec(_BTBuilder));

      command = _BTBuilder.m_aCommands.Dequeue();

    }

    return parallel;

  }

  public BTBCMD_KEY
  GetKey()
  {

    return BTBCMD_KEY.kParallel;

  }

  private OPERATION_POLICY _m_eSuccessPolicy;

  private OPERATION_POLICY _m_eFailurePolicy;

}
