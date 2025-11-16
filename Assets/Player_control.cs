using System.Threading;
using TMPro;
using Unity.VisualScripting;
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
    float HP = 500;
    [SerializeField]Slider HP_slider;
    [SerializeField]int shootType = 0;
    [SerializeField]float flagtime = 0;
    [SerializeField]float shootType_reset_count = 10;
    bool Rapid_fire_flag = false;
    [SerializeField]float shootingvalue;
    [SerializeField] GameObject Barrier;
    [SerializeField] bool barrier = false;
    [SerializeField] Sprite []Player1;
    [SerializeField] Sprite[]Player2;
    [SerializeField] GameObject Explosion;
    float Damage = 0;
    [SerializeField]float Damage_Time = 3;
    [SerializeField]TextMeshProUGUI Damagetext;
    float aim = 200;
    Animator animator;

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
    void OnDrawGizmos()
    {
        if (gameObject.name == "Player1(Clone)")
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.up * 10f);
        }
        else if (gameObject.name == "Player2(Clone)")
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + -transform.up * 10f);
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
        animator = Damagetext.GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        Debug.Log(aim);
        if (gameObject.name == "Player1(Clone)")
        {
            RaycastHit2D hit = Physics2D.Raycast(Player1_shootPos.transform.position, transform.up, 10f);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
               
                if (hit.collider.gameObject.name == "Player2(Clone)")
                {
                    float distance = Vector2.Distance(transform.position, hit.collider.gameObject.transform.position);
                    //Debug.Log(distance);
                    float assist = 100 + distance * 10;
                    Debug.Log(assist);
                    aim = assist;
                }
                else
                {
                    aim = 200;
                }
            }
            
        }
        else if(gameObject.name == "Player2(Clone)")
        {
            RaycastHit2D hit = Physics2D.Raycast(Player2_shootPos.transform.position, -transform.up, 10f);

            if (hit.collider != null)
            {
                if(hit.collider.gameObject.name == "Player1(Clone)")
                {
                    float distance = Vector2.Distance(transform.position, hit.collider.gameObject.transform.position);
                    //Debug.Log(distance);
                    float assist = 100 + distance * 10;
                    //Debug.Log(assist);
                    aim = assist;
                }
                else
                {
                    aim = 200;
                }
            }
           
        }



        Rapid_fire();
        flagtime -= Time.deltaTime;
        HP_slider.value = HP;
        if (gameObject.name == "Player1(Clone)")
        {
            float MoveX = move.x;
            float MoveY = move.y;
            transform.position += Vector3.right * MoveX * 6 * Time.deltaTime;
            transform.position += Vector3.up * MoveY * 6 * Time.deltaTime;
            if (MoveX > 0) { GetComponent<SpriteRenderer>().sprite = Player1[1]; }
            if (MoveX == 0) { GetComponent<SpriteRenderer>().sprite = Player1[0]; }
            if (MoveX < 0) { GetComponent<SpriteRenderer>().sprite = Player1[2]; }
            float MoveX2 = move2.x;
            transform.eulerAngles -= transform.forward * MoveX2 * aim * Time.deltaTime;
        }
        else if (gameObject.name == "Player2(Clone)")
        {
            float MoveX = move.x;
            float MoveY = move.y;
            transform.position -= Vector3.right * MoveX * 6 * Time.deltaTime;
            transform.position -= Vector3.up * MoveY * 6 * Time.deltaTime;
            if (MoveX > 0) { GetComponent<SpriteRenderer>().sprite = Player2[1]; }
            if (MoveX == 0) { GetComponent<SpriteRenderer>().sprite = Player2[0]; }
            if (MoveX < 0) { GetComponent<SpriteRenderer>().sprite = Player2[2]; }
            float MoveX2 = move2.x;
            transform.eulerAngles -= transform.forward * MoveX2 * aim * Time.deltaTime;
        }
        if (HP <= 0)
        {
            Instantiate(Explosion, transform.position, this.transform.rotation);
            gameObject.SetActive(false);
            HP_slider.value = 0;
        }
        if (HP > 500)
        {
            HP = 500;
        }
        if (transform.position.x < -2.6)
        {
            transform.position = new Vector3(-2.6f, transform.position.y);
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

        if (shootType == 1 || shootType == 2)
        {
            shootType_reset_count -= Time.deltaTime;
            if (shootType_reset_count <= 0)
            {
                shootTypeOF();
            }
        }
        if(Damage_Time <= 0.5f)
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
            Damagetext.text = ($"{Damage}");

        }
        Damage_Time -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "tama")
        {
            if (barrier == false)
            {
                HP -= Mathf.Floor(collision.gameObject.GetComponent<bullethell>().damage);
                Damagetext.rectTransform.localPosition = new Vector3(Random.Range(-1.214f, -0.726f), Damagetext.rectTransform.localPosition.y, Damagetext.rectTransform.localPosition.z);
                animator.SetBool("damage",true);
                Damage += Mathf.Floor(collision.gameObject.GetComponent<bullethell>().damage); 
                Damage_Time = 1;
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
            shootType_reset_count = 10;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "shooting_Type2")
        {
            shootType = 2;
            shootType_reset_count = 10;
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
                HP -= Mathf.Floor(collision.gameObject.GetComponent<bullethell>().damage); 
                animator.SetBool("damage", true);
                Damage_Time = 1;
                Damage += Mathf.Floor(collision.gameObject.GetComponent<bullethell>().damage);
                
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }

        }

        if (collision.gameObject.name == "Obstacle(Clone)")
        {

            CircleCollider2D myCol = GetComponent<CircleCollider2D>();
            BoxCollider2D playerCol = collision.gameObject.GetComponent<BoxCollider2D>();

            if (myCol != null && playerCol != null)
            {
                Physics2D.IgnoreCollision(myCol, playerCol, true);
            }

        }
    }
    void BarrierOF()
    {
        Barrier.SetActive(false);
        barrier = false;

    }
    void shootTypeOF()
    {
        shootType = 0;
        Rapid_fire_flag = false;
    }
}