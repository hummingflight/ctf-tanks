public class TankBehaviorFactory
{

  public static BehaviorTree 
  LIGHT_TANK()
  {

    return new BTBuilder()
                .ActiveSelector()
                  .Sequence()
                    .Action(BT_ACTION.kCourse_Towards_Enemy_Base)
                    .Action(BT_ACTION.kGet_Navigation_Mesh)
                    .Action(BT_ACTION.kGet_Path_To_Destination)
                    .Action(BT_ACTION.kMove_To)
                    .Return()
                  .Return()
                .Build();

  }

}