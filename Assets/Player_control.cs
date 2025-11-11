using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private Vector2 move;
    public float speed = 5f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    public void OnFire()
    {
        Debug.Log($"{gameObject.name} fired!");
    }
    public void OnSelect()
    {
        Debug.Log("aijdaldjal;j");
    }

    void FixedUpdate()
    {
        if (gameObject.name == "Player1(Clone)")
        {
            float MoveX = move.x;
            float MoveY = move.y;
            transform.position += transform.right * MoveX * 2 * Time.deltaTime;
            transform.position += transform.up * MoveY * 2 * Time.deltaTime;
        }
        else if(gameObject.name == "Player2(Clone)")
        {
            float MoveX = move.x;
            float MoveY = move.y;
            transform.position -= transform.right * MoveX * 2 * Time.deltaTime;
            transform.position -= transform.up * MoveY * 2 * Time.deltaTime;
        }
    }
}