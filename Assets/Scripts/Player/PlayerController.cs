using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerIdleState pidleState;
    public PlayerWalkState pwalkState;
    public PlayerRunState prunState;
    public PlayerJumpState pjumpState;
    public PlayerFaintState pfaintState;
    State state;

    public int speed = 2;
    public int sprintSpeed = 5;
    public int currentSpeed = 0;
    public float playerInputX { get; set; }
    private float jumpForce = 8f;
    public Vector2 jump;

    public bool _isGround = true;
    public bool _isFacingRight = true;
    Rigidbody2D rb2d;
    [SerializeField]SpriteRenderer spriteRenderer;
    public int playerAct;
    public Animator anim;
    SoundManager soundmanager;

    public void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        //jump = new Vector2(0f, 2f);
        //currentSpeed = speed;

        pidleState.Setup(rb2d, anim, this);
        pwalkState.Setup(rb2d, anim, this);
        prunState.Setup(rb2d, anim, this);
        pjumpState.Setup(rb2d, anim, this);
        pfaintState.Setup(rb2d, anim, this);
        state = pidleState;
    }
    public void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //soundmanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>(); เอาเเพิ่มด้วยยยยยยยยยยยยยยยยย
    }
    public void Update()
    {
        Movement();
        JumpInput();
        Sprint();
        SelectState();
        state.Do();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Ground"))
        {
            _isGround = true;
            //soundmanager.PlaySfx(soundmanager.Landing);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
       {
           _isGround = false;
           //soundmanager.PlaySfx(soundmanager.Landing);
       }
    }
    public void Movement()
    {
        playerInputX = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
        transform.Translate(playerInputX, 0, 0);
        if (playerInputX > 0f)
        {
            Direction(1);
        }
        if (playerInputX < 0f)
        {
            Direction(-1);
        }
    }
    public void JumpInput()
    {
        if (Input.GetButtonDown("Jump") && _isGround)
        {
            //soundmanager.PlaySfx(soundmanager.Jump); เอาเเพิ่มด้วยยยยยยยยยยยยยยยยย
            //rb2d.AddForce(jump * jumpForce, ForceMode2D.Impulse);
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);

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
    public void Direction(int direction)
    {
        _isFacingRight = direction > 0;
        Vector2 tranScale = transform.localScale;
        tranScale.x = Mathf.Abs(tranScale.x) * direction;
        transform.localScale = tranScale;
    }
    void SelectState()
    {
        State oldstate = state;
        if (_isGround)
        {
            if (Input.GetKey(KeyCode.LeftShift) && playerInputX != 0)
            {
                state = prunState;
            }
            else if (playerInputX != 0)
            {
                state = pwalkState;
            }
            else
            {
                state = pidleState;
            }
        }
        else
        {
            state = pjumpState;
        }

        if (oldstate != state || oldstate.isComplete) 
        {
            oldstate.Exit();
            state.initialise();
            state.Enter();
        }
        
    }
}
