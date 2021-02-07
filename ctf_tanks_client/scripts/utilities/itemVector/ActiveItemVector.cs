public class ActiveItemVector <T> 
: ItemVector<T> where T : class
{

  public void
  Start()
  {

    _m_active = GetFirst();

    return;

  }

  public ItemVectorNode<T>
  Next()
  {

    ItemVectorNode<T> next = _m_active.GetNext();

    if(next != null)
    {

      _m_active = next;

    }

    return _m_active;

  }

  public ItemVectorNode<T>
  Prev()
  {

    ItemVectorNode<T> prev = _m_active.GetPrevious();

    if (prev != null)
    {

      _m_active = prev;

    }

    return prev;

  }

  public void
  SetActive(ItemVectorNode<T> _active)
  {

    _m_active = _active;

    return;

  }

  public ItemVectorNode<T>
  ACTIVE
  {
    get
    {
      return _m_active;
    }
  }

  /// <summary>
  /// The Active node.
  /// </summary>
  protected ItemVectorNode<T> _m_active;

}
