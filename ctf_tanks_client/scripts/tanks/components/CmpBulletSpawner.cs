using Godot;

public class CmpBulletSpawner
  : Component<KinematicBody>
{

  public override void 
  _Ready()
  {

    MasterManager master = MasterManager.GetInstance();

    _m_bulletManager 
      = master.GetManager<BulletManager>(MANAGER_KEY.kBulletManager);

    return;
  
  }

  public override void 
  _PhysicsProcess(float _delta)
  {

    if(!_m_ready)
    {

      _m_time += _delta;

      if(_m_time > _m_reloadTime)
      {

        _m_time = 0.0f;
        _m_ready = !_m_ready;

      }

    }

    _UpdateShoot();

    return;
    
  }

  private void
  _UpdateShoot()
  {

    if(_m_ready)
    {

      BItem shootSignal =
        _m_actor.m_blackboard.GetItem<BItem>(BLACKBOARD_ITEM.kShootSignal);

      if (shootSignal.iValue > 0)
      {

        CmpTurretController turret 
          = _m_actor.GetComponent<CmpTurretController>(COMPONENT_ID.kTurretController);

        Transform turretTransform = turret.GLOBAL_TRANSFORM;

        Vector3 bulletDirection = turretTransform.basis.z;

        Vector3 bulletPosition = turret.BULLET_SPAWN_POSITION.origin;

        if(Shoot(bulletPosition, bulletDirection * 150.0f, "pool_name"))
        {

          _m_ready = !_m_ready;

        }

      }

    }

    return;

  }

  public bool
  Shoot(Vector3 _position, Vector3 _velocity, string _poolName)
  {

    BulletPool pool = _m_bulletManager.GetPool(_poolName);

    if(pool != null)
    {

      Bullet bullet = pool.Get();

      if(bullet != null)
      {

        bullet.Enable(_position, _velocity);

        return true;

      }
      else
      {

        return false;
      
      }

    }
    else
    {

      GD.PrintErr("Pool: " + _poolName + " does not exists.");

    }

    return false;

  }

  private bool _m_ready = true;

  private float _m_reloadTime = 3.0f;

  private float _m_time = 1.0f;

  private BulletManager _m_bulletManager;

}