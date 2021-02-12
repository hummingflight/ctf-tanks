using Godot;

public class CmpBehaviorTreeKinematic
  : Component<KinematicBody>
{

  public CmpBehaviorTreeKinematic(BehaviorTree _behaviorTree)
  {

    _m_behaviorTree = _behaviorTree;

    return;

  }

  /// <summary>
  ///Called during the physics processing step of the main loop. Physics 
  ///processing means that the frame rate is synced to the physics, i.e. the 
  ///delta variable should be constant.
  /// </summary>
  /// <param name="_delta"></param>
  public override void
  _PhysicsProcess(float _delta)
  {

    _m_behaviorTree.Update(_m_actor);

    return;

  }

  public void 
  SetBehaviorTree(BehaviorTree _behaviorTree)
  {

    _m_behaviorTree = _behaviorTree;

    return;

  }

  public override COMPONENT_ID 
  GetID()
  {

    return COMPONENT_ID.kBehaviorTree;
  
  }


  private BehaviorTree _m_behaviorTree;

}
