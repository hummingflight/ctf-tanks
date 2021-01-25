public enum GAME_MANAGER
{

  kUndefined = 0,

  kConnectionManager

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

public enum COMPONENT_ID
{

  /// <summary>
  /// Undefined component.
  /// </summary>
  kUndefined,

  kTankInput,

  kTankPhysics

}

public enum BLACKBOARD_ITEM
{

  kTank_Steering,

  kAcceleration_Strength,

  kReverse_Strength


}

public enum VARIABLE_TYPE
{

  kNumber,

  kInt,

  kFloat,

  kString,

  kVector2,

  kVector3

}

public enum MESSAGE_ID
{

}