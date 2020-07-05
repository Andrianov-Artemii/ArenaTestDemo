using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private int _health;
    private int _protection;

    private HealthSystem _healthSystem;

    private void Awake()
    {
        Health = new HealthSystem(_health, _protection);
    }

    public HealthSystem Health
    {
        get => _healthSystem;
        protected set => _healthSystem = value;
    }
}
