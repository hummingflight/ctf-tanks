using Godot;

public class Action_SelectEnemy
  : BehaviorNode
{

  public override NODE_STATUS 
  Update(Actor<KinematicBody> _actor)
  {

    return NODE_STATUS.kFailure;
  
  }

}