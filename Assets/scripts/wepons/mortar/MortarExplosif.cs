using UnityEngine;

public class MortarExplosif : MonoBehaviour
{
    private float power;
    private float shotAngle;
    private Vector2 launchDirection;
    public float lifeTime = 5f;
    private Rigidbody2D rb;
    public float startingRotation = 90f;

    private SpriteRenderer sr;
    public GameObject hitDamage;
    public GameObject hitEffect;
    
    void Awake() {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        Destroy(gameObject, lifeTime);
    }
    public void Launch() {
        rb.linearVelocity = launchDirection * power;
    }

    public void SetAngle(float shotAngle)
    {
        this.shotAngle = shotAngle;
    }

    public void SetPower(float power)
    {
        this.power = power;
    }

    public void SetLaunchDirection(Vector2 dir) {
        launchDirection = dir;
    }

    void FixedUpdate() 
    { 
        if (rb.linearVelocity.sqrMagnitude > 0.01f) 
        { 
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg; 
            float dir = Mathf.Sign(rb.linearVelocity.x); 
            sr.flipY = dir > 0;
            rb.rotation = angle + startingRotation * dir;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(hitDamage,transform.position, Quaternion.identity, null);
        Instantiate(hitEffect, transform.position, Quaternion.identity, null);
        Destroy(gameObject);
    }
}
