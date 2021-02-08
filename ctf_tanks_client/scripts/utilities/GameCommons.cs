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

  kSucess,

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

  kIs_Path_Node_In_DeadZone

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

  kRepeat,

  kAction

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

  kContactSensors

}

public enum BLACKBOARD_ITEM
{

  kTank_Steering,

  kAcceleration_Strength,

  kReverse_Strength,

  kFire_Button,

  kDestination,

  kPath,

  kNavigation

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