using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    public GameObject bulletPrefab;
    private SpriteRenderer muzzleFlash; 
    public Transform firePoint;
    public float shootInterval = 0.25f;
    private float lastShoot = 0;
    public float flashDuration = 0.1f;

    void Start()
    {
        muzzleFlash = firePoint.GetComponentInChildren<SpriteRenderer>();
        muzzleFlash.enabled = false;
    }

    public override void Fire()
    {
        lastShoot += Time.deltaTime;
        if (lastShoot < shootInterval) return;
        lastShoot = 0;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        muzzleFlash.enabled = false;
    }
}