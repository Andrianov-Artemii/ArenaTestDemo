using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapons
{
    [Header("Oher parameters")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Transform _muzzlePosition;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private ParticleSystem _shotEffect;

    protected override int Atack(int currShotCount, Transform player)
    {
        if (currShotCount > 0)
        {
            currShotCount--;
            _shotEffect.Play();
            GameObject newBullet = Instantiate(_bulletPrefab, _muzzlePosition.position, Quaternion.Euler(player.localEulerAngles));
            newBullet.GetComponent<Bullet>().Setup(_bulletSpeed, Damage, ShotRange, _blood);

        }

        return currShotCount;
    }
}
