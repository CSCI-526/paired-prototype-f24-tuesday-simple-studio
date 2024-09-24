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
    public Vector2 pos;
    public GameObject Enemy;
    public int MaxEnemies = 10;
    public List<GameObject> CreatedEnemies = new List<GameObject>();
    public Vector2 enemySizeRange;
    public float enemyspeed;


    private void Start()
    {
        StartCoroutine(CreateMass(CreatedMasses, MaxMass, Mass));
        StartCoroutine(CreateMass(CreatedEnemies, MaxEnemies, Enemy));
    }

    public IEnumerator CreateMass(List<GameObject> CreatedMasses, int MaxMass, GameObject Mass)
    {
        // Wait for a specified amount of time
        yield return new WaitForSecondsRealtime(Time_To_Instantiate);

        if (CreatedMasses.Count <= MaxMass)
        {
            // Generate random position
            Vector2 Position = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20)) / 2;

            // Instantiate the mass or enemy at the random position
            GameObject m = Instantiate(Mass, Position, Quaternion.identity);

            // Check if the object is an enemy (comparing the instantiated object with your Enemy prefab)
            if (Mass == Enemy)
            {
                // Generate random size based on enemySizeRange
                float randomSize = Random.Range(1.0f, 3.0f);

                m.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
            }

            // Add the mass/enemy to the list
            AddMass(m, CreatedMasses);
        }

        // Continue to instantiate objects
        StartCoroutine(CreateMass(CreatedMasses, MaxMass, Mass));
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