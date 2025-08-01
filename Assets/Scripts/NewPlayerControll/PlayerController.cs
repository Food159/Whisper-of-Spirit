﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : Subject, IOserver, IPausable
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
    bool _isRunning = false;
    private bool _isWalkSfxPlaying = false;
    private bool _isRunSfxPlaying = false;
    private int groundContacts = 0;
    public bool _isWalking;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    [SerializeField] SpriteRenderer spriteRenderer;
    public bool _CanMove = true;
    public bool _isGround = true;
    public bool isOnPlatform = false;
    private float platformExitDelay = 0.5f;
    private float platformExitTimer = 0f;
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
        if (!_CanMove)
            return;
        if(status._isPlayerDead)
        {
            ChangeToFaint();
            return;
        }

        if(_isGround)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if(Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (platformExitTimer > 0f)
        {
            platformExitTimer -= Time.deltaTime;
            if (platformExitTimer <= 0f)
            {
                isOnPlatform = false;
            }
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
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platfrom") || collision.gameObject.CompareTag("firstPlatform"))
        {
            groundContacts++;
            _isGround = true;
            shadow.SetActive(true);
        }
        if(collision.gameObject.CompareTag("Platfrom"))
        {
            isOnPlatform = true;
            platformExitTimer = 0f;
        }
        else
        {
            isOnPlatform = false;
            platformExitTimer = platformExitDelay;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platfrom") || collision.gameObject.CompareTag("firstPlatform"))
        {
            groundContacts--;
            if (groundContacts <= 0)
            {
                _isGround = false;
                shadow.SetActive(false);
            }
        }
        //if (collision.gameObject.CompareTag("Platfrom"))
        //{
        //    platformExitTimer = platformExitDelay;
        //}
    }
    public void CanMove()
    {
        _CanMove = false;
        state = pidleState;
        state.Enter();

    }    
    public void ChangeToFaint()
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
        playerInputX = Input.GetAxis("Horizontal");

        Vector2 currentVelocity = rb2d.velocity;
        float targetHorizontalSpeed = playerInputX * currentSpeed;
        rb2d.velocity = new Vector2(targetHorizontalSpeed, currentVelocity.y);
        //SoundManager.instance.PlaySfx(SoundManager.instance.tawanWalkClip);
        _isWalking = Mathf.Abs(playerInputX) > 0.01f && _isGround;
        //if (_isWalking)
        //{
        //    if (_isRunning)
        //    {
        //        if (!_isRunSfxPlaying)
        //        {
        //            SoundManager.instance.PlaySfx(SoundManager.instance.tawanRunClip);
        //            _isRunSfxPlaying = true;
        //            _isWalkSfxPlaying = false;
        //        }
        //    }
        //    else
        //    {
        //        if (!_isWalkSfxPlaying)
        //        {
        //            SoundManager.instance.PlaySfx(SoundManager.instance.tawanWalkClip);
        //            _isWalkSfxPlaying = true;
        //            _isRunSfxPlaying = false;
        //        }
        //    }
        //}
        //else
        //{
        //    audioSource.Stop();
        //    _isRunSfxPlaying = false;
        //    _isWalkSfxPlaying = false;
        //}

        if (playerInputX > 0f)
        {
            Direction(1);
        }
        else if (playerInputX < 0f)
        {
            Direction(-1);
        }
        
        //if (_isWalking || Input.GetKey(KeyCode.LeftShift))
        //{
        //    _isWalkSfxPlaying = false;
        //}

        //if (playerInputX > 0f)
        //{
        //    Direction(1);
        //}
        //if (playerInputX < 0f)
        //{
        //    Direction(-1);
        //}
    }
    public void JumpInput()
    {
        {
            if(jumpBufferCounter > 0f && coyoteTimeCounter > 0f) 
            {
                SoundManager.instance.PlaySfx(SoundManager.instance.tawanJumpClip);
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);

                float horizontalJumpForce = 0f;
                if(_isRunning)
                {
                    horizontalJumpForce = playerInputX * sprintSpeed;
                }
                else
                {
                    horizontalJumpForce = playerInputX * speed;
                }
                Vector2 finalJumpForce = new Vector2(horizontalJumpForce, jumpForce);
                rb2d.AddForce(finalJumpForce, ForceMode2D.Impulse);

                _isGround = false;
                jumpBufferCounter = 0f;
                coyoteTimeCounter = 0f;
            }
            //if (Input.GetButtonDown("Jump") && _isGround)
            //{
            //    SoundManager.instance.PlaySfx(SoundManager.instance.tawanJumpClip);
            //    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            //    float horizontalJumpForce = playerInputX * speed;

            //    if (Input.GetKey(KeyCode.LeftShift))
            //    {
            //        horizontalJumpForce = playerInputX * sprintSpeed;
            //    }
            //    Vector2 finalJumpForce = new Vector2(horizontalJumpForce, jumpForce);
            //    rb2d.AddForce(finalJumpForce, ForceMode2D.Impulse);

            //    _isGround = false;
            //}
        }
    }
    private void Sprint()
    {
        _isRunning = Input.GetKey(KeyCode.LeftShift);

        if (!_isGround && _isRunning)
        {
            //currentSpeed = 3.5f;
            currentSpeed = sprintSpeed * 0.7f;
        }
        else
        {
            if(_isRunning)
            {
                currentSpeed = sprintSpeed;
            }
            else
            {
                currentSpeed = speed;
            }
        }

        //bool _isRunning = Input.GetKey(KeyCode.LeftShift);

        //if (!_isRunning)
        //{
        //    _isRunSfxPlaying = false;
        //}
        //if (_isRunning)
        //{
        //    currentSpeed = sprintSpeed;
        //    //SoundManager.instance.PlaySfx(SoundManager.instance.tawanRunClip);
        //}
        //else
        //{
        //    currentSpeed = speed;
        //}
        //if (!_isGround && _isRunning)
        //{
        //    currentSpeed = 3.5f;
        //}
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
    public void SelectState()
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
    public void Pause()
    {
        enabled = false;
    }

    public void Resume()
    {
        enabled = true;
    }
}
#endregion
