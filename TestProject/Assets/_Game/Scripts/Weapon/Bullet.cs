using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed, shotRange, detroyBulletTime;
    private float damage;

    private Vector3 startPosition;
    private ParticleSystem blood;

    public void Setup(float speed, float damage, float detroyBulletTime, ParticleSystem blood)
    {
        this.speed = speed;
        this.damage = damage;
        this.detroyBulletTime = detroyBulletTime;
        this.blood = blood;
    }

    private void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, detroyBulletTime);
    }

    private void Update()
    {
        Move();
    }
   
    private void Move()
    {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance); ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Unit>())
        {
            Unit unit = other.GetComponent<Unit>();

            ParticleSystem bloodObj = Instantiate(blood, other.transform);
            Destroy(bloodObj.gameObject, 0.5f);

            unit.Health.GetDamage(damage);
        }
    }
}
