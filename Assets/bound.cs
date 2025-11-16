using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bound : MonoBehaviour
{
    // ボールの移動の速さを指定する変数
   float speed;
    Rigidbody2D myRigidbody;
    int choice;

    void Start()
    {
        choice = Random.Range(0, 2);
        if (choice == 0)
        {
            speed = 3;
        }
        else
        {
            speed = -3;
        }
        // Rigidbodyにアクセスして変数に保持しておく
        myRigidbody = GetComponent<Rigidbody2D>();
        // 右斜め45度に進む
        myRigidbody.velocity = new Vector3(speed, speed, 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "tama")
        {
            if (gameObject.name == "Obstacle(Clone)")
            {
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.name == "Obstacle(Clone)")
        {
            
                BoxCollider2D myCol = GetComponent<BoxCollider2D>();
                CircleCollider2D playerCol = collision.gameObject.GetComponent<CircleCollider2D>();
              
                if (myCol != null && playerCol != null)
                {
                    Physics2D.IgnoreCollision(myCol, playerCol, true);
                }
            
        }
        if (collision.gameObject.tag == "tama")
        {
            if (gameObject.name == "Obstacle(Clone)")
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
