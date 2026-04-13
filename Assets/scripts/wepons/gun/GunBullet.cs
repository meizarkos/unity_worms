using UnityEngine;

public class GunBullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    private Rigidbody2D rb;
    private float rotation = 90f;

    public int damage = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed + transform.up * 1;
        Destroy(gameObject,lifeTime);
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            rb.rotation = angle + rotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ennemy ennemy = collision.gameObject.GetComponent<Ennemy>();
        ennemy?.TakeDamage(damage);
        Destroy(gameObject);
    }
}
