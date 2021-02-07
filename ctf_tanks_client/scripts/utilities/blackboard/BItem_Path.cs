using Godot;

public class BItem_Path
: BItem
{

  public override VARIABLE_TYPE 
  GeType()
  {

    return VARIABLE_TYPE.kPath;

  }

  public ActiveItemVector<CTF.PathNode> m_vectorPathNode;

}
