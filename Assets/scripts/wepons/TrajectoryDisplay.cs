using UnityEngine;
using System.Collections;

public class TrajectoryDisplay : MonoBehaviour {
    public GameObject dotPrefab;
    public int dotCount = 5;
    public float disableDots = 1.5f;

    private GameObject[] dots;
    private Coroutine currentRoutine;

    void Awake()
    {
        dots = new GameObject[dotCount];
        for (int i = 0; i < dotCount; i++)
        {
            dots[i] = Instantiate(dotPrefab, transform);
            dots[i].transform.localScale = Vector3.one * 0.1f;
            dots[i].SetActive(false);
        }
    }

    public void Show(bool value)
    {
        foreach (var dot in dots)
            dot.SetActive(value);
    }

    public void UpdateDots(Vector2 startPos, Vector2 direction, float startingAngle, float power, float timeStep, bool isNotAffectedByGravity)
    {
        if (isNotAffectedByGravity)
        {
            Vector2 velocity = direction * 20f;
            for (int i = 0; i < dots.Length; i++) {
                float t = (i + 1) * timeStep;
                dots[i].transform.position = startPos + velocity * t;
            }
        }
        else
        {
            float facingDir = Mathf.Sign(direction.x);
            float rad = startingAngle * facingDir * Mathf.Deg2Rad;
            Vector2 rotated = new Vector2(
                direction.x * Mathf.Cos(rad) - direction.y * Mathf.Sin(rad),
                direction.x * Mathf.Sin(rad) + direction.y * Mathf.Cos(rad)
            );

            Vector2 velocity = rotated * power;
            for (int i = 0; i < dots.Length; i++)
            {
                float t = i * timeStep;
                Vector2 pos = startPos + velocity * t + 0.5f * Physics2D.gravity * t * t;
                dots[i].transform.position = pos;
            }
        }
    }

    public void DeactivateWhileFiring()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(DeactivateDots());
    }
    IEnumerator DeactivateDots() {
        this.Show(false);
        yield return new WaitForSeconds(disableDots);
        this.Show(true);
    }
}