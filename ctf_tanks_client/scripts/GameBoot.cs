using Godot;

public class GameBoot
: Node
{

  public override void
  _Ready()
  {

    // Prepare master manager.
    MasterManager.Prepare();

    _m_master = MasterManager.GetInstance();

    return;

  }

  private MasterManager _m_master;

}