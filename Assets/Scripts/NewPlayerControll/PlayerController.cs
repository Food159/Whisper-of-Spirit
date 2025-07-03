using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Subject, IOserver
{
    #region Variable
    [Header("FSM")]
    public PlayerIdleState pidleState;
    public PlayerWalkState pwalkState;
    public PlayerRunState prunState;
    public PlayerJumpState pjumpState;
    public PlayerFaintState pfaintState;
    State state;

    [Header("GameObject")]
    [SerializeField] GameObject shadow;

    [Header("Variable")]
    public int speed = 2;
    public int sprintSpeed = 5;
    public float currentSpeed = 0;
    public Vector2 jump = new Vector2(0, 2);
    public float playerInputX { get; set; }
    private int jumpForce = 8;
    public int jumpSpeed;
    private bool _isWalkSfxPlaying = false;
    private bool _isRunSfxPlaying = false;
    private int groundContacts = 0;
    public bool _isWalking;

    [SerializeField] SpriteRenderer spriteRenderer;
    public bool _CanMove = true;
    public bool _isGround = true;
    private bool _isFacingRight = true;
    Rigidbody2D rb2d;
    public int playerAct;
    public Animator anim;
    SoundManager soundmanager;
    PlayerHealth status;

    [Header("KnockbackForce")]
    [SerializeField] float knockbackX;
    [SerializeField] float knockbackY;
    #endregion

    #region Code
    public void Start()
    {
        Time.timeScale = 1;
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
        status = GetComponent<PlayerHealth>();
    }
    public void Update()
    {
        if (_CanMove == false)
            return;
        if(status._isPlayerDead)
        {
            ChangeToFaint();
            return;
        }
        Check();
        Movement();
        JumpInput();
        Sprint();
        SelectState();
        state.Do();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platfrom"))
        {
            groundContacts++;
            _isGround = true;
            shadow.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platfrom"))
        {
            groundContacts--;
            if (groundContacts <= 0)
            {
                _isGround = false;
                shadow.SetActive(false);
            }
        }
    }
    public void CanMove()
    {
        _CanMove = false;
        state = pidleState;
        state.Enter();

    }    
    void ChangeToFaint()
    {
        if (state != pfaintState)
        {
            state.Exit();
            state = pfaintState;
            state.initialise();
            state.Enter();
        }
    }
    public void Movement()
    {
        playerInputX = Input.GetAxisRaw("Horizontal");

        Vector2 currentVelocity = rb2d.velocity;
        float targetHorizontalSpeed = playerInputX * currentSpeed;
        rb2d.velocity = new Vector2(targetHorizontalSpeed, currentVelocity.y);

        _isWalking = Mathf.Abs(playerInputX) > 0.01f && _isGround;
        if (_isWalking || Input.GetKey(KeyCode.LeftShift))
        {
            _isWalkSfxPlaying = false;
        }

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
        {
            if (Input.GetButtonDown("Jump") && _isGround)
            {
                SoundManager.instance.PlaySfx(SoundManager.instance.tawanJumpClip);
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                float horizontalJumpForce = playerInputX * speed;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    horizontalJumpForce = playerInputX * sprintSpeed;
                }
                Vector2 finalJumpForce = new Vector2(horizontalJumpForce, jumpForce);
                rb2d.AddForce(finalJumpForce, ForceMode2D.Impulse);

                _isGround = false;
            }
        }
    }
    private void Sprint()
    {
        bool _isRunning = Input.GetKey(KeyCode.LeftShift);

        if (!_isRunning)
        {
            _isRunSfxPlaying = false;
        }
        if (_isRunning)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = speed;
        }
        if (!_isGround && _isRunning)
        {
            currentSpeed = 3.5f;
        }
    }
    private void Direction(int direction)
    {
        if (status._isPlayerDead)
            return;
        _isFacingRight = direction > 0;
        Vector2 tranScale = transform.localScale;
        tranScale.x = Mathf.Abs(tranScale.x) * direction;
        transform.localScale = tranScale;
    }
    public void knockback(Transform enemy)
    {
        if(status._isPlayerDead) 
            return;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb2d.AddForce(direction * knockbackX, ForceMode2D.Impulse);
        rb2d.AddForce(Vector2.up * knockbackY, ForceMode2D.Impulse);
    }
    void Check()
    {
        if (status._isPlayerDead)
        {
            state = pfaintState;
            state.Enter();
        }
    }
    void SelectState()
    {
        if (status._isPlayerDead)
            return;
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
    public void OnNotify(PlayerAction action)
    {
        switch (action) 
        {
            case (PlayerAction.Pause):
                return;

        }
    }
}
#endregion
