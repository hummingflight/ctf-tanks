using Godot;

public class SteerForce
{

  public static Vector3
  Seek
  (
    Vector3 _actualVelocity,
    float _maxSpeed,
    Vector3 _destination,
    Vector3 _position,
    float _seekLength
  )
  {

    // Calculate desire velocity.
    Vector3 vector = _position.DirectionTo(_destination) * _maxSpeed;

    // Calculate steer force.
    return (vector - _actualVelocity).Normalized() * _seekLength;
  }

}
