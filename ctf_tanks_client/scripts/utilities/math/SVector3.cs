using Godot;

public class SVector3
{

  public static Vector3
  RotateByQuaternion(ref Vector3 _vector, ref Quat _quat)
  {

    Vector3 vQuat = new Vector3(_quat.x, _quat.y, _quat.z);
    float s = _quat.w;

    return 2.0f * vQuat.Dot(_vector)  * vQuat 
           + (s * s - vQuat.Dot(vQuat)) * _vector 
           + 2.0f * s * vQuat.Cross(_vector);

  }

  public static Vector3
  Copy(ref Vector3 _from, ref Vector3 _to)
  {

    _to.x = _from.x;
    _to.y = _from.y;
    _to.z = _from.z;

    return _to;

  }

  public static Vector3
  Substract(ref Vector3 _a, ref Vector3 _b, ref Vector3 _out)
  {

    _out.x = _a.x - _b.x;
    _out.y = _a.y - _b.y;
    _out.z = _a.z - _b.z;

    return _out;

  }

  public static Vector3
  Add(ref Vector3 _a, ref Vector3 _b, ref Vector3 _out)
  {

    _out.x = _a.x + _b.x;
    _out.y = _a.y + _b.y;
    _out.z = _a.z + _b.z;

    return _out;

  }

  public static Vector3
  Cross(ref Vector3 _a, ref Vector3 _b, ref Vector3 _out)
  {

    _out.x = _a.y * _b.z - _a.z * _b.y;
    _out.y = _a.z * _b.x - _a.x * _b.z;
    _out.z = _a.x * _b.y - _a.y * _b.x;

    return _out;

  }

  public static Vector3
  Normalized(ref Vector3 _in, ref Vector3 _out)
  {

    float mag = _in.Length();

    if(mag < 0.0001f)
    {

      _out.x = 0;
      _out.y = 0;
      _out.z = 0;

    }
    else
    {

      float mult = 1.0f / mag;

      _out.x = _in.x * mult;
      _out.y = _in.y * mult;
      _out.z = _in.z * mult;

    }    

    return _out;

  }

  public static Vector3
  Normalize(ref Vector3 _vector)
  {

    return Normalized(ref _vector, ref _vector);

  }

  public static Vector3
  MaxLengthLimit( ref Vector3 _vector, float _maxLength)
  {

    if(_vector.Length() > _maxLength)
    {

      return _vector.Normalized() * _maxLength;

    }

    return _vector;

  }

}