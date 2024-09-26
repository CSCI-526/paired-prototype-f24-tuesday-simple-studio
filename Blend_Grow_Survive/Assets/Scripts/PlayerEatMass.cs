using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerEatMass : MonoBehaviour
{

    public GameObject[] Mass;
    public GameObject[] Enemies;
    public GameObject[] Ammos;
    public Transform Player;
    public Text resultText;
    public Button restartButton;

    public void UpdateMass()
    {
        Mass = GameObject.FindGameObjectsWithTag("Mass");
    }

    public void UpdateEnemy()
    {
        Mass = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void UpdateAmmo()
    {
        Ammos = GameObject.FindGameObjectsWithTag("ammo");
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
            if (Mass[i] == null)
            {
                continue;
            }
            Transform m = Mass[i].transform;

            if (Vector2.Distance(transform.position, m.position) <= (transform.localScale.x + Player.localScale.x) / 2)
            {
                if (m.gameObject.CompareTag("Mass") || m.gameObject.CompareTag("ammo"))
                {
                    // Eat the mass/food
                    RemoveMass(m.gameObject);
                    PlayerEat();
                    if (m.gameObject.CompareTag("Mass"))
                    {
                        ms.RemoveMass(m.gameObject, ms.CreatedMasses);
                        Destroy(m.gameObject);
                    }
                    else
                    {
                        ms.RemoveMass(m.gameObject, ms.CreatedAmmos);
                        Destroy(m.gameObject);
                        ms.CreateBullet();
                    }
                }
                else if (m.gameObject.CompareTag("Enemy"))
                {
                    // Compare sizes between player and enemy
                    if (transform.localScale.x > m.localScale.x)
                    {
                        RemoveMass(m.gameObject);
                        PlayerEat();
                        ms.RemoveMass(m.gameObject, ms.CreatedEnemies);
                        Destroy(m.gameObject);
                        if (ms.CreatedEnemies.Count == 0)
                        {
                            WinGame();
                        }
                    }
                    else
                    {
                        GameOver();
                    }
                }
            }
        }
    }

    public void CheckEnemy()
    {
        CheckGameObject(Enemies);
    }

    public void CheckAmmo()
    {
        CheckGameObject(Ammos);
    }

    public void GameOver()
    {
        CancelInvoke("Check");
        CancelInvoke("CheckEnemy");

        ms.StopAllMassSpawning();

        resultText.text = "Game Over!";
        resultText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        gameObject.SetActive(false);

        Time.timeScale = 0f;
    }

    public void WinGame()
    {
        CancelInvoke("Check");
        CancelInvoke("CheckEnemy");

        ms.StopAllMassSpawning();

        resultText.text = "You Win!";
        resultText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        gameObject.SetActive(false);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    MassSpawner ms;
    // Start is called before the first frame update
    void Start()
    {
        UpdateMass();
        UpdateEnemy();
        UpdateAmmo();
        InvokeRepeating("Check", 0, 0.1f);
        InvokeRepeating("CheckEnemy", 0, 0.1f);
        InvokeRepeating("CheckAmmo", 0, 0.1f);
        ms = MassSpawner.ins;

        ms.Players.Add(gameObject);
    }

    void PlayerEat()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

}