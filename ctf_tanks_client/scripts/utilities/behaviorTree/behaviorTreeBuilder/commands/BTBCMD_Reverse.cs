public class BTBCMD_Reverse
: BTBCMD_Decorator
{

  public override BehaviorNode 
  Instantiate(BehaviorNode _child)
  {

    return new ReverseNode(_child);
  
  }

}