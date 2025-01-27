using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int speed = 2;
    private int sprintSpeed = 5;
    private int currentSpeed;
    public float playerInputX;
    private float jumpForce = 4f;
    public Vector2 jump;

    public bool _isGround;
    public bool _isFacingRight = true;
    Rigidbody2D rb2d;
    [SerializeField]SpriteRenderer spriteRenderer;
    public int playerAct;
    public Animator anim;
    SoundManager soundmanager;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        jump = new Vector2(0f, 2f);
        currentSpeed = speed;
    }
    public void Awake()
    {
        anim = GetComponent<Animator>();
        soundmanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }
    public void Update()
    {
        Movement();
        Jump();
        Sprint();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isGround = true;
        soundmanager.PlaySfx(soundmanager.Landing);
    }
    private void Movement()
    {
        playerInputX = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
        transform.Translate(playerInputX, 0, 0);

        if (playerInputX == 0f)
        {
            playerAct = 0;
        }
        if (playerInputX > 0f)
        {
            playerAct = 1;
            Direction(1);
        }
        if (playerInputX < 0f)
        {
            playerAct = 1;
            Direction(-1);
        }
        anim.SetInteger("playerAct", playerAct);
        
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            soundmanager.PlaySfx(soundmanager.Jump);
            rb2d.AddForce(jump * jumpForce, ForceMode2D.Impulse);
            _isGround = false;
        }
    }
    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _isGround)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = speed;
        }
    }
    private void Direction(int direction)
    {
        _isFacingRight = direction > 0;
        Vector2 tranScale = transform.localScale;
        tranScale.x = Mathf.Abs(tranScale.x) * direction;
        transform.localScale = tranScale;
    }
}
