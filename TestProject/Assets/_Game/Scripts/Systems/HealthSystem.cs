using UnityEngine;

public class HealthSystem
{
    public float Health;
    public float Protection;

    public delegate void HitAction();
    public event HitAction HitEvent;

    public delegate void DieAction();
    public event DieAction DieEvent;
    

    public HealthSystem (float health, float protection)
    {
        Health = health;
        Protection = protection;
    }

    public void GetDamage(float damage)
    {
        Health -= damage - Protection * 0.6f;
        HitEvent?.Invoke();

        if (Health <= 0)
            DieEvent?.Invoke();
    }
}
