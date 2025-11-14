using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_manager : MonoBehaviour
{
    [SerializeField] GameObject []item;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("item_spawn", 10, Random.Range(15,25));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void item_spawn()
    {
       
        Instantiate(item[Random.Range(0, 5)], transform.position, Quaternion.identity) ;
    }
   
}
