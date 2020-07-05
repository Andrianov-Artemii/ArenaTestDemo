using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class BarsSystem : MonoBehaviour
{
    [SerializeField] private Image _healthBar, _attackBar;
    [SerializeField] private Transform _healthBarPosition, _attackBarPosition;

    private int maxHealth;

    Unit unit;

    private void Start()
    {
        unit = GetComponent<Unit>();

        maxHealth = unit.Health.Health;

        unit.Health.HitEvent += HealthBarChanges;
    }

    private void FixedUpdate()
    {
        if (_healthBar != null)
            _healthBar.transform.position = Camera.main.WorldToScreenPoint(_healthBarPosition.position);

        if (_attackBar != null)
            _attackBar.transform.position = Camera.main.WorldToScreenPoint(_attackBarPosition.position);
    }

    public void HealthBarChanges()
    {
        _healthBar.fillAmount = (float)unit.Health.Health / maxHealth;
    }

    public void ShotBarChanges(int currShotCount, int maxShotCount)
    {
        _attackBar.fillAmount = (float)currShotCount / maxShotCount;
    }
}
