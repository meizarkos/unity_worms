using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public int health = 100;
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.position;
        target.z = transform.position.z; // keep same Z (important in 2D)

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            Time.deltaTime
        );
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(20);
        }
    }
}
