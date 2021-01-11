using Godot;
using System;

public class LightTankTurret : Spatial
{

  public override void 
  _Ready()
  {

    return;
      
  }

  public override void
  _PhysicsProcess(float _delta)
  {

    _UpdateTorretRotation(_delta);

    return;

  }

  [Export] public float m_rotationSpeed = 2.0f;

  private void
  _UpdateTorretRotation(float _delta)
  {

    float value = Input.GetActionStrength("steer_torret_right")
                - Input.GetActionStrength("steer_torret_left");

    Rotate(-Transform.basis.y, value * m_rotationSpeed * _delta);

    return;

  }

}
