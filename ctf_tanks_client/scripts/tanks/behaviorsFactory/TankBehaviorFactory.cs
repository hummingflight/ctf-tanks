public class TankBehaviorFactory
{

  public static BehaviorTree 
  LIGHT_TANK()
  {

    return new BTBuilder()
                .ActiveSelector()
                  .Sequence()
                    .Reverse()
                      .Action(BT_ACTION.kHas_Active_Path)
                      .Return()
                    .Action(BT_ACTION.kCourse_Towards_Enemy_Base)
                    .Action(BT_ACTION.kGet_Path_To_Destination)
                    .Return()
                  .ActiveSelector()
                    .Sequence()
                      .Action(BT_ACTION.kIs_Path_Node_In_DeadZone)
                      .Action(BT_ACTION.kStop)
                      .Action(BT_ACTION.kReverse_Alignment)
                      .Return()
                    .Sequence()
                      .Action(BT_ACTION.kMove_To)
                      .Action(BT_ACTION.kStop)
                      .Return()
                    .Return()
                  .Return()
                .Build();

  }

}