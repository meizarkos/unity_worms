using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    public GameObject bulletPrefab;
    private SpriteRenderer muzzleFlash;
    public Transform firePoint;
    public float shootInterval = 0.25f;
    private float lastShoot = 0;
    private float currentAngle;
    public float flashDuration = 0.1f;

    private GameObject[] dots;

    public GameObject dotPrefab;
    public float speed = 20f;

    void Start()
    {
        dots = new GameObject[dotCount];
        for (int i = 0; i < dotCount; i++)
        {
            dots[i] = Instantiate(dotPrefab);
            dots[i].transform.localScale = Vector3.one * 0.1f;
        }
        muzzleFlash = firePoint.GetComponentInChildren<SpriteRenderer>();
        muzzleFlash.enabled = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.X)) {
            currentAngle += smooth * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.C)) {
            currentAngle -= smooth * Time.deltaTime;
        }
        currentAngle = Mathf.Clamp(currentAngle , minAngle, maxAngle);
        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
        ShowTrajectory();
    }

    void ShowTrajectory() {
        Vector2 startPos = firePoint.position;
        Vector2 velocity = firePoint.right * speed;

        for (int i = 0; i < dots.Length; i++) {
            float t = (i + 1) * timeStep;
            Vector2 pos = startPos + velocity * t;
            dots[i].transform.position = pos;
        }
    }

    public override void Fire()
    {
        StartCoroutine(DeactivateDots());
        lastShoot += Time.deltaTime;
        if (lastShoot < shootInterval) return;
        lastShoot = 0;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine(Flash());
    }

    IEnumerator DeactivateDots() {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(false);
        }
        yield return new WaitForSeconds(disableDots);
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(true);
        }
    }

    IEnumerator Flash()
    {
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        muzzleFlash.enabled = false;
    }
}