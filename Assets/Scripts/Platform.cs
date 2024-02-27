using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    private GameObject _player = null;
    private GameObject _cam = null;
    private bool _hasCopied = false;
    private Collider _collider;
    private Collider _collider1;
    [SerializeField] private string playerTag = null;
    [SerializeField] private string camTag = null;

    void Start()
    {
        // get player
        _player = GameObject.FindWithTag(playerTag);
        
        // get camera
        _cam = GameObject.FindWithTag(camTag);
        
        // disable box collider
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
        
    }

    void Update()
    {
        // enable box collider of platform when player just on top
        if (_player.transform.position.y >= transform.position.y + 1f)
            _collider.enabled = true;
        else if (_player.transform.position.y < transform.position.y + 1f
                 || _player.transform.position.y >= transform.position.y + 3f)
            _collider.enabled = false;

        // create copy of platform when player above one platform once !
        if (_player.transform.position.y >= transform.position.y + 5f && !_hasCopied)
        {
            int change = (Random.Range(0, 2) * 2 - 1); // -1 (do not change) or 1 (change), 
            float rdmAdd = Random.Range(-0.5f, 0.5f);
            float posX = change < 0 ? transform.position.x + rdmAdd : // if - 1 do not change pos
                transform.position.x < _cam.transform.position.x ? _cam.transform.position.x + 1.3f + rdmAdd : // else inverse pos
                _cam.transform.position.x - 1.3f + rdmAdd;
            posX = posX < _player.transform.position.x - 2.7f ? 
                _player.transform.position.x - 2.7f : 
                posX > _player.transform.position.x + 2.7f ? 
                    _player.transform.position.x + 2.7f : 
                    posX;
            Vector3 position = transform.position;
            GameObject newPlatform = Instantiate(gameObject, new Vector3(posX, position.y + 8f, position.z),
                Quaternion.identity);
            if (Math.Abs(newPlatform.transform.position.y % 20f) <= 1.3f) // for each platform multiples of 34 widen the platform
                newPlatform.transform.localScale = new Vector3(26, 1, 1);
            else
                newPlatform.transform.localScale = new Vector3(1, 1, 1);
            _hasCopied = true;
        }
    }
}
