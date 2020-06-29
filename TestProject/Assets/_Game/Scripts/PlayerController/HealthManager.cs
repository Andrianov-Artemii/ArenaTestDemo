using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    public UnityEvent DeathEvent;
    [SerializeField] private int _health;
    [SerializeField] private int _protection;
    private int currHealth;

    BarsManager barsManager;

    public int Health { get { return _health; } private set { _health = value;  } }
    public int Protection { get { return _protection; } private set { _protection = value;  } }
    private void Start()
    {
        barsManager = GetComponent<BarsManager>();
        currHealth = Health;
    }

    public void TakeDamage(int damage)
    {
        currHealth -= (int)(damage - _protection * 0.6f);
        barsManager.HealthBarChanges(currHealth, Health);

        if (currHealth <= 0)
            DeathEvent.Invoke();
    }
}
