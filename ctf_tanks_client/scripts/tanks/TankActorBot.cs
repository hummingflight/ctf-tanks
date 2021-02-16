using Godot;

public class TankActorBot
  : KinematicActor
{

  /**********************************************/
  /* Public                                     */
  /**********************************************/  

  override public void
  _Create()
  {

    ////////////////////////////////////////////
    // Blackboard

    Blackboard blackboard = _m_actor.m_blackboard;

    blackboard.AddItem(BLACKBOARD_ITEM.kShootSignal, new BItem());
    blackboard.AddItem(BLACKBOARD_ITEM.kTurret_Rotation, new BItem());
    blackboard.AddItem(BLACKBOARD_ITEM.kTank_Steering, new BItem());
    blackboard.AddItem(BLACKBOARD_ITEM.kAcceleration_Strength, new BItem());
    blackboard.AddItem(BLACKBOARD_ITEM.kReverse_Strength, new BItem());
    blackboard.AddItem(BLACKBOARD_ITEM.kFire_Button, new BItem());
    blackboard.AddItem(BLACKBOARD_ITEM.kPath, new BItem_Path());
    blackboard.AddItem(BLACKBOARD_ITEM.kDestination, new BItem_Vector3());
    blackboard.AddItem(BLACKBOARD_ITEM.kNavigation, new BItem_NavigationMesh());
    blackboard.AddItem(BLACKBOARD_ITEM.kEnemy, new BItem_KinematicActor());

    ////////////////////////////////////////////
    // Components

    _m_actor.AddComponent(new CmpTankProperties());
    _m_actor.AddComponent(new CmpContactSensors());
    _m_actor.AddComponent(new CmpBehaviorTreeKinematic(TankBehaviorFactory.LIGHT_TANK()));
    _m_actor.AddComponent(new CmpTankPhysicsDebug());
    _m_actor.AddComponent(new CmpTankVisionDebug());
    _m_actor.AddComponent(new CmpTurretControllerDebug());
    _m_actor.AddComponent(new CmpBulletSpawner());

    return;

  }

  override public void
  _Ready()
  {

    ////////////////////////////////////////////
    // Get Reference to Children

    CmpTurretController turret 
      = _m_actor.GetComponent<CmpTurretController>(COMPONENT_ID.kTurretController);

    Spatial turretNode = GetNode<Spatial>("TankRoot/Cartoon_TL_Base/Turret");

    turret.SetTurretNode(turretNode);

    ////////////////////////////////////////////
    // Teams

    // Set properties

    CmpTankProperties properties 
      = _m_actor.GetComponent<CmpTankProperties>(COMPONENT_ID.kTankProperties);

    properties.m_teamKey = m_team;

    MasterManager master = MasterManager.GetInstance();

    GameManager gameManager = master.GAME_MANAGER;

    Team team = gameManager.m_teamsManagers.GetTeam(m_team);

    team.AddMember(_m_actor.GetNode().Name, _m_actor);

    // Active Tank

    if(m_activeTank)
    {

      gameManager.SetActiveTank(_m_actor);

    }

    ////////////////////////////////////////////
    // Ready

    _m_actor._Ready();

    return;

  }

  override public void
  _Process(float _delta)
  {

    _m_actor._Process(_delta);

    return;

  }

  override public void
  _PhysicsProcess(float _delta)
  {

    _m_actor._PhysicsProcess(_delta);

    return;

  }

  [Export]
  public TEAM_KEY m_team;

  [Export]
  public bool m_activeTank = false;  

}