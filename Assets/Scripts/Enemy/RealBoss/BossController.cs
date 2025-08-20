using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("FSM")]
    public BossIdleState bossIdleState;
    public BossFireState bossFireState;
    public BossHappyState bossHappyState;
    BossState state;

    [Space]
    [Header("Variable")]
    Rigidbody2D rb2d;
    Animator anim;
    public bool _isFacingRight = false;
    public Transform playerTarget;

    [Space]
    [Header("GameObject")]
    [SerializeField] GameObject shadow;

    [Space]
    [Header("Settings")]
    public bool isAleart;
    public bool canShoot = true;
    public float shootTimer;
    private float shootDuration = 3f;
    private int attackCount;
    private int currentAttackCount;

    BossHealth status;
    public void Awake()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            playerTarget = player.transform;
        }
        else
        {
            Debug.Log("player not found");
        }

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        status = GetComponent<BossHealth>();
    }
    private void Start()
    {
        attackCount = Random.Range(3, 4);

        bossIdleState.Setup(rb2d, anim, this);
        bossFireState.Setup(rb2d, anim, this);
        bossHappyState.Setup(rb2d, anim, this);
        state = bossIdleState;
    }
    private void Update()
    {
        //Check();
        SelectState();
        state.Do();
    }
    void SelectState()
    {
        if (status.isDead)
            return;
        if(canShoot) 
        {
            shootTimer = 0f;
            state = bossFireState;

            state.Enter();
        }
        else if(!canShoot) 
        {
            if(state != bossIdleState)
            {
                state = bossIdleState;
                state.Enter();
            }
            shootTimer += Time.deltaTime;
            if(shootTimer >= shootDuration)
            {
                canShoot = true;
            }
        }
    }
    void Check()
    {
        if(status.isDead)
        {
            state = bossHappyState;
            state.Enter();
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
    // && currentAttackCount < attackCount             currentAttackCount++;
}
