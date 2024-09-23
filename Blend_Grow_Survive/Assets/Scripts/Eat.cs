using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    public string Tag;
    public float increase_size;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag)
        {
            transform.localScale += new Vector3(increase_size, increase_size, increase_size);
            Destroy(other.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
