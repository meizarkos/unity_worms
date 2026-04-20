using UnityEngine;

public class GrenadeExplosif : MonoBehaviour
{
    private float power;
    private float shotAngle;
    private Vector2 launchDirection;
    public float lifeTime = 5f;
    public float spinSpeed = 25f;
    private Rigidbody2D rb;

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
        rb.angularVelocity = spinSpeed;
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

    void OnDestroy()
    {
        Instantiate(hitDamage, transform.position, Quaternion.identity);
        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }
}
