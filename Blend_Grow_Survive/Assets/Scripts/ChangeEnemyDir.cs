using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemyDir : MonoBehaviour
{
    public float EnemySpeed;
    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        SetRandomTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, EnemySpeed* Time.deltaTime);

        // If the object reaches the target position, choose a new random target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-10, 10);
        float randomY = Random.Range(-10, 10);

        targetPosition = new Vector3(randomX, randomY, transform.position.z); 
    }
}
