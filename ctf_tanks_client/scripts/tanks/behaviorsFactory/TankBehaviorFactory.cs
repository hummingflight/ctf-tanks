public class TankBehaviorFactory
{

  public static BehaviorTree 
  LIGHT_TANK()
  {

    return new BTBuilder()
                .ActiveSelector()
                  .Sequence()
                    .Action(BT_ACTION.kCourse_Towards_Enemy_Base)
                    .Action(BT_ACTION.kGet_Path_To_Destination)
                    .ActiveSelector()
                      .Sequence()
                      .Action(BT_ACTION.kIs_Path_Node_In_DeadZone)
                      .Action(BT_ACTION.kMove_To)
                      .Return()
                    .Return()
                  .Return()
                .Build();

  }

}