using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private const float _speedPlayer = 8f;
    private const float _forceJump = 5f;
    private bool _isAerial = false;
    private Rigidbody _rb;
    private string playerTag = null;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        playerTag = gameObject.tag;
    }

    void Update()
    {
        // jumps
        if ((Input.GetKeyDown(KeyCode.C) && playerTag == "P1" && !_isAerial) 
            || (Input.GetKeyDown(KeyCode.P) && playerTag == "P2" && !_isAerial))
        {
            _rb.velocity = _forceJump * new Vector3(0, 5, 0);
            _isAerial = true;
        }
        
        // moves in the air only
        float h = playerTag == "P1" ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("Horizontal2");
        if (h != 0 && _isAerial)
            transform.Translate(new Vector3(h, 0, 0) * (_speedPlayer * Time.deltaTime));
        // only between -3 and 3
        if (transform.localPosition.x < -3)
            transform.Translate(new Vector3(0.5f, 0, 0) * (_speedPlayer * Time.deltaTime));
        if (transform.localPosition.x > 3)
            transform.Translate(new Vector3(-0.5f, 0, 0) * (_speedPlayer * Time.deltaTime));

        // is no more aerial when does not move in y
        if (_rb.velocity.y >= -0.05f && _rb.velocity.y <= 0.05f)
            _isAerial = false;
    }
}
