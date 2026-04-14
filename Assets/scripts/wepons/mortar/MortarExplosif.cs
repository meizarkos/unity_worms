using System.Collections;
using UnityEngine;

public class MortarExplosif : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float verticalSpeed = 10f;
    public float lifeTime = 5f;
    private Rigidbody2D rb;
    public float rotation = 90f;

    private SpriteRenderer sr;
    public GameObject hitDamage;
    public GameObject hitEffect;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * horizontalSpeed + transform.up * verticalSpeed;
        Destroy(gameObject,lifeTime);
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            float dir = Mathf.Sign(rb.linearVelocity.x); // +1 right, -1 left
            rb.rotation = angle + rotation * -dir;
            if(dir < 0)
            {
                sr.flipY = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(hitDamage,transform.position, Quaternion.identity, null);
        Instantiate(hitEffect, transform.position, Quaternion.identity, null);
        Destroy(gameObject);
    }
}
