using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullethell : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    int bound_count = 0;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        // ‰EŽÎ‚ß45“x‚Éi‚Þ
        myRigidbody.velocity = transform.up * 12;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name != "Player1_tama_shooting_Type2(Clone)")
        {
            transform.position += transform.up * 5 * Time.deltaTime;
        }
        if(bound_count >= 4)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.name == "Player1_tama_shooting_Type2(Clone)")
        {
            bound_count += 1;
        }
    }
}
