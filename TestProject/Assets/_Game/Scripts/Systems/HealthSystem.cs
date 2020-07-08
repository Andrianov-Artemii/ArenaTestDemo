using UnityEngine;

public class HealthSystem
{
    private float health;
    private float protection;

    public delegate void HitAction();
    public event HitAction HitEvent;

    public delegate void DieAction();
    public event DieAction DieEvent;

    public float Health
    {
        get => health;
        set => health = value;
    }
    public float Protection
    {
        get => protection;
        set => protection = value;
    }

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
