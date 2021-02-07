using Godot;

namespace CTF
{

  public class PathNode
  {

    /**********************************************/
    /* Public                                     */
    /**********************************************/

    public PathNode()
    {

      position = new Vector3();

      return;

    }

    public PathNode(Vector3 _position)
    {

      position = _position;

      return;

    }

    /// <summary>
    /// Indicates if the node position is in the Tank steering dead zone.
    /// </summary>
    /// <param name="_physics">Tank physics component.</param>
    /// <param name="_agleThreshold">Minimum angle to be in the dead zone</param>
    /// <returns>True if the node position is in the Tank steering dead zone, 
    /// otherwise returns False.</returns>
    public bool
    IsInSteeringDeadZone(CmpTankPhysics _physics, float _angleThreshold)
    {

      // Direction from tank position to node position.
      Vector3 toDestination = _physics.POSITION.DirectionTo(position);

      // Angle between toDestination and the tank direction vector.
      float angle = toDestination.AngleTo(_physics.DIRECTION);

      return angle > _angleThreshold;

    }

    /// <summary>
    /// Indicates if the node position is in the Tank steering safe zone.
    /// </summary>
    /// <param name="_physics">Tank physics component.</param>
    /// <param name="_agleThreshold">Maximum angle to be in the dead zone</param>
    /// <returns>True if the node position is in the Tank steering safe zone, 
    /// otherwise returns False.</returns>
    public bool
    IsInSteeringSafeZone(CmpTankPhysics _physics, float _angleThreshold)
    {

      // Direction from tank position to node position.
      Vector3 toDestination = _physics.POSITION.DirectionTo(position);

      // Angle between toDestination and the tank direction vector.
      float angle = toDestination.AngleTo(_physics.DIRECTION);

      return angle <= _angleThreshold;

    }

    /// <summary>
    /// Path node position.
    /// </summary>
    public Vector3 position;

  }

}


