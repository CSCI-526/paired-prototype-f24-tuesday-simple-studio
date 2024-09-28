using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{

    Actions actions;
    MassSpawner ms;
    public float Speed = 5f;
    public GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        actions = GetComponent<Actions>();
        ms = MassSpawner.ins;
        ms.Players.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float Speed_ = Speed / transform.localScale.x;
        Vector2 Direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.MoveTowards(transform.position, Direction, Speed_ * Time.deltaTime);
        if (ms.CreatedBullet.Count > 0 && ms.CreatedBullet[0] != null)
        {
            //ms.CreatedBullet[0].transform.position = transform.position;
        }
        if (Input.GetKey(KeyCode.W))
        {
            actions.ThrowMass();
        }
    }
}