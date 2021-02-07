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

  public static Vector3
  Arrive
  (
    Vector3 _actualVelocity,
    Vector3 _destination,
    Vector3 _position,
    float _arriveRadius,
    float _arriveStrength
  )
  {

    Vector3 toDestination = _destination - _position;
    
    float distance = toDestination.Length();
    
    if(distance < _arriveRadius)
    {

      _arriveStrength *= (distance / _arriveRadius);

    }

    // Calculate desire velocity.
    Vector3 desireVelocity = toDestination.Normalized() * _arriveStrength;

    // Calculate steer force.
    return desireVelocity - _actualVelocity;
  }

}
