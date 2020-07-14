using UnityEngine;

public class Knife : Weapon
{
    [SerializeField] private AudioClip clip;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<Unit>() && Attacked)
        {
            GetComponent<AudioSource>().PlayOneShot(clip);     
            Unit unit = other.GetComponent<Unit>();

            ParticleSystem blood = Instantiate(_blood, other.transform);
            Destroy(blood.gameObject, 0.5f);

            unit.Health.GetDamage(_damage);
        }
    }
}
