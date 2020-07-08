using UnityEngine;

public class Knife : Weapon
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Unit>() && Attacked)
        {
            Unit unit = other.GetComponent<Unit>();

            ParticleSystem blood = Instantiate(_blood, other.transform);
            Destroy(blood.gameObject, 0.5f);

            unit.Health.GetDamage(_damage);
        }
    }
}
