using Godot;

class BItem_KinematicActor
: BItem
{

  /// <summary>
  /// Get the reference to the actor.
  /// </summary>
  public Actor<KinematicBody>
  ACTOR
  {
    get
    {
      return _m_actor;
    }
    set
    {
      _m_actor = value;
    }
  }

  private Actor<KinematicBody> _m_actor;

}
