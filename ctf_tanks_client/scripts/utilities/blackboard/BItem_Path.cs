using Godot;

public class BItem_Path
: BItem
{

  public override VARIABLE_TYPE 
  GeType()
  {

    return VARIABLE_TYPE.kPath;

  }

  public Vector3[] m_path;

}
