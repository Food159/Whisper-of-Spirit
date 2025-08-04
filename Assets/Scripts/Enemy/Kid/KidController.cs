using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidController : MonoBehaviour
{
    [Header("FSM")]
    public KidIdleState kidIdestate;
    public KidWalkState kidWalkstate;
    public KidShootState kidShootstate;
    public KidHappyState kidHappystate;
    KidState state;

    [Header("Variable")]
    public bool _isFacingRight = false;
    Rigidbody2D rb2d;
    Animator anim;
    public Transform playerTarget;
    public LayerMask playerLayer;
    private float distance;
    public float attackRange = 5f;
    public float yPlayer = 2f;

    [Header("GameObject")]
    [SerializeField] GameObject shadow;

    [Header("Settings")]
    [SerializeField] private float patrolLenght;
    [SerializeField] Transform Area_negX;
    [SerializeField] Transform Area_posX;
    [SerializeField] bool isAleart;
    //[SerializeField] public float attackRange;
    public float sentDirection = -1;
    public bool isOutofArea;
    EnemyHealth status;
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
        status = GetComponent<EnemyHealth>();
    }
    private void Start()
    {
        kidIdestate.Setup(rb2d, anim, this);
        kidWalkstate.Setup(rb2d, anim, this);
        kidShootstate.Setup(rb2d, anim, this);
        kidHappystate.Setup(rb2d, anim, this);
        state = kidIdestate;
    }
    private void Update()
    {
        LookForPlayer();
        SelectState();
        Check();
        state.Do();
        if (playerTarget.position.x > transform.position.x && !_isFacingRight)
        {
            Flip();
        }
        else if (playerTarget.position.x < transform.position.x && _isFacingRight)
        {
            Flip();
        }
    }
    void SelectState()
    {
        if (status._isDead)
        {
            return;
        }
        float distance = DistanceCal();
        if (isAleart)
        {
            if(distance <= attackRange)
            {
                state = kidShootstate;
            }
            else
            {
                state = kidWalkstate;
            }
        }
        else
        {
            state = kidWalkstate;
        }
        state.Enter();
    }
    public void Flip()
    {
        if (status._isDead)
            return;
        _isFacingRight = !_isFacingRight;
        Vector3 direction = transform.localScale;
        direction.x *= -1;
        transform.localScale = direction;
        sentDirection *= -1;
    }
    void LookForPlayer()
    {
        float distance = DistanceCal();
        float yOffset = Mathf.Abs(playerTarget.position.y - transform.position.y);

        Vector2 rayDirectionLeft = Vector2.left;
        Vector2 rayDirectionRight = Vector2.right;

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, rayDirectionLeft, patrolLenght, playerLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, rayDirectionRight, patrolLenght, playerLayer);

        Debug.DrawRay(transform.position, rayDirectionLeft * patrolLenght, Color.red);
        Debug.DrawRay(transform.position, rayDirectionRight * patrolLenght, Color.red);

        //if ((hitLeft.collider != null || hitRight.collider != null) && DistanceCal() <= attackRange)
        if(distance <= patrolLenght && yOffset <= yPlayer)
        {
            isAleart = true;
        }
        else
        {
            isAleart = false;
        }
    }
    void Check()
    {
        if (status._isDead)
        {
            state = kidHappystate;
            state.Enter();
        }
    }
    public float DistanceCal()
    {
        float distance = Mathf.Abs(playerTarget.position.x - transform.position.x);
        return distance;
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
