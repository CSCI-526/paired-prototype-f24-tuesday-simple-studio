using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public float bulletSpeed = 20f;  // Speed of the bullet
    public float maxDistance = 20f;  // Maximum distance the bullet can travel
    public LayerMask enemyLayerMask; // Assign the layer for enemies in the Inspector

    private Vector3 startPosition;  // To track where the bullet was spawned
    ObjectGenerator ms;

    void Start()
    {
        startPosition = transform.position;  // Record the start position when the bullet is spawned
        ms = ObjectGenerator.ins;  // Reference to the MassSpawner instance
    }

    void Update()
    {
        // Move the bullet forward in a straight line
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);

        // Check if the bullet has traveled the max distance
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);  // Destroy the bullet after it travels 20 units
        }

        // Raycast to detect if the bullet is close to an enemy
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, bulletSpeed * Time.deltaTime, enemyLayerMask);
        if (hit.collider != null)
        {
            // Check if the ray hit an enemy
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);  // Destroy the enemy

                // Remove the enemy from the CreatedEnemies list
                ms.RemoveMass(hit.collider.gameObject, ms.CreatedEnemies);

                // Destroy the bullet itself
                Destroy(gameObject);

                // Check if all enemies are destroyed
                if (ms.CreatedEnemies.Count == 0)
                {
                    // If there are no enemies left, call WinGame
                    FindObjectOfType<PlayerEatMass>().WinGame();
                }
            }
        }
    }
}
