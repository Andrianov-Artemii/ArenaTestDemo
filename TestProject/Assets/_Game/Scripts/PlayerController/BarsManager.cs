using UnityEngine;
using UnityEngine.UI;

public class BarsManager : MonoBehaviour
{
    [SerializeField] private Image _healthBar, _shotBar;

    private void LateUpdate()
    {
        if (_healthBar != null)
            _healthBar.transform.LookAt(Camera.main.transform);

        if (_shotBar != null)
            _shotBar.transform.LookAt(Camera.main.transform);
    }

    public void HealthBarChanges(int currHealth, int maxHealth)
    {
        _healthBar.fillAmount = (float)currHealth / maxHealth;
    }

    public void ShotBarChanges(int currShotCount, int maxShotCount)
    {
        _shotBar.fillAmount = (float)currShotCount / maxShotCount;
    }
}
