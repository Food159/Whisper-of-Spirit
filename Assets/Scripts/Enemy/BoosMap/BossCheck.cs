using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheck : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyhp;
    [SerializeField] private GameObject pointcheck;
    [SerializeField] private PointCheck pointcheckScript;
    public bool _isBossDie = false;
    private bool hasStarted = false;
    private void Update()
    {
        if (enemyhp.currentHealth <= 0 && hasStarted == false)
        {
            hasStarted = true;
            _isBossDie = true;
            pointcheck.SetActive(true);
            pointcheckScript.StartCheck();
        }
    }
}
