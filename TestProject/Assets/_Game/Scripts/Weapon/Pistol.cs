using UnityEngine;

public class Pistol : Weapon
{
    [Header("Oher parameters")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _destroyBulletTime;
    [SerializeField] private Transform _muzzlePosition;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private ParticleSystem _shotEffect;
    [SerializeField] private AudioClip clip;
    protected override void Attack()
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
        _shotEffect.Play();
        GameObject newBullet = Instantiate(_bulletPrefab, _muzzlePosition.position, Quaternion.Euler(GameManager.Inctance.Player.transform.eulerAngles));
        newBullet.GetComponent<Bullet>().Setup(_bulletSpeed, _damage, _destroyBulletTime, _blood);
    }
}
