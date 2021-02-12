using Godot;

public enum MANAGER_KEY
{

  kUndefined = 0,

  kGameManager,

  kDebugManager,

  kCameraManager

}

public enum TEAM_KEY
{

  kRed,

  kBlue

}

public enum NODE_STATUS
{

  kInvalid,

  kSuccess,

  kFailure,

  kRunning,

  kAborted

}

public enum OPERATION_POLICY
{

  KRequiereOne,

  kRequireAll

}

public enum BT_ACTION
{

  kCourse_Towards_Enemy_Base,

  kGet_Path_To_Destination,

  kMove_To,

  kIs_Path_Node_In_DeadZone,

  kStop,

  kReverse_Alignment,

  kHas_Active_Path,

  kIs_The_Enemy_In_Sight,

  kShoot_The_Enemy,

  kSelect_Enemy

}

public enum OPERATION_RESULT
{

  /// <summary>
  /// The operation was successful.
  /// </summary>
  kSuccess,

  /// <summary>
  /// The operation failed.
  /// </summary>
  kFail,

  /// <summary>
  /// The operation had an unexpected error.
  /// </summary>
  kError

}

public enum BTBCMD_KEY
{

  kEnd,

  kReturn,

  kComposite,

  kSequence,

  kSelector,

  kActiveSelector,

  kParallel,

  kAction,

  kDecorator

}

public enum COMPONENT_ID
{

  /// <summary>
  /// Undefined component.
  /// </summary>
  kUndefined,

  kTankInput,

  kTankPhysics,

  kTankProperties,

  kContactSensors,

  kTankVision,

  kBehaviorTree,

  kTurretController

}

public enum STATE_ID
{

  kUndefined,

  kStopShooting,

  kTargetTheEnemy,

  kShoot,

  kReload

}

public enum BLACKBOARD_ITEM
{

  kTurret_Rotation,

  kTank_Steering,

  kAcceleration_Strength,

  kReverse_Strength,

  kFire_Button,

  kDestination,

  kPath,

  kNavigation,

  kEnemy

}

public enum VARIABLE_TYPE
{

  kNumber,

  kInt,

  kFloat,

  kString,

  kVector2,

  kVector3,

  kPath,

  kNavigationMesh

}

public enum MESSAGE_ID
{

  KActive_Camera

}

public enum CAMERA_MANAGER_STATE
{

  KTop_orthographic,

  kBehind_active_tank

}