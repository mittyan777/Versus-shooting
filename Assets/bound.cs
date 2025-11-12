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
}
