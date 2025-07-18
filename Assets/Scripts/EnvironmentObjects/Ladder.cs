using System.Collections.Generic;
using UnityEngine;
public class Ladder : MonoBehaviour
{
    public BoxCollider blockCollider;
    [SerializeField] private float totalHeight;
    public float TotalHeight => totalHeight;
    public List<IClimber> climbers = new List<IClimber>();

    public void AddClimber(IClimber climber)
    {
        if (!climbers.Contains(climber))
        {
            climbers.Add(climber);
        }
    }

    public void RemoveClimber(IClimber climber)
    {
        if (climbers.Contains(climber))
        {
            climbers.Remove(climber);
        }
    }







}