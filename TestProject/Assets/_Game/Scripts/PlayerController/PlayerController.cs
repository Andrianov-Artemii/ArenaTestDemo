using System.Collections;
using UnityEngine;

public class PlayerController : PlayerBase
{

    private int currShotCount;

    private FixedJoystick walkingJoystick;
    private FloatingJoystick atackingJoystick;

    private Vector3 moveSpeed;
    private float reloadTimer = 0;
    private float y;
    private bool hit;

    CharacterController controllerComponent;
    PlayerBase playerBase;
    BarsManager barsManager;
    Animator animator;
    Weapons weapons;

    public int CurrShotCount { get { return currShotCount; } private set { currShotCount = value; } }

    public Weapons Weapons { get { return weapons; } private set { weapons = value; } }

    public void Death()
    {
        animator.SetBool("Death", true);
    }

    private void Start()
    {
        controllerComponent = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerBase = GetComponent<PlayerBase>();

        weapons = GetComponentInChildren<Weapons>();
        barsManager = GetComponent<BarsManager>();

        walkingJoystick = GameManager.Inctance.WalkingJoystick;
        atackingJoystick = GameManager.Inctance.AtackingJoystick;

        currShotCount = Weapons.ShotCount;
    }

    private void Update()
    {
        if (!hit)
            Atack();

        if (CurrShotCount < Weapons.ShotCount)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= Weapons.ReloadTime)
            {
                reloadTimer = 0;
                CurrShotCount++;
                barsManager.ShotBarChanges(CurrShotCount, weapons.ShotCount);
            }
        }
    }

    void FixedUpdate()
    {
        if (!hit)
            Walk();
    }

    private void Walk()
    {
        float walkingSpeed = playerBase.WalkingSpeed;
        float runningSpeed = playerBase.RunningSpeed;

        moveSpeed.y = 0;

        float animSpeed = Mathf.Abs(walkingJoystick.Vertical) + Mathf.Abs(walkingJoystick.Horizontal);
        float speed = walkingSpeed * animSpeed;

        animator.SetFloat("Speed", animSpeed);

        Vector3 direction = Vector3.forward * walkingJoystick.Vertical + Vector3.right * walkingJoystick.Horizontal;
        Vector3 target = Mathf.Clamp(speed, walkingSpeed, runningSpeed) * new Vector3(walkingJoystick.Horizontal, 0, walkingJoystick.Vertical).normalized;

        if (direction != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 720);

        moveSpeed = Vector3.MoveTowards(moveSpeed, target, Time.deltaTime * 300);

        controllerComponent.Move(moveSpeed * Time.deltaTime);
    }

    public void Atack()
    {
        LineRenderer atackArea = Weapons.AtackArea;

        if (Mathf.Abs(atackingJoystick.Horizontal) > 0.01f || Mathf.Abs(atackingJoystick.Vertical) > 0.01f)
        {
            float shotRange = Weapons.ShotRange;
            

            if (!atackArea.gameObject.activeSelf)
                atackArea.gameObject.SetActive(true);

            RaycastHit hit;
            Transform atackTrailTransform = atackArea.transform;

            atackTrailTransform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);

            Vector3 direction = new Vector3(atackingJoystick.Horizontal + transform.position.x, 0, atackingJoystick.Vertical + transform.position.z);

            atackTrailTransform.LookAt(new Vector3(direction.x, 0, direction.z));
            atackTrailTransform.eulerAngles = new Vector3(0, atackTrailTransform.eulerAngles.y, 0);
            atackArea.SetPosition(0, atackTrailTransform.position);

            if (Physics.Raycast(atackTrailTransform.position, atackTrailTransform.forward, out hit, shotRange))
            {
                atackArea.SetPosition(1, hit.point);
            }
            else
                atackArea.SetPosition(1, atackTrailTransform.position + atackTrailTransform.forward * shotRange);

            y = atackArea.transform.eulerAngles.y;
        }
        else
            atackArea.gameObject.SetActive(false);
    }

    public void Shot()
    {
        if (CurrShotCount > 0)
        {
            hit = true;
            weapons.Hit = hit;

            transform.eulerAngles = new Vector3(0, y, 0);
            animator.SetTrigger("Shooting");

            StartCoroutine(AtackAnimation());

            CurrShotCount = Weapons.ToAtack(CurrShotCount, transform);
            barsManager.ShotBarChanges(CurrShotCount, weapons.ShotCount);
        }
    }

    private IEnumerator AtackAnimation()
    {
        yield return new WaitForSeconds(Weapons.AtackDelay);
        hit = false;
        weapons.Hit = hit;
    }
}
