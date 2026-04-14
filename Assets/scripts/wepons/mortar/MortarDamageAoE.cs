using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public int damage = 15;
    public float radius = 1.15f; 
    private void Start()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Ennemy"))
            {
                float distance = Vector2.Distance(transform.position, hit.ClosestPoint(transform.position));

                // Normalize distance (0 at center → 1 at edge)
                float t = distance / radius;

                // Damage falloff (linear)
                float finalDamage = damage * (1 - t * t);
                Debug.LogFormat("Distance: {0}, Damage: {1}", distance, finalDamage);

                hit.GetComponent<Ennemy>()?.TakeDamage(Mathf.RoundToInt(finalDamage));
            }
        }

        Destroy(gameObject);
    }
}
