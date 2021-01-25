using Godot;

public class BlackboardVector3Item
: BlackboardItem
{

  public BlackboardVector3Item()
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