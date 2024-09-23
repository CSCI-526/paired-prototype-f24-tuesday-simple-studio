using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveColor : MonoBehaviour
{
    public List<Material> mat_list = new List<Material> ();

    void Awake()
    {
        GetComponent<Renderer>().material = mat_list[Random.Range(0, mat_list.Count)];
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
