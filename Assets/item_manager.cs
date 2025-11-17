using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_manager : MonoBehaviour
{
    [SerializeField] GameObject []item;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("item_spawn", 8, 8);
        InvokeRepeating("stone", 6, 6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void item_spawn()
    {
       
        Instantiate(item[Random.Range(0, 4)], transform.position, Quaternion.identity) ;
    }
    void stone()
    {

        Instantiate(item[4], transform.position, Quaternion.identity);
    }

}
