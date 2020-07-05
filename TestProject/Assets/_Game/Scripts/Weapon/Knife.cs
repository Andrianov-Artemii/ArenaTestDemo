using UnityEngine;

public class Knife : Weapon
{
    private void OnTriggerEnter(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();

        if (unit != null && Attacked)
        {
            ParticleSystem blood = Instantiate(_blood, other.transform);
            Destroy(blood.gameObject, 0.5f);

            unit.Health.GetDamage(_damage);
        }
    }
}
