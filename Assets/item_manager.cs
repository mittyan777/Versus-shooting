using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_manager : MonoBehaviour
{
    [SerializeField] GameObject kaihuku;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("item_spawn", 10, 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void item_spawn()
    {
        Instantiate(kaihuku, transform.position, Quaternion.identity);
    }
}
