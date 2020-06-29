using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed, shotRange;
    private int damage;

    private Vector3 startPosition;
    private ParticleSystem blood;

    public void Setup(float speed, int damage, float shotRange, ParticleSystem blood)
    {
        this.speed = speed;
        this.damage = damage;
        this.shotRange = shotRange;
        this.blood = blood;
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        Move();
        CheckDistance();
    }
   
    private void Move()
    {
        float moveDistance = speed * Time.deltaTime;

        transform.Translate(Vector3.forward * moveDistance); ;
    }

    private void CheckDistance()
    {
        bool checkDistance = Vector3.Distance(startPosition, transform.position) >= shotRange;
        if (checkDistance)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthManager healthManager = other.GetComponent<HealthManager>();

        if (healthManager != null)
        {
            ParticleSystem bloodObj = Instantiate(blood, other.transform);
            Destroy(bloodObj.gameObject, 0.5f);

            healthManager.TakeDamage(damage);
        }
    }
}
