using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{

    Actions actions;

    public float Speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        actions = GetComponent<Actions>();
    }

    // Update is called once per frame
    void Update()
    {
        float Speed_ = Speed / transform.localScale.x;
        Vector2 Direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.MoveTowards(transform.position, Direction, Speed_ * Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
        {
            actions.ThrowMass();
        }
    }
}