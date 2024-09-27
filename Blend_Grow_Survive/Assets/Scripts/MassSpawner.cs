using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassSpawner : MonoBehaviour
{

    #region instance 
    public static MassSpawner ins;

    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
        }
    }
    #endregion

    public GameObject Mass;
    public List<GameObject> Players = new List<GameObject>();
    public List<GameObject> CreatedMasses = new List<GameObject>();
    public int MaxMass = 50;
    public float Time_To_Instantiate = 0.5f;
    public float Time_To_Instantiate_Enemy = 5.0f;
    public Vector2 pos;
    public GameObject Enemy;
    public int MaxEnemies = 10;
    public List<GameObject> CreatedEnemies = new List<GameObject>();
    public Vector2 enemySizeRange;
    public float enemyspeed;

    public List<GameObject> CreatedAmmos = new List<GameObject>();
    public List<GameObject> CreatedBullet = new List<GameObject>();
    public GameObject bullet;

    private void Start()
    {
        // Create 5 enemies at the beginning
        for (int i = 0; i < 5; i++)
        {
            if (CreatedEnemies.Count < MaxEnemies)
            {
                Vector2 Position = GetRandomValidPosition();
                GameObject m = Instantiate(Enemy, Position, Quaternion.identity);

                // Generate random size for enemies
                float randomSize = Random.Range(1.0f, 3.0f);
                m.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

                AddMass(m, CreatedEnemies);
            }
        }
        StartCoroutine(CreateFood());
        StartCoroutine(CreateEnemy());
    }

    public void CreateBullet()
    {
        Vector2 Position = Players[0].transform.position;
        GameObject m = Instantiate(bullet, Position, Quaternion.identity);
        CreatedBullet.Add(m);
    }

    public void DestroyPlayerBullet()
    {
        if (CreatedBullet.Count > 0)
        {
            GameObject playerBullet = CreatedBullet[0];
            if (playerBullet != null)
            {
                Destroy(playerBullet);  // Destroy the bullet
                CreatedBullet.RemoveAt(0);
            }
        }
    }

    // Coroutine to spawn food at the speed defined by Time_To_Instantiate
    public IEnumerator CreateFood()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Time_To_Instantiate);

            if (CreatedMasses.Count < MaxMass)
            {
                Vector2 Position = GetRandomValidPosition();
                GameObject m = Instantiate(Mass, Position, Quaternion.identity);
                AddMass(m, CreatedMasses);
            }
        }
    }

    // Coroutine to spawn enemies at the speed defined by Time_To_Instantiate_Enemy
    public IEnumerator CreateEnemy()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Time_To_Instantiate_Enemy);

            if (CreatedEnemies.Count < MaxEnemies)
            {
                Vector2 Position = GetRandomValidPosition();
                GameObject m = Instantiate(Enemy, Position, Quaternion.identity);

                // Generate random size for enemies
                float randomSize = Random.Range(1.0f, 3.0f);
                m.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

                AddMass(m, CreatedEnemies);
            }
        }
    }

    // Get a random position that is not too close to the player
    public Vector2 GetRandomValidPosition()
    {
        Vector2 Position = Vector2.zero;
        bool validPosition = false;

        // Get the player's position
        Vector2 playerPosition = transform.position;

        // Keep generating positions until we find one that is far enough from the player
        while (!validPosition)
        {
            Position = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20)) / 2;

            if (Vector2.Distance(Position, playerPosition) > 0.5f)
            {
                validPosition = true;  // Position is valid
            }
        }

        return Position;
    }

    public void AddMass(GameObject m, List<GameObject> CreatedMasses)
    {
        if (CreatedMasses.Contains(m) == false)
        {
            CreatedMasses.Add(m);
            for (int i = 0; i < Players.Count; i++)
            {
                PlayerEatMass pp = Players[i].GetComponent<PlayerEatMass>();
                pp.AddMass(m);
            }
        }
    }

    public void RemoveMass(GameObject m, List<GameObject> CreatedMasses)
    {
        if (CreatedMasses.Contains(m) == true)
        {
            CreatedMasses.Remove(m);
            for (int i = 0; i < Players.Count; i++)
            {
                PlayerEatMass pp = Players[i].GetComponent<PlayerEatMass>();
                pp.RemoveMass(m);
            }
        }
    }

    public void StopAllMassSpawning()
    {
        StopAllCoroutines();
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, pos);
    }
}