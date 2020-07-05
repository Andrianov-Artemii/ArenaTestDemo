using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed, shotRange, detroyBulletTime;
    private int damage;

    private Vector3 startPosition;
    private ParticleSystem blood;

    public void Setup(float speed, int damage, float detroyBulletTime, ParticleSystem blood)
    {
        this.speed = speed;
        this.damage = damage;
        this.detroyBulletTime = detroyBulletTime;
        this.blood = blood;
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        Move();
    }
   
    private void Move()
    {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance); ;

        Destroy(gameObject, detroyBulletTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();

        if (unit != null)
        {
            ParticleSystem bloodObj = Instantiate(blood, other.transform);
            Destroy(bloodObj.gameObject, 0.5f);

            unit.Health.GetDamage(damage);
        }
    }
}
