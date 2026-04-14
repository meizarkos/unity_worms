using System.Collections;
using UnityEngine;

public class MortarExplosionEffect : MonoBehaviour
{
    private SpriteRenderer[] muzzleFlashes;
    public float flashDuration = 0.1f;
    void Start()
    {
        muzzleFlashes = GetComponentsInChildren<SpriteRenderer>();
        foreach (var flash in muzzleFlashes)
        {
            flash.enabled = false;
        }
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        foreach (var flash in muzzleFlashes)
        {
            flash.enabled = true;
        }
        yield return new WaitForSeconds(flashDuration);
        muzzleFlashes[0].enabled = false;

        yield return new WaitForSeconds(flashDuration);
        muzzleFlashes[1].enabled = false;

        Destroy(gameObject);
    }
}
