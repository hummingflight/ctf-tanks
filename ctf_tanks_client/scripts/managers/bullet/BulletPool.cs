using Godot;
using System.Collections.Generic;

public class BulletPool
: Node
{

  public override void 
  _Ready()
  {

    _m_aDisable = new List<Bullet>();

    // Add pool to bullet manager.

    MasterManager master = MasterManager.GetInstance();

    BulletManager bulletManager 
      = master.GetManager<BulletManager>(MANAGER_KEY.kBulletManager);

    bulletManager.AddPool(m_poolName, this);

    // Create bullets

    PackedScene pack = GD.Load<PackedScene>("res://prefabs/misc/Bullet.tscn");

    for(int index = 0; index < m_size; ++index)
    {

      // Instantiate.
      Bullet bullet = pack.Instance() as Bullet;

      // Add bullet to the tree.
      AddChild(bullet);

      // Set the bullet pool.
      bullet.SetPool(this);

      // Disable bullet.
      bullet.Disable();      

    }

    return;

  }

  /// <summary>
  /// Get a bullet. Returns null if there's no bullet available.
  /// </summary>
  /// <returns></returns>
  public Bullet
  Get()
  {

    if(_m_aDisable.Count > 0)
    {

      // Get bullet.
      Bullet bullet = _m_aDisable[0];

      // Remove it from list.
      _m_aDisable.RemoveAt(0);

      return bullet;

    }

    return null;

  }

  /// <summary>
  /// Add a bullet to the disable list.
  /// </summary>
  /// <param name="_bullet"></param>
  public void
  AddToDisable(Bullet _bullet)
  {

    _m_aDisable.Add(_bullet);
    
    return;

  }

  /// <summary>
  /// The size of the bullet pool.
  /// </summary>
  [Export]
  public uint m_size = 0;

  /// <summary>
  /// The name of this bullet pool.
  /// </summary>
  [Export]
  public string m_poolName = "pool_name";

  /// <summary>
  /// List of disable bullets.
  /// </summary>
  protected List<Bullet> _m_aDisable;

  /// <summary>
  /// Create a bullet.
  /// </summary>
  /// <returns></returns>
  private Bullet
  _CreateBullet()
  {

    PackedScene pack = GD.Load<PackedScene>("res://prefabs/misc/Bullet.tscn");

    // Instance.
    return pack.Instance() as Bullet;

  }

}