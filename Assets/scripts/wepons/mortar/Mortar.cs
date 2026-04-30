using System.Collections;
using UnityEngine;

public class Mortar : Weapon
{
    public GameObject bulletPrefab;
    private SpriteRenderer[] muzzleFlashes;
    public float flashDuration = 0.1f;
    public float shootInterval = 0.025f;
    private float lastShoot = 0;

    void Start()
    {
        muzzleFlashes = firePoint.GetComponentsInChildren<SpriteRenderer>();
        foreach (var flash in muzzleFlashes)
        {
            flash.enabled = false;
        }
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
        Debug.Log("Last shoot time: " + lastShoot);
        if( lastShoot < shootInterval ) return;
        lastShoot = 0;
        GameObject mortarObus = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        MortarExplosif obus = mortarObus.GetComponent<MortarExplosif>();
        obus.SetPower(power);
        float facingDir = Mathf.Sign(firePoint.right.x); 
        Vector2 dir = RotateVector(firePoint.right, startingAngle * facingDir); 
        obus.SetLaunchDirection(dir);
        obus.SetAngle(currentAngle);
        obus.Launch();
        StartCoroutine(MortarShootAnimation());
    }

    IEnumerator MortarShootAnimation()
    {
        muzzleFlashes[0].enabled = true;
        muzzleFlashes[1].enabled = true;

        yield return new WaitForSeconds(flashDuration);
        muzzleFlashes[0].enabled = false;

        yield return new WaitForSeconds(flashDuration);
        muzzleFlashes[1].enabled = false;
    }

    Vector2 RotateVector(Vector2 v, float degrees) {
        float rad = degrees * Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
        );
    }
}