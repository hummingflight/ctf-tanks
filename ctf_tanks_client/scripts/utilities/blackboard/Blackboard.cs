using Godot;
using System.Collections.Generic;

public class Blackboard
{

  public Blackboard()
  {

    // Create table of values.
    _m_hItems = new Dictionary<BLACKBOARD_ITEM, BItem>();

    return;

  }

  public OPERATION_RESULT
  AddItem(BLACKBOARD_ITEM _key, BItem _item)
  {

    if(!HasItem(_key))
    {

      _m_hItems.Add(_key, _item);

      return OPERATION_RESULT.kSuccess;

    }
    else
    {

      GD.Print("Blackboard already has a key : " + _key + ".");

      return OPERATION_RESULT.kFail;

    }

  }

  public T
  GetItem<T>(BLACKBOARD_ITEM _key)
  where T : BItem
  {

    if(HasItem(_key))
    {

      return _m_hItems[_key] as T;

    }
    else
    {

      GD.PrintErr("Key : " + _key.ToString() + " doesn't exists in Blackboard.");

      return null;

    }

  }

  public OPERATION_RESULT
  RemoveAndDestroyItem(BLACKBOARD_ITEM _key)
  {

    BItem item = RemoveItem(_key);

    if(item != null)
    {

      item.Destroy();

      return OPERATION_RESULT.kSuccess;

    }
    else
    {

      return OPERATION_RESULT.kFail;

    }

  }

  public BItem
  RemoveItem(BLACKBOARD_ITEM _key)
  {

    BItem item = GetItem<BItem>(_key);

    if(item != null)
    {

      _m_hItems.Remove(_key);

    }

    return item;

  }

  public bool
  HasItem(BLACKBOARD_ITEM _key)
  {

    return _m_hItems.ContainsKey(_key);

  }

  public void
  Clear()
  {

    _m_hItems.Clear();

    return;

  }

  public void
  Destroy()
  {

    Clear();

    _m_hItems = null;

    return;

  }

  private Dictionary<BLACKBOARD_ITEM, BItem> _m_hItems;

}
