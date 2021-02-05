public class ItemVectorNode<T>
{

  public ItemVectorNode(T _object)
  {

    m_item = _object;

    m_next = default;
    m_previous = default;

    return;

  } 

  public ItemVectorNode<T>
  GetNext()
  {

    return m_next;

  }

  public ItemVectorNode<T>
  GetPrevious()
  {

    return m_previous;

  }

  public void
  SetNext(ItemVectorNode<T> _next)
  {

    if (_next != default)
    {

      _next.m_previous = this;
      _next.m_next = m_next;

    }    

    if(m_next != default)
    {     
      
      m_next.m_previous = _next;

    }

    m_next = _next;    

    return;

  }

  public void
  SetPrevious(ItemVectorNode<T> _previous)
  {

    if (_previous != default)
    {

      _previous.m_previous = m_previous;
      _previous.m_next = this;

    }    

    if(m_previous != default)
    {     
     
      m_previous.m_next = _previous;

    }

    m_previous = _previous;    

    return;

  }

  public void
  Detach()
  {

    if(m_previous != default)
    {

      m_previous.m_next = m_next;

    }

    if(m_next != default)
    {

      m_next.m_previous = m_previous;

    }

    m_previous = default;
    m_next = default;

    return;

  }

  public T m_item;

  private ItemVectorNode<T> m_next;

  private ItemVectorNode<T> m_previous;

}
