using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponStats stats;

    [SerializeField] private Image _attackBar;
    [SerializeField] private Transform _attackBarPosition;

    protected float _damage;
    protected int _maxAttackCount;

    protected float _reloadTime;
    protected float _attackRange;
    protected float _attackDelay;

    [SerializeField] private LineRenderer _attackArea;
    [SerializeField] protected ParticleSystem _blood;

    protected int currAttackCount;
    private bool attacked;
    private float reloadTimer = 0, delayTimer = 0;

    Unit owner;

    private void Setup()
    {
        _damage = stats.Damage;
        _maxAttackCount = stats.MaxAttackCount;
        _reloadTime = stats.ReloadTime;
        _attackRange = stats.AttackRange;
        _attackDelay = stats.AttackDelay;
    }

    private void Start()
    {
        Setup();

        owner = GetComponentInParent<Unit>();

        currAttackCount = _maxAttackCount;
        FloatingJoystick.AttackEvent += OnAttack;

        owner.Bar.BarChanges(_attackBar, currAttackCount, _maxAttackCount);
    }

    public float AttackRange
    {
        get => _attackRange;
        protected set => _attackRange = value;
    }

    public int CurrAttackCount
    {
        get => currAttackCount;
        protected set => currAttackCount = value;
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
                owner.Bar.BarChanges(_attackBar, currAttackCount, _maxAttackCount);
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

    private void FixedUpdate()
    {
        owner.Bar.BarPosition(_attackBar, _attackBarPosition);
    }

    protected virtual void Attack() { }

    private void OnAttack()
    {
        if (currAttackCount > 0)
        {
            Attacked = true;
            currAttackCount--;
            owner.Bar.BarChanges(_attackBar, currAttackCount, _maxAttackCount);
            Attack();
        }
    }

    private void OnDestroy()
    {
        FloatingJoystick.AttackEvent -= OnAttack;
    }

}
