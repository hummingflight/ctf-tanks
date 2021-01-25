using Godot;
using System.Collections.Generic;

public class Blackboard
{

  public Blackboard()
  {

    // Create table of values.
    _m_hItems = new Dictionary<BLACKBOARD_ITEM, BlackboardItem>();

    return;

  }

  public OPERATION_RESULT
  AddItem(BLACKBOARD_ITEM _key, BlackboardItem _item)
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

  public BlackboardItem
  GetItem(BLACKBOARD_ITEM _key)
  {

    if(HasItem(_key))
    {

      return _m_hItems[_key];

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

    BlackboardItem item = RemoveItem(_key);

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

  public BlackboardItem
  RemoveItem(BLACKBOARD_ITEM _key)
  {

    BlackboardItem item = GetItem(_key);

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

  private Dictionary<BLACKBOARD_ITEM, BlackboardItem> _m_hItems;

}
