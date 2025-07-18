
using UnityEngine;

public interface IClimber
{
    public float ClimbSpeed { get; set; }
    public bool IsClimbing { get; set; }
    public bool CanClimb { get; set; }
    public Vector3 LastLadderSnapPoint { get; set; }
    public Vector3 LastLadderContactNormal { get; set; }
    public Vector3 LastLadderFaceContact { get; set; }
    public float CurrentHeightToGround { get; }
    public float HeightRequireToDive { get; }
    public float MinimumDiveVelocity { get; }
    public float ClimbProgress();
}

