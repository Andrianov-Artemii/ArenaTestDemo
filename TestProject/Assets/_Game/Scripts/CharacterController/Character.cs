using UnityEngine;

public class Character : Unit
{
    [SerializeField] private CharacterStats stats;
    [SerializeField] private bool isEnemy;

    private float walkingSpeed;
    private float runningSpeed;

    private FixedJoystick walkingJoystick;
    private FloatingJoystick atackingJoystick;

    private Vector3 moveSpeed;
    private float y;

    CharacterController controllerComponent;
    Animator animator;
    Weapon weapon;

    private void Setup()
    {
        walkingSpeed = stats.WalkingSpeed;
        runningSpeed = stats.RunningSpeed;
        Health.Health = stats.Health;
        Health.Protection = stats.Protection;
    }

    private void Start()
    {
        Setup();

        maxHealth = Health.Health;
        ChangesHealthBar();

        controllerComponent = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        weapon = GetComponentInChildren<Weapon>();

        if (!isEnemy)
        {
            walkingJoystick = GameManager.Inctance.WalkingJoystick;
            atackingJoystick = GameManager.Inctance.AtackingJoystick;
        }

        FloatingJoystick.AttackEvent += CharacterAttack;
    }

    private void Update()
    {
        if (!isEnemy)
        {
            ControllingAttackJoystick();

            if (!weapon.Attacked)
                CharacterWalk();
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!isEnemy)
        {
            if (!weapon.Attacked && !isEnemy)
                CharacterWalk();
        }
    }

    private void CharacterWalk()
    {
        moveSpeed.y = 0;

#if UNITY_EDITOR
        float animSpeed = Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"));
        Vector3 direction = Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal");

#elif UNITY_ANDROID
        float animSpeed = Mathf.Abs(walkingJoystick.Vertical) + Mathf.Abs(walkingJoystick.Horizontal);
        Vector3 direction = Vector3.forward * walkingJoystick.Vertical + Vector3.right * walkingJoystick.Horizontal;

#endif

        float speed = walkingSpeed * animSpeed;
        animator.SetFloat("Speed", animSpeed);

        CharacterRotate(direction);

        moveSpeed = Mathf.Clamp(speed, walkingSpeed, runningSpeed) * direction * Time.deltaTime * 30;
        controllerComponent.Move(moveSpeed * Time.deltaTime);
    }

    private void CharacterRotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 720);
    }

    private void ControllingAttackJoystick()
    {
        LineRenderer attackArea = weapon.AttackArea;

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
        float attackRange = weapon.AttackRange;

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
        if (!isEnemy && weapon.CurrAttackCount > 0)
        {
            transform.eulerAngles = new Vector3(0, y, 0);
            animator.SetTrigger("Shooting");
        }
    }

    protected override void Death()
    {
        base.Death();
        animator.SetBool("Death", true);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        FloatingJoystick.AttackEvent -= CharacterAttack;
    }
}
