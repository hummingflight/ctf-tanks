using Godot;

public class BlackboardVector2Item
: BlackboardItem
{

  public BlackboardVector2Item()
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
