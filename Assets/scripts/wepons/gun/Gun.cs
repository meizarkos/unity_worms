using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    public GameObject bulletPrefab;
    private SpriteRenderer muzzleFlash;
    public float shootInterval = 0.25f;
    private float lastShoot = 0;
    public float flashDuration = 0.1f;
    public float speed = 20f;

    void Start()
    {
        muzzleFlash = firePoint.GetComponentInChildren<SpriteRenderer>();
        muzzleFlash.enabled = false;
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
        if (lastShoot < shootInterval) return;
        lastShoot = 0;
        Debug.Log("Firing gun with power: " + power + " and angle: " + currentAngle);
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