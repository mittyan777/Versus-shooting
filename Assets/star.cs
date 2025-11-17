using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour
{
    [SerializeField] bool right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (right == false)
        {
            if (transform.position.y < -10)
            {
                transform.position = new Vector2(transform.position.x, 11.3f);
            }
            transform.position -= transform.up * 5 * Time.deltaTime;
        }
        else
        {
            if (transform.position.x >= 19)
            {
                transform.position = new Vector2(-18,transform.position.y);
            }
            transform.position += transform.right * 5 * Time.deltaTime;
        }
    }
}
