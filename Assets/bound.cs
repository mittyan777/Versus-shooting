using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class bound : MonoBehaviour
{
    // ボールの移動の速さを指定する変数
   float speed;
    Rigidbody2D myRigidbody;
    int choice;
    float HP = 50;
    Animator animator;
    [SerializeField] TextMeshProUGUI Damagetext;
    Text a;
    [SerializeField] float Damage_Time = 0;
    [SerializeField] GameObject Explosion;

    float Damage = 0;
    void Start()
    {
      
        if (gameObject.name == "Obstacle(Clone)") { animator = Damagetext.GetComponent<Animator>(); }
            
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
    private void Update()
    {
        if (gameObject.name == "Obstacle(Clone)")
        {
            if (Damage_Time <= 0.5f)
            {
                animator.SetBool("damage", false);
            }
            if (Damage_Time <= 0)
            {
                Damagetext.text = "";
                Damage = 0;

            }
            else
            {
                Damagetext.fontMaterial.SetColor("_OutlineColor", Color.white);
                Damagetext.text = ($"{Damage}");

            }
            if(HP <= 0)
            {
                Instantiate(Explosion, transform.position, this.transform.rotation);
                Destroy(gameObject);
            }
            Damage_Time -= Time.deltaTime;
        }
        //Damagetext.text = ($"{a}");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "tama")
        {
            if (gameObject.name == "Obstacle(Clone)")
            {
                Damage_Time = 1;
                HP -= 10;
                Damage += 10;
                animator.SetBool("damage", true);
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        if (collision.gameObject.tag == "tama")
        {
            if (gameObject.name == "Obstacle(Clone)")
            {
                Damage_Time = 1;
                HP -= 10;
                Damage += 10;
                animator.SetBool("damage", true);
                Destroy(collision.gameObject);
            }
        }
    }
}
