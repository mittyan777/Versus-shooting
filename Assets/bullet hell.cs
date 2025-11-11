using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullethell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * 10 * Time.deltaTime;
    }
}
