using UnityEngine;

public class Pistol : Weapon
{
    [Header("Oher parameters")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _destroyBulletTime;
    [SerializeField] private Transform _muzzlePosition;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private ParticleSystem _shotEffect;

    protected override void Attack()
    {
        _shotEffect.Play();
        GameObject newBullet = Instantiate(_bulletPrefab, _muzzlePosition.position, Quaternion.Euler(GameManager.Inctance.Player.transform.eulerAngles));
        newBullet.GetComponent<Bullet>().Setup(_bulletSpeed, _damage, _destroyBulletTime, _blood);
    }
}
