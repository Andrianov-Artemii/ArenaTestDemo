using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [Header("Main parameters")]
    [SerializeField] private int _damage;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _shotCount;
    [SerializeField] private float _shotRange;
    [SerializeField] private float _atackDelay;
    [SerializeField] private LineRenderer _atackArea;
    [SerializeField] protected ParticleSystem _blood;

    private bool hit;

    public bool Hit { get { return hit; }  set { hit = value; } }

    public int Damage { get { return _damage; } private set { _damage = value; } }
    public int ShotCount { get { return _shotCount; } private set { _shotCount = value; } }
    public float ShotRange { get { return _shotRange; } private set { _shotRange = value; } }
    public float ReloadTime { get { return _reloadTime; } private set { _reloadTime = value; } }
    public float AtackDelay { get { return _atackDelay; } private set { _atackDelay = value; } }
    public LineRenderer AtackArea { get { return _atackArea; } private set { _atackArea = value; } }

    public int ToAtack(int currShotCount, Transform player)
    {
        return Atack(currShotCount, player);
    }
    protected virtual int Atack(int currShotCount, Transform player)
    {
        return 0;
    }
}
