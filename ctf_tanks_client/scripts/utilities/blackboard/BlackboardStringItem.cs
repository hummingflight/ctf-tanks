public class BlackboardStringItem
: BlackboardItem
{

  public override VARIABLE_TYPE 
  GeType()
  {
    return VARIABLE_TYPE.kString;
  }

  public string strValue = "";

}
