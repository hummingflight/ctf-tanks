using Godot;

public class BehaviorNode
{

  public virtual NODE_STATUS
  Update(Actor<KinematicBody> _actor)
  {

    return NODE_STATUS.kInvalid;

  }

  public virtual void
  OnInit(Actor<KinematicBody> _actor)
  {

    return;

  }

  public virtual void
  OnTerminate(Actor<KinematicBody> _actor, NODE_STATUS status)
  {

    return;

  }

  public virtual NODE_STATUS
  Tick(Actor<KinematicBody> _actor)
  {

    if(_m_status != NODE_STATUS.kRunning)
    {

      OnInit(_actor);

    }

    _m_status = Update(_actor);

    if(_m_status != NODE_STATUS.kRunning)
    {

      OnTerminate(_actor, _m_status);

    }

    return _m_status;

  }

  public void
  Reset()
  {

    _m_status = NODE_STATUS.kInvalid;

    return;

  }

  public void
  Abort(Actor<KinematicBody> _actor)
  {

    _m_status = NODE_STATUS.kAborted;

    OnTerminate(_actor, _m_status);

    return;

  }

  public bool
  IsTerminated()
  {

    return _m_status == NODE_STATUS.kSuccess || _m_status == NODE_STATUS.kFailure;

  }

  public bool
  IsRunning()
  {

    return _m_status == NODE_STATUS.kRunning;

  }

  public NODE_STATUS
  GetStatus()
  {

    return _m_status;

  }

  protected NODE_STATUS _m_status;

}
