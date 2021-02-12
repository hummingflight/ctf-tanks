using System.Collections.Generic;

public class FSM<T,U>
{

  public FSM(T _parent)
  {

    _m_parent = _parent;

    _m_hStates = new Dictionary<STATE_ID, FSM_State<T, U>>();

    _m_activeState = new FSM_State<T, U>();

    return;

  }

  public NODE_STATUS
  Update(U _arg)
  {

    return _m_activeState.Tick(_arg);

  }

  public void
  SetActive(STATE_ID _state, U _arg)
  {

    if(_m_hStates.ContainsKey(_state))
    {

      _m_activeState.OnExit(_arg);

      _m_activeState = _m_hStates[_state];

      _m_activeState.OnEnter(_arg);

    }

    return;

  }

  public void 
  Add(FSM_State<T,U> _state)
  {

    _state.SetFSM(this);

    _m_hStates.Add(_state.GetID(), _state);

    return;

  }

  public FSM_State<T,U>
  GetState(STATE_ID _state)
  {

    return _m_hStates[_state];

  }

  public bool
  HasState(STATE_ID _state)
  {

    return _m_hStates.ContainsKey(_state);

  }

  public void
  RemoveState(STATE_ID _state)
  {

    _m_hStates.Remove(_state);
    return;

  }

  public void
  Clear()
  {

    _m_hStates.Clear();
    return;

  }

  public T
  PARENT
  {
    get
    {
      return _m_parent;
    }
  }

  public FSM_State<T,U>
  ACTIVE_STATE
  {
    get
    {
      return _m_activeState;
    }
  }

  protected FSM_State<T, U> _m_activeState;

  protected T _m_parent;

  protected Dictionary<STATE_ID, FSM_State<T,U>> _m_hStates;

}