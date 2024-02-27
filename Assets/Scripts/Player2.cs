using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player2 : MonoBehaviour
{
    private const float _speedPlayer = 8f;
    private const float _forceJump = 5f;
    private bool _isAerial = false;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // jumps
        if (Input.GetKeyDown(KeyCode.Space) && !_isAerial)
        {
            _rb.velocity = _forceJump * new Vector3(0, 5, 0);
            _isAerial = true;
        }
        
        // moves in the air only
        float h = Input.GetAxisRaw("Horizontal2");
        if (h != 0 && _isAerial)
        {
            transform.Translate(new Vector3(h, 0, 0) * _speedPlayer * Time.deltaTime);
        }
        
        // only between -3 and 3
        if (transform.localPosition.x < -3)
            transform.Translate(new Vector3(0.02f, 0, 0));
        if (transform.localPosition.x > 3)
            transform.Translate(new Vector3(-0.02f, 0, 0));

        // is no more aerial when does not move in y
        if (_rb.velocity.y == 0)
            _isAerial = false;
    }
}
