using Godot;
using System.Collections.Generic;

public class BulletManager
  : Manager
{

  public BulletManager()
  {

    _m_aPools = new Dictionary<string, BulletPool>();

    return;

  }
  
  public BulletPool
  GetPool(string _poolName)
  {

    if(HasPool(_poolName))
    {
      return _m_aPools[_poolName];
    }

    return null;

  }

  public  bool
  HasPool(string _poolName)
  {

    return _m_aPools.ContainsKey(_poolName);

  }


  public void
  AddPool(string _poolName, BulletPool _pool)
  {

    _m_aPools.Add(_poolName, _pool);

    return;

  }

  private Dictionary<string, BulletPool> _m_aPools;

}
