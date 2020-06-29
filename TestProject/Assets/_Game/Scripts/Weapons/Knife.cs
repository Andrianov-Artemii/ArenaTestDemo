using UnityEngine;

public class Knife : Weapons
{
    protected override int Atack(int currShotCount, Transform player)
    {
        if (currShotCount >= 0)
            currShotCount--;

        return currShotCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthManager healthManager = other.GetComponent<HealthManager>();

        if (healthManager != null && Hit)
        {
            ParticleSystem blood = Instantiate(_blood, other.transform);
            Destroy(blood.gameObject, 0.5f);

            healthManager.TakeDamage(Damage);
        }
    }
}
