using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10)
        {
            transform.position = new Vector2(transform.position.x, 11.3f);
        }
        transform.position -= transform.up * 5 * Time.deltaTime;
    }
}
