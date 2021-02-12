public class FSM_Transition<T,U>
{

  public virtual bool
  IsValid(U _args)
  {

    return false;

  }

  public virtual STATE_ID
  GetNextState()
  {

    return STATE_ID.kUndefined;

  }

  public virtual void
  OnTransition(U _args)
  {

    return;

  }

}
