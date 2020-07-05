using UnityEngine;

public class HealthSystem
{
    private int _health;
    private int _protection;

    public delegate void HitAction();
    public event HitAction HitEvent;

    public delegate void DieAction();
    public event DieAction DieEvent;

    public int Health
    {
        get => _health;
        set => _health = value;
    }
    public int Protection
    {
        get => _protection;
        set => _protection = value;
    }

    public HealthSystem (int health, int protection)
    {
        Health = health;
        Protection = protection;
    }

    public void GetDamage(int damage)
    {
        Health -= (int)(damage - Protection * 0.6f);
        HitEvent?.Invoke();

        if (Health <= 0)
            DieEvent?.Invoke();
    }
}
