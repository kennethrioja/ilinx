using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraMovement : MonoBehaviour
{
    private GameObject _player = null;
    private Vector3 _offset;
    private const float _smoothTime = 0.25f;
    private const float _smoothTimeX = 0.5f;
    private Vector3 _currentVelocity = Vector3.zero;
    private Rigidbody _playerRigidbody;

    [SerializeField] private string playerTag;

    private void Awake()
    {
        _player = GameObject.FindWithTag(playerTag);
        _playerRigidbody = _player.GetComponent<Rigidbody>();
        Vector3 currPos = transform.position;
        _offset = new Vector3(currPos.x, currPos.y - _player.transform.position.y, currPos.z);
    }
    
    private void LateUpdate()
    {
        // follows player if goes up
        if (_player.transform.position.y > transform.position.y + 1.5f && _playerRigidbody.velocity.y > 0f)
        {
            Vector3 targetPos = new Vector3(0, _player.transform.position.y, 0) + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos,
                ref _currentVelocity, _smoothTime);
        }
        
        // follows player if falls down
        if (_player.transform.position.y < transform.position.y - 3.4f)
        {
            Vector3 targetPos = new Vector3(0, _player.transform.position.y, 0) + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos,
                ref _currentVelocity, _smoothTimeX * _smoothTime);
        }
    }
}
