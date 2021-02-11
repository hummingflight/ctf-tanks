using Godot;

public class KinematicActor
  : KinematicBody
{

  /**********************************************/
  /* Protected                                  */
  /**********************************************/

  /// <summary>
  /// Get the actor of this object.
  /// </summary>
  public Actor<KinematicBody>
  Actor
  {
    get
    {
      return _m_actor;
    }
  }

  /// <summary>
  /// Wrapped actor manager.
  /// </summary>
  protected Actor<KinematicBody> _m_actor;

}
