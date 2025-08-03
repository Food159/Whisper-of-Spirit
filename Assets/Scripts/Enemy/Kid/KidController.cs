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

    [Header("GameObject")]
    [SerializeField] GameObject shadow;

    [Header("Settings")]
    [SerializeField] private float patrolLenght;
    [SerializeField] Transform Area_negX;
    [SerializeField] Transform Area_posX;
    [SerializeField] bool isAleart;
    //[SerializeField] public float attackRange;
    public float sentDirection = 1;
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
}
