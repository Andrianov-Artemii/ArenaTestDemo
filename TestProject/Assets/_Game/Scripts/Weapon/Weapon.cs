using System.Collections;
using System.Reflection;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected int _damage;
    protected int _maxAttackCount;

    protected float _reloadTime;
    protected float _attackRange;
    protected float _attackDelay;

    [SerializeField] private LineRenderer _attackArea;
    [SerializeField] protected ParticleSystem _blood;

    protected int currAttackCount;
    private bool attacked;
    private float reloadTimer = 0, delayTimer = 0;

    BarsSystem barsSystem;
    
    public void Setup(WeaponStats stats)
    {
        _damage = stats.Damage;
        _maxAttackCount = stats.MaxAttackCount;
        _reloadTime = stats.ReloadTime;
        _attackRange = stats.AttackRange;
        _attackDelay = stats.AttackDelay;
    }

    private void Start()
    {
        currAttackCount = _maxAttackCount;
        FloatingJoystick.AttackEvent += OnAttack;

        barsSystem = GetComponentInParent<BarsSystem>();
        barsSystem.ShotBarChanges(currAttackCount, _maxAttackCount);
    }

    public float AttackRange
    {
        get => _attackRange;
        protected set => _attackRange = value;
    }


    public bool Attacked
    {
        get => attacked;
        protected set => attacked = value;
    }

    public LineRenderer AttackArea
    {
        get => _attackArea;
        protected set => _attackArea = value;
    }

    private void Update()
    {
        if (currAttackCount < _maxAttackCount)
        {
            reloadTimer += Time.deltaTime;

            if (reloadTimer >= _reloadTime)
            {
                reloadTimer = 0;
                currAttackCount++;
                barsSystem.ShotBarChanges(currAttackCount, _maxAttackCount);
            }
        }

        if (Attacked)
        {
            delayTimer += Time.deltaTime;

            if (delayTimer >= _attackDelay)
            {
                delayTimer = 0;
                Attacked = false;
            }
        }
    }

    protected virtual void Attack() { }

    private void OnAttack()
    {
        if (currAttackCount > 0)
        {
            currAttackCount--;
            barsSystem.ShotBarChanges(currAttackCount, _maxAttackCount); 
            Attacked = true;
            Attack();
        }
    }

}
