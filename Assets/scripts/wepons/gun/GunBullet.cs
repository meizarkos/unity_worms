using UnityEngine;

public class GunBullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    private Rigidbody2D rb;
    private readonly float rotation = 90f;

    public int damage = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = transform.right * speed;
        rb.rotation = transform.eulerAngles.z - rotation;
        Destroy(gameObject,lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ennemy ennemy = collision.gameObject.GetComponent<Ennemy>();
        ennemy?.TakeDamage(damage);
        Destroy(gameObject);
    }
}
