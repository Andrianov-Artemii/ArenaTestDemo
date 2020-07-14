using System;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] protected Image _healthBar;
    [SerializeField] protected Transform _healthBarPosition;

    private HealthSystem healthSystem;
    private BarSystem barSystem;

    protected float maxHealth;

    private void Awake()
    {
        Health = new HealthSystem( 0, 0);
        Bar = new BarSystem();

        Health.HitEvent += ChangesHealthBar;
        Health.DieEvent += Death;
    }

    public HealthSystem Health
    {
        get => healthSystem;
        protected set => healthSystem = value;
    }

    public BarSystem Bar
    {
        get => barSystem;
        protected set => barSystem = value;
    }

    protected virtual void FixedUpdate()
    {
        Bar.BarPosition(_healthBar, _healthBarPosition);
    }

    protected void ChangesHealthBar()
    {
        Bar.BarChanges(_healthBar, Health.Health, maxHealth);
    }

    protected virtual void OnDestroy()
    {
        Health.HitEvent -= ChangesHealthBar;
        Health.DieEvent -= Death;
    }

    protected virtual void Death() 
    {
        Destroy(gameObject, 5f);
    }
}
