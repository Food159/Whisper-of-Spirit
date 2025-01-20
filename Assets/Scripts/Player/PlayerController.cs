using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 5;
    public float playerInputX;
    public float jumpForce = 2f;
    public Vector2 jump;

    private bool _isFacingRight;
    public bool _isGround;
    Rigidbody2D rb2d;
    [SerializeField]SpriteRenderer spriteRenderer;
    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        jump = new Vector2(0f, 2f);
    }
    public void Update()
    {
        playerInputX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(playerInputX, 0, 0);


        if(Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            rb2d.AddForce(jump * jumpForce, ForceMode2D.Impulse);
            _isGround = false;
        }
        Direction();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        _isGround = true;
    }
    private void Direction()
    {
        if(playerInputX < 0f) 
        {
            spriteRenderer.flipX = true;
        }
        else if (playerInputX > 0f) 
        {
            spriteRenderer.flipX = false;
        }
    }
}

