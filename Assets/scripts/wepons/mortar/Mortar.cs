using System.Collections;
using UnityEngine;

public class Mortar : Weapon
{
    public GameObject bulletPrefab;
    private SpriteRenderer[] muzzleFlashes;
    public Transform firePoint;
    public float flashDuration = 0.1f;
    public float shootInterval = 1f;
    private float lastShoot = 0;

    void Start()
    {
        muzzleFlashes = firePoint.GetComponentsInChildren<SpriteRenderer>();
        foreach (var flash in muzzleFlashes)
        {
            flash.enabled = false;
        }
    }

    public override void Fire()
    {
        lastShoot += Time.deltaTime;
        if( lastShoot < shootInterval ) return;
        lastShoot = 0;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
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
}