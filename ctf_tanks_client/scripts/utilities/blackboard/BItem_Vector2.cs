using Godot;

public class BItem_Vector2
: BItem
{

  public BItem_Vector2()
  {

    v2Value = new Vector2();

    return;

  }

  public override VARIABLE_TYPE
  GeType()
  {
    return VARIABLE_TYPE.kVector2;
  }

  public Vector2 v2Value;

}
