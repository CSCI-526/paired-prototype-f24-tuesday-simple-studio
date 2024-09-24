using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEatMass : MonoBehaviour
{

    public GameObject[] Mass;
    public GameObject[] Enemies;
    public Transform Player;

    public void UpdateMass()
    {
        Mass = GameObject.FindGameObjectsWithTag("Mass");
    }
    public void UpdateEnemy()
    {
        Mass = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void RemoveMass(GameObject MassObject)
    {
        List<GameObject> MassList = new List<GameObject>();

        for (int i = 0; i < Mass.Length; i++)
        {
            MassList.Add(Mass[i]);
        }
        MassList.Remove(MassObject);

        Mass = MassList.ToArray();
    }

    public void AddMass(GameObject MassObject)
    {
        List<GameObject> MassList = new List<GameObject>();

        for (int i = 0; i < Mass.Length; i++)
        {
            MassList.Add(Mass[i]);
        }
        MassList.Add(MassObject);

        Mass = MassList.ToArray();
    }


    public void Check()
    {
        CheckGameObject(Mass);
    }

    public void CheckGameObject(GameObject[] Mass)
    {
        for (int i = 0; i < Mass.Length; i++)
        {
            Transform m = Mass[i].transform;

            if (Vector2.Distance(transform.position, m.position) <= (transform.localScale.x  + Player.localScale.x)/2)
            {
                RemoveMass(m.gameObject);
                // eat 
                PlayerEat();

                // destroy
                if (m.gameObject.CompareTag("Mass"))
                {
                    ms.RemoveMass(m.gameObject, ms.CreatedMasses);  // Remove from CreatedMasses
                }
                else if (m.gameObject.CompareTag("Enemy"))
                {
                    ms.RemoveMass(m.gameObject, ms.CreatedEnemies);  // Remove from CreatedEnemies
                }
                Destroy(m.gameObject);
            }
        }
    }

    public void CheckEnemy()
    {
        CheckGameObject(Enemies);
    }

    MassSpawner ms;
    // Start is called before the first frame update
    void Start()
    {
        UpdateMass();
        UpdateEnemy();
        InvokeRepeating("Check", 0, 0.1f);
        InvokeRepeating("CheckEnemy", 0, 0.1f);
        ms = MassSpawner.ins;

        ms.Players.Add(gameObject);
    }

    void PlayerEat()
    {
        transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
    }

}