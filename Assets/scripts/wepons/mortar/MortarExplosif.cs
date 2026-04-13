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

    public int damage = 15;
    public float flashDuration = 0.1f;
    public SpriteRenderer[] explosionFlashes;
    private bool hasExploded = false;

    void Start()
    {
        explosionFlashes = GetComponentsInChildren<SpriteRenderer>();
        foreach (var flash in explosionFlashes)
        {
            flash.enabled = false;
        }
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
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
        if (hasExploded) return;
        hasExploded = true;
        StartCoroutine(Flash(collision));
        Ennemy ennemy = collision.gameObject.GetComponent<Ennemy>();
        ennemy?.TakeDamage(damage);
    }

    IEnumerator Flash(Collider2D collision)
    {
        rb.linearVelocity = Vector2.zero;
        sr.enabled = false;

        foreach (var flash in explosionFlashes)
        {
            flash.enabled = true;
            flash.transform.position = collision.transform.position;
        }
        yield return new WaitForSeconds(flashDuration);
        explosionFlashes[0].enabled = false;

        yield return new WaitForSeconds(flashDuration);
        explosionFlashes[1].enabled = false;

        Destroy(gameObject);
    }
}
