using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    // Gravity Scale editable on the inspector
    // providing a gravity scale per object
    public float gravityScale = 1.0f;
 
    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.
    public static float globalGravity = -9.81f;
 
    Rigidbody m_rb;
    private GameObject _player = null;

    void Start()
    {
        _player = gameObject;
    }
    
    void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }
 
    void FixedUpdate()
    {
        if (_player.GetComponent<Rigidbody>().velocity.y == 0)
            gravityScale = 1.0f;
        
        if (_player.GetComponent<Rigidbody>().velocity.y > 0)
        {
            gravityScale = _player.transform.position.y >= transform.position.y ? gravityScale * 1.3f : gravityScale * 1.1f;
            Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            m_rb.AddForce(gravity, ForceMode.Acceleration);
        }
        
        if (_player.GetComponent<Rigidbody>().velocity.y < 0)
        {
            Vector3 gravity = globalGravity * 2 * Vector3.up;
            m_rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}
