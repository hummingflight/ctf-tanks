using Godot;

/// <summary>
/// Base interface of any debugging command.
/// </summary>
public class DCMD
{

  /// <summary>
  /// Override this method to implement the drawing process
  /// </summary>
  /// <param name="_canvas">Drawing canvas node</param>
  public virtual void
  Exec(Control _canvas)
  {

    return;

  }

}
