using UnityEngine;

public class Character : Unit
{
    private float _walkingSpeed;
    private float _runningSpeed;

    public float WalkingSpeed { get { return _walkingSpeed; } private set { _walkingSpeed = value; } }
    public float RunningSpeed { get { return _runningSpeed; } private set { _runningSpeed = value; } }

    private FixedJoystick walkingJoystick;
    private FloatingJoystick atackingJoystick;

    private Vector3 moveSpeed;
    private float y;
    private bool hit;

    CharacterController controllerComponent;
    BarsSystem barsSystem;
    Animator animator;
    Weapon weapons;

    public Weapon Weapon
    {
        get => weapons;
        private set => weapons = value;
    }

    public void Setup(CharacterStats stats)
    {
        WalkingSpeed = stats.WalkingSpeed;
        RunningSpeed = stats.RunningSpeed;
        Health.Health = stats.Health;
        Health.Protection = stats.Protection;
    }

    private void Start()
    {
        controllerComponent = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Weapon = GetComponentInChildren<Weapon>();
        barsSystem = GetComponent<BarsSystem>();

        walkingJoystick = GameManager.Inctance.WalkingJoystick;
        atackingJoystick = GameManager.Inctance.AtackingJoystick;

        FloatingJoystick.AttackEvent += CharacterAttack;
        Health.DieEvent += Death;
    }

    private void Update()
    {
        ControllingAttackJoystick();
    }

    void FixedUpdate()
    {
        if (!Weapon.Attacked)
            CharacterWalk();
    }

    private void CharacterWalk()
    {
        moveSpeed.y = 0;

        float animSpeed = Mathf.Abs(walkingJoystick.Vertical) + Mathf.Abs(walkingJoystick.Horizontal);
        float speed = _walkingSpeed * animSpeed;

        animator.SetFloat("Speed", animSpeed);

        Vector3 direction = Vector3.forward * walkingJoystick.Vertical + Vector3.right * walkingJoystick.Horizontal;
        Vector3 target = Mathf.Clamp(speed, _walkingSpeed, _runningSpeed) * new Vector3(walkingJoystick.Horizontal, 0, walkingJoystick.Vertical).normalized;

        if (direction != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 720);

        moveSpeed = Vector3.MoveTowards(moveSpeed, target, Time.deltaTime * 300);

        controllerComponent.Move(moveSpeed * Time.deltaTime);
    }

    private void ControllingAttackJoystick()
    {
        LineRenderer attackArea = Weapon.AttackArea;

        if (Mathf.Abs(atackingJoystick.Horizontal) > 0.01f || Mathf.Abs(atackingJoystick.Vertical) > 0.01f)
        {

            if (!attackArea.gameObject.activeSelf)
                attackArea.gameObject.SetActive(true);

            ChangingAttackAreaPosition(attackArea);
        }
        else
            attackArea.gameObject.SetActive(false);
    }

    private void ChangingAttackAreaPosition(LineRenderer attackArea)
    {
        float attackRange = Weapon.AttackRange;

        RaycastHit hit;
        Transform atackTrailTransform = attackArea.transform;

        atackTrailTransform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);

        Vector3 direction = new Vector3(atackingJoystick.Horizontal + transform.position.x, 0, atackingJoystick.Vertical + transform.position.z);

        atackTrailTransform.LookAt(new Vector3(direction.x, 0, direction.z));
        atackTrailTransform.eulerAngles = new Vector3(0, atackTrailTransform.eulerAngles.y, 0);
        attackArea.SetPosition(0, atackTrailTransform.position);

        if (Physics.Raycast(atackTrailTransform.position, atackTrailTransform.forward, out hit, attackRange))
        {
            attackArea.SetPosition(1, hit.point);
        }
        else
            attackArea.SetPosition(1, atackTrailTransform.position + atackTrailTransform.forward * attackRange);

        y = attackArea.transform.eulerAngles.y;
    }

    private void CharacterAttack()
    {
        transform.eulerAngles = new Vector3(0, y, 0);
        animator.SetTrigger("Shooting");
    }
    private void Death()
    {
        animator.SetBool("Death", true);
    }
}
