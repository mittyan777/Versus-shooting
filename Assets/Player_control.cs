using System.Threading;
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
    [SerializeField] GameObject shootType2_tama;
    [SerializeField] GameObject Player1_shootPos;
    [SerializeField] GameObject Player2_shootPos;
    float HP = 50;
    [SerializeField]Slider HP_slider;
    [SerializeField]int shootType = 0;
    [SerializeField]float flagtime = 0;
    bool Rapid_fire_flag = false;
    [SerializeField]float shootingvalue;
    [SerializeField] GameObject Barrier;
    [SerializeField] bool barrier = false;

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
    public void OnFire(InputValue value)
    {
        shootingvalue = value.Get<float>();
        if (shootType == 0)
        {
            if (shootingvalue == 1)
            {
                if (gameObject.name == "Player1(Clone)" && flagtime <= 0)
                {
                    Instantiate(Player1_tama, Player1_shootPos.transform.position, this.transform.rotation);
                    flagtime = 0.1f;
                }
                else if (gameObject.name == "Player2(Clone)" && flagtime <= 0)
                {
                    Instantiate(Player2_tama, Player2_shootPos.transform.position, this.transform.rotation);
                    flagtime = 0.1f;
                }
            }
        }
        else if(shootType == 1)
        {
            Rapid_fire_flag = true;
        }
        else if (shootType == 2)
        {
            if (shootingvalue == 1)
            {
                if (gameObject.name == "Player1(Clone)" && flagtime <= 0)
                {
                    Instantiate(shootType2_tama, Player1_shootPos.transform.position, this.transform.rotation);
                    flagtime = 0.1f;
                }
                else if (gameObject.name == "Player2(Clone)" && flagtime <= 0)
                {
                    Instantiate(shootType2_tama, Player2_shootPos.transform.position, this.transform.rotation);
                    flagtime = 0.1f;
                }
            }
        }
    }
    void Rapid_fire()
    {
        if (Rapid_fire_flag == true)
        {
            if (gameObject.name == "Player1(Clone)" && flagtime <= 0)
            {
                Instantiate(Player1_tama, Player1_shootPos.transform.position, this.transform.rotation);
                flagtime = 0.1f;
            }
            else if (gameObject.name == "Player2(Clone)" && flagtime <= 0)
            {
                Instantiate(Player2_tama, Player2_shootPos.transform.position, this.transform.rotation);
                flagtime = 0.1f;
            }
        }
        if(shootingvalue == 0)
        {
            Rapid_fire_flag = false;
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
        if (gameObject.name == "Player2(Clone)")
        {
            HP_slider = GameObject.Find("Player2_slider").GetComponent<Slider>();
        }
    }
    void FixedUpdate()
    {
      
        Rapid_fire();
       flagtime -= Time.deltaTime;
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
            if (barrier == false)
            {
                HP -= 1;
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }
            
        }
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "kaihuku")
        {
            HP += 10;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "shooting_Type1")
        {
            shootType = 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "shooting_Type2")
        {
            shootType = 2;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Barrier")
        {
            Barrier.SetActive(true);
            barrier = true;
            Invoke("BarrierOF", 5);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "tama")
        {
            if (barrier == false)
            {
                HP -= 1;
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }

        }
    }
    void BarrierOF()
    {
        Barrier.SetActive(false);
        barrier = false;
    }
}