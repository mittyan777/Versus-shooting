using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class bullethell : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    int bound_count = 0;
    [SerializeField]Vector2 start_Transform;
    public float distance ;
    public float damage = 10;
    [SerializeField] bool Player1;
    [SerializeField] AudioClip hansya_Sound;
    [SerializeField] AudioClip shootSound;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(shootSound, transform.position,1);
        start_Transform = transform.position;
        if (Player1 == true)
        {
            distance = Vector2.Distance(start_Transform, GameObject.Find("Player2(Clone)").gameObject.transform.position);
        }
        else
        {
            distance = Vector2.Distance(start_Transform, GameObject.Find("Player1(Clone)").gameObject.transform.position);
        }
        if (gameObject.name == "Player1_tama_shooting_Type2(Clone)" )
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            // 右斜め45度に進む
            myRigidbody.velocity = transform.up * 8;
        }
        else if(gameObject.name == "Player2_tama_shooting_Type2(Clone)")
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            // 右斜め45度に進む
            myRigidbody.velocity = transform.up * 8;
        }

        if (gameObject.name != "Player1_tama_shooting_Type2(Clone)" && gameObject.name != "Player2_tama_shooting_Type2(Clone)")
        {
            Invoke("des", 5);

        }
        damage -= distance / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name != "Player1_tama_shooting_Type2(Clone)" && gameObject.name != "Player2_tama_shooting_Type2(Clone)")
        {
            transform.position += transform.up * 8 * Time.deltaTime;

        }
        if (bound_count >= 4)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "frame")
        {
            if (gameObject.name == "Player1_tama_shooting_Type2(Clone)")
            {
                bound_count += 1;
                AudioSource.PlayClipAtPoint(hansya_Sound, transform.position,1);
            }
            else if (gameObject.name == "Player2_tama_shooting_Type2 1Clone)")
            {
                bound_count += 1;
                AudioSource.PlayClipAtPoint(hansya_Sound, transform.position,1);
            }
        }

      
    }
    void des()
    {
        Destroy(gameObject);
    }
}
