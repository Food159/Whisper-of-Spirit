using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingTitle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private SpriteRenderer _renderer2;
    [SerializeField] private float _x, _y;
    private Vector2 _initialPos;
    private float _width;
    private void Start()
    {
        _initialPos = _renderer.transform.position;
    }
    private void Update()
    {
        _renderer.transform.position += new Vector3(_x, _y) * Time.deltaTime;
        _renderer.transform.position += new Vector3(_x, _y) * Time.deltaTime;
        if(_renderer.transform.position.x >= _width)
        {
            _renderer.transform.position = new Vector2(-_width, _renderer.transform.position.y);
        }
        if (_renderer2.transform.position.x >= _width)
        {
            _renderer2.transform.position = new Vector2(-_width, _renderer2.transform.position.y);
        }
    }
}
