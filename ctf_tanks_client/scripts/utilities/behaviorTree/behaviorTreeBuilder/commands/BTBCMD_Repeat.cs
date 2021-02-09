using Godot;

public class BTBCMD_Repeat
: BTBCMD_Decorator
{

  public BTBCMD_Repeat(int _count)
  {

    _m_count = _count;

    return;

  }

  public override BehaviorNode 
  Instantiate(BehaviorNode _child)
  {

    RepeatNode repeat = new RepeatNode(_child);

    repeat.SetCount(_m_count);

    return repeat;
  
  }

  protected int _m_count;
}
