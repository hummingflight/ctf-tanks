using Godot;

public class STT_TargetTheEnemy
: FSM_State<BehaviorNode, Actor<KinematicBody>>
{

  public STT_TargetTheEnemy()
    : base()
  {

    _m_aTransitions.Add(new Trans_EnemyOutOfSight());

    return;

  }

  public override NODE_STATUS 
  Update(Actor<KinematicBody> _actor)
  {

    BItem_KinematicActor enemyItem
      = _actor.m_blackboard.GetItem<BItem_KinematicActor>(BLACKBOARD_ITEM.kEnemy);

    // Get the turret controller.
    CmpTurretController turret
      = _actor.GetComponent<CmpTurretController>(COMPONENT_ID.kTurretController);

    Vector3 turretPosition = turret.GLOBAL_POSITION;

    // Get the enemy position.
    Vector3 enemyPosition =  enemyItem.ACTOR.GetNode().Transform.origin;

    // Fix enemy position height.
    enemyPosition.y = turretPosition.y;

    // Direction to enemy position.
    Vector3 toEnemy = turretPosition.DirectionTo(enemyPosition);

    float a = turret.GLOBAL_DIRECTION.AngleTo(toEnemy);
    float b = turret.FIRE_OPENING_ANGLE * 0.5f;    

    if(a < b)
    {

      // Enemy in fire zone, shoot!
      //_m_fsm.SetActive(STATE_ID.kShoot, _actor);

      BItem turretRotation
        = _actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kTurret_Rotation);

      turretRotation.fValue = 0.0f;

    }
    else
    {

      // Get the rotation strength.
      float rotationValue = Mathf.Min(1.0f, a / b);

      // Get the rotation direction.
      Vector3 xVector = _actor.
                        GetNode()
                        .GlobalTransform
                        .basis
                        .x;

      rotationValue *= Mathf.Sign(xVector.Dot(toEnemy));

      BItem turretRotation
      = _actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kTurret_Rotation);

      turretRotation.fValue = rotationValue;

    }

    return NODE_STATUS.kRunning;
    
  }

  public override void 
  OnExit(Actor<KinematicBody> _actor)
  {

    BItem turretRotation
     = _actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kTurret_Rotation);

    turretRotation.fValue = 0.0f;

    return;
    
  }

  public override STATE_ID 
  GetID()
  {

    return STATE_ID.kTargetTheEnemy;
    
  }

}
