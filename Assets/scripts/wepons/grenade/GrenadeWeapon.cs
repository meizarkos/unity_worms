using System.Collections;
using UnityEngine;

public class GrenadeWeapon : Weapon
{
    public GameObject grenadePrefab;
    public float disableDisplay = 5.5f;
    public float shootInterval = 0.025f;
    private float lastShoot = 0;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ChangeAngle();
        ChangePower();
        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }

    public override void Fire()
    {
        lastShoot += Time.deltaTime;
        if( lastShoot < shootInterval ) return;
        lastShoot = 0;
        GameObject grenade = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
        GrenadeExplosif obus = grenade.GetComponent<GrenadeExplosif>();
        obus.SetPower(power);
        float facingDir = Mathf.Sign(firePoint.right.x); 
        Vector2 dir = RotateVector(firePoint.right, startingAngle * facingDir); 
        obus.SetLaunchDirection(dir);
        obus.SetAngle(currentAngle);
        obus.Launch();
        StartCoroutine(GrenadeShootAnimation());
    }

    IEnumerator GrenadeShootAnimation()
    {
        sr.enabled = false;
        yield return new WaitForSeconds(disableDisplay);
        sr.enabled = true;
    }

    Vector2 RotateVector(Vector2 v, float degrees) {
        float rad = degrees * Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
        );
    }
}