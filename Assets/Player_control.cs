using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private Vector2 move;
    private Vector2 move2;
    public float speed = 5f;
    private Rigidbody2D rb;
    [SerializeField] GameObject Player1_tama;
    [SerializeField] GameObject Player2_tama;
    [SerializeField] GameObject Player1_shootPos;
    [SerializeField] GameObject Player2_shootPos;
    float HP = 50;
    [SerializeField]Slider HP_slider;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        move2 = value.Get<Vector2>();
    }
    public void OnFire()
    {
        if (gameObject.name == "Player1(Clone)")
        {
           Instantiate(Player1_tama, Player1_shootPos.transform.position, this.transform.rotation);
        }
        else
        {
            Instantiate(Player2_tama, Player2_shootPos.transform.position, this.transform.rotation);
        }
    }
    public void OnSelect()
    {
      

    }
    private void Start()
    {
        if (gameObject.name == "Player1(Clone)")
        {
            HP_slider = GameObject.Find("Player1_slider").GetComponent<Slider>();
        }
        else if (gameObject.name == "Player2(Clone)")
        {
            HP_slider = GameObject.Find("Player2_slider").GetComponent<Slider>();
        }
    }
    void FixedUpdate()
    {
        HP_slider.value = HP;
        if (gameObject.name == "Player1(Clone)")
        {
            float MoveX = move.x;
            float MoveY = move.y;
            transform.position += Vector3.right * MoveX * 6 * Time.deltaTime;
            transform.position += Vector3.up * MoveY * 6 * Time.deltaTime;
            float MoveX2 = move2.x;
            transform.eulerAngles -= transform.forward * MoveX2 * 200 * Time.deltaTime;
        }
        else if(gameObject.name == "Player2(Clone)")
        {
            float MoveX = move.x;
            float MoveY = move.y;
            transform.position -= Vector3.right * MoveX * 6 * Time.deltaTime;
            transform.position -= Vector3.up * MoveY * 6 * Time.deltaTime;
            float MoveX2 = move2.x;
            transform.eulerAngles -= transform.forward * MoveX2 * 200 * Time.deltaTime;
        }
        if(HP <= 0)
        {
            gameObject.SetActive(false);
            HP_slider.value = 0;
        }
        if(HP > 100)
        {
            HP = 50;
        }
        if(transform.position.x < -2.6)
        {
            transform.position = new Vector3(-2.6f,transform.position.y);
        }
        if (transform.position.x > 3)
        {
            transform.position = new Vector3(3, transform.position.y);
        }
        if (transform.position.y > 4.6f)
        {
            transform.position = new Vector3(transform.position.x, 4.6f);
        }
        if (transform.position.y < -4.4f)
        {
            transform.position = new Vector3(transform.position.x, -4.4f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "tama")
        {
            HP -= 1;
            Destroy(collision.gameObject);
        }
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "kaihuku")
        {
            HP += 10;
            Destroy(collision.gameObject);
        }
    }
}