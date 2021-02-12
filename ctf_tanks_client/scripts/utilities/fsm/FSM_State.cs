using System.Collections.Generic;

public class FSM_State<T,U>
{

  public FSM_State()
  {
    
    _m_aTransitions = new List<FSM_Transition<T, U>>();
    return;

  }


  public virtual NODE_STATUS
  Update(U _arg)
  {

    return NODE_STATUS.kFailure;

  }

  public virtual void
  OnEnter(U _arg)
  {

    return;

  }

  public virtual void
  OnExit(U _arg)
  {

    return;

  }

  public virtual STATE_ID
  GetID()
  {

    return STATE_ID.kUndefined;

  }

  public NODE_STATUS
  Tick(U _arg)
  {

    if(!CheckTransitions(_arg))
    {

      Update(_arg);

    }

    return NODE_STATUS.kRunning;

  }

  public void
  SetFSM(FSM<T,U> _fsm)
  {

    _m_fsm = _fsm;

    return;

  }

  protected bool
  CheckTransitions(U _arg)
  {

    foreach(FSM_Transition<T,U> transition in _m_aTransitions)
    {

      if(transition.IsValid(_arg))
      {

        // On transition callback.
        transition.OnTransition(_arg);

        // Change state.
        _m_fsm.SetActive(transition.GetNextState(), _arg);

        return true;

      }

    }

    return false;

  }

  /// <summary>
  /// Reference to the state machine of this state
  /// </summary>
  protected FSM<T, U> _m_fsm;

  /// <summary>
  /// List of transitions.
  /// </summary>
  protected List<FSM_Transition<T, U>> _m_aTransitions;

}
