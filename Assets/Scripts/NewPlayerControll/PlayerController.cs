using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
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
    public int currentSpeed = 0;
    public float playerInputX { get; set; }
    private float jumpForce = 8f;
    public Vector2 jump;
    public float jumpSpeed;
    
    public bool _isGround = true;
    private bool _isFacingRight = true;
    Rigidbody2D rb2d;
    [SerializeField]SpriteRenderer spriteRenderer;
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
        //rb2d = GetComponent<Rigidbody2D>();
        //jump = new Vector2(0f, 2f);
        //currentSpeed = speed;

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
        //soundmanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>(); เอาเเพิ่มด้วยยยยยยยยยยยยยยยยย
    }
    public void Update()
    {
        if (!_isGround && Input.GetKey(KeyCode.LeftShift) && Mathf.Abs(playerInputX) > 0.1f)
        {
            rb2d.velocity = new Vector2(sprintSpeed * Mathf.Sign(playerInputX), rb2d.velocity.y);
        }
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
        if (collision.gameObject.tag == ("Ground") || (collision.gameObject.tag == ("Platfrom")))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    _isGround = true;
                    shadow.SetActive(true);
                    //soundmanager.PlaySfx(soundmanager.Landing);
                    return;
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Platfrom"))
        {
            {
                _isGround = false;
                shadow.SetActive(false);
                //soundmanager.PlaySfx(soundmanager.Landing);
            }
        }
        if (collision.gameObject.tag == ("Ground"))
        {
            {
                shadow.SetActive(false);
            }
        }
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
            jumpSpeed = speed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                jumpSpeed = sprintSpeed;
            }
            //rb2d.velocity = new Vector2(rb2d.velocity.x * jumpSpeed, jumpForce);
            rb2d.velocity = new Vector2(jumpSpeed * Mathf.Sign(playerInputX), jumpForce);
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
}
#endregion
