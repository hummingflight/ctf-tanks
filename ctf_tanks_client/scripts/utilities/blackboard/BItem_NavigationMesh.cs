using Godot;

public class BItem_NavigationMesh
  : BItem
{

  public override VARIABLE_TYPE 
  GeType()
  {

    return VARIABLE_TYPE.kNavigationMesh;
    
  }

  public Navigation m_navigationMesh;

}