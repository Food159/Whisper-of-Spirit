using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TawanTitle : MonoBehaviour
{
    public Vector3 targetpos;
    public float moveSpeed = 5f;
    private bool _isMoving = false;
    public ButtonMenu bttnscript;
    private void Update()
    {
        if(_isMoving)
        {
            Vector3 targetposition = new Vector3(targetpos.x, targetpos.y , targetpos.z);
            transform.position = Vector3.MoveTowards(transform.position, targetpos, moveSpeed * Time.deltaTime);
            if(transform.position.x == targetpos.x)
            {
                //bttnscript.OnButtonPlay();
                SceneManager.LoadScene("SceneOne");
                _isMoving = false;
            }
        }
    }
    public void StartMoving()
    {
        _isMoving = true;
    }
}
