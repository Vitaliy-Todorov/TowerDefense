using System;

public interface IHealthData
{
    public event Action<float> UpdateHealth;
    public event Action<float> UpdateRestoringHealth;
    
    public float Health { get; set; }
    public float RestoringHealth  { get; set; }
}