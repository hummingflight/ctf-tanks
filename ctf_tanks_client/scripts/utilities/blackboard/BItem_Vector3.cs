using Godot;

public class BItem_Vector3
: BItem
{

  public BItem_Vector3()
  {

    v3Value = new Vector3();

    return;

  }

  public override VARIABLE_TYPE
  GeType()
  {
    return VARIABLE_TYPE.kVector3;
  }

  public Vector3 v3Value;

}