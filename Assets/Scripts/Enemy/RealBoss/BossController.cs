using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("FSM")]
    public BossIdleState bossIdleState;

    [Space]
    [Header("Variable")]
    public bool _isFacingRight = false;

    [Space]
    [Header("GameObject")]
    [SerializeField] GameObject shadow;

    [Space]
    [Header("Settings")]
    public bool isAleart;
}
