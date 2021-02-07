public class ItemVector <T> where T : class
{

  /**********************************************/
  /* Public                                     */
  /**********************************************/
  
  public ItemVector()
  {

    _m_start = new ItemVectorNode<T>(default);
    _m_end = new ItemVectorNode<T>(default);

    _m_start.SetNext(_m_end);
    _m_end.SetPrevious(_m_start);

    _m_size = 0;

    return;

  }

  public ItemVector(T[] _itemArray)
  {

    _m_start = new ItemVectorNode<T>(default);
    _m_end = new ItemVectorNode<T>(default);

    _m_start.SetNext(_m_end);
    _m_end.SetPrevious(_m_start);

    _m_size = 0;

    foreach (T item in _itemArray)
    {

      AddAtEnd(item);  

    }

    return;

  }

  public ItemVectorNode<T>
  GetFirst()
  {

    return _m_start.GetNext();

  }

  public ItemVectorNode<T>
  GetLast()
  {

    return _m_end.GetPrevious();

  }

  public void
  AddAtEnd(T item)
  {

    _m_end.SetPrevious( new ItemVectorNode<T>(item));

    ++_m_size;

    return;

  }

  public void
  AddToStart(T item)
  {

    _m_start.SetNext(new ItemVectorNode<T>(item));

    ++_m_size;

    return;

  }

  public void
  Remove(T _item)
  {

    ItemVectorNode<T> node = _m_start.GetNext();

    while (node != _m_end)
    {

      if (node == default)
      {

        // ERROR.

        break;

      }

      if (node.m_item == _item)
      {

        ItemVectorNode<T> nextNode = node.GetNext();

        node.Detach();

        node = nextNode;

        --_m_size;

      }
      else
      {

        node = node.GetNext();

      }

    }

    return;

  }

  public void
  Remove(ItemVectorNode<T> _item)
  {

    ItemVectorNode<T> node = _m_start.GetNext();

    while (node != _m_end)
    {

      if (node == default)
      {

        // ERROR.

        break;

      }

      if(node == _item)
      {

        ItemVectorNode<T> nextNode = node.GetNext();

        node.Detach();

        node = nextNode;

        --_m_size;

      }
      else
      {

        node = node.GetNext();

      }      

    }

    return;

  }

  public void
  Clear()
  {

    ItemVectorNode<T> node = _m_start.GetNext();

    while(node != _m_end)
    {

      if(node == default)
      {

        // ERROR.

        break;

      }

      ItemVectorNode<T> nextNode = node.GetNext();

      node.Detach();

      node = nextNode;

    }

    _m_size = 0;

    return;

  }

  public ItemVectorNode<T> BEGIN
  {

    get
    {

      return _m_start;

    }

  }

  public ItemVectorNode<T> END
  {

    get
    {

      return _m_end;

    }

  }

  public int SIZE
  {

    get
    {

      return _m_size;

    }

  }

  /**********************************************/
  /* Protected                                  */
  /**********************************************/
  
  protected ItemVectorNode<T> _m_start;

  protected ItemVectorNode<T> _m_end;

  protected int _m_size;

}
