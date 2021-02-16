using Godot;

public class KinematicActor
  : KinematicBody
{

  public KinematicActor()
  : base()
  {

    // Create actor.
    _m_actor = new Actor<KinematicBody>(this);

    // On create callback.
    _Create();

    return;

  }

  public virtual void
  _Create()
  {

    return;

  }

  public override void 
  _Ready()
  {
    
    return;

  }

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
