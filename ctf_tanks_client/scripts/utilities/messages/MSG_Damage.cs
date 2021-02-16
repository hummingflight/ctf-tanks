using Godot;

public class MSG_Damage
: IMessage
{

  /// <summary>
  /// Damage points.
  /// </summary>
  public int m_damagePoints;

  /// <summary>
  /// The object that infringed the damage.
  /// </summary>
  public Node m_object;

}
