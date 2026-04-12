using System.Collections;
using UnityEngine;

public class Mortar : Weapon
{
    public GameObject bulletPrefab;
    private SpriteRenderer muzzleFlash; 
    public Transform firePoint;
    public float bulletForce = 10f;
    public float flashDuration = 0.1f;

    void Start()
    {
        muzzleFlash = firePoint.GetComponentInChildren<SpriteRenderer>();
        muzzleFlash.enabled = false;
    }

    public override void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletPrefab.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.right * bulletForce;
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        muzzleFlash.enabled = false;
    }
}