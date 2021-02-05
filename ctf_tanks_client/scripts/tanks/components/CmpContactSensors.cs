using Godot;
using System.Collections.Generic;

/// <summary>
/// Manage contact from ray casts or sensors with other bodies.
/// </summary>
public class CmpContactSensors
: Component<KinematicBody>
{

  /**********************************************/
  /* Public                                     */
  /**********************************************/
  
  public CmpContactSensors()
  {

    _m_hRaycast = new Dictionary<string, RayCast>();

    return;

  }

  public override COMPONENT_ID 
  GetID()
  {

    return COMPONENT_ID.kContactSensors;

  }

  /// <summary>
  /// Adds a new RayCast to this contact sensor.
  /// </summary>
  /// <param name="_name">RayCast name.</param>
  /// <param name="_rayCast">RayCast.</param>
  public void
  AddRayCast(string _name, RayCast _rayCast)
  {

    _m_hRaycast.Add(_name, _rayCast);

    return;

  }

  /// <summary>
  /// Get a RayCast by name.
  /// </summary>
  /// <param name="_name">RayCast name.</param>
  /// <returns></returns>
  public RayCast
  GetRayCast(string _name)
  {

    if(HasRayCast(_name))
    {
     
      return _m_hRaycast[_name];
    
    }
    else
    {

      GD.PrintErr("RayCast of name: " + _name + " does not exists in the contact sensor");

    }

    return null;

  }

  /// <summary>
  /// Indicates if a RayCast exists in this contact sensor.
  /// </summary>
  /// <param name="_name"></param>
  /// <returns></returns>
  public bool
  HasRayCast(string _name)
  {

    return _m_hRaycast.ContainsKey(_name);

  }

  /// <summary>
  /// Get a list with the position of the first object that each RayCast is 
  /// colliding with.
  /// </summary>
  /// <returns>List of objects position.</returns>
  public List<Vector3>
  GetCollidersPosition()
  {

    List<Vector3> aPosition = new List<Vector3>();

    foreach(KeyValuePair<string, RayCast> item in _m_hRaycast)
    {

      RayCast rayCast = item.Value;

      if(rayCast.IsColliding())
      {

        Spatial collider = rayCast.GetCollider() as Spatial;

        aPosition.Add(collider.Transform.origin);

      }
        
    }

    return aPosition;

  }

  /**********************************************/
  /* Private                                    */
  /**********************************************/

  /// <summary>
  /// Table of ray cast object.
  /// </summary>
  private Dictionary<string, RayCast> _m_hRaycast;

}