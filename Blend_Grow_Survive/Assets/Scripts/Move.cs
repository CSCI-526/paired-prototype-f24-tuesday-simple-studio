using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime / transform.localScale.x);
    }
}
