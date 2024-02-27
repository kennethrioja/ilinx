using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Number : MonoBehaviour
{
    private GameObject _player = null;
    private GameObject _opponent = null;
    private TMP_Text _text = null;
    [SerializeField] private string playerTag;
    [SerializeField] private string opponentTag;

    void Start()
    {
        // get player
        _player = GameObject.FindWithTag(playerTag);
        // get text
        _text = gameObject.GetComponent<TMP_Text>();
        // get opponent
        _opponent = GameObject.FindWithTag(opponentTag);

    }
    
    void Update()
    {
        // update text
        _text.text = Mathf.RoundToInt(_player.transform.position.y - 1.5f).ToString();
        // red if last position, green if first position
        if (Mathf.RoundToInt(_player.transform.position.y) > Mathf.RoundToInt(_opponent.transform.position.y))
            _text.color = Color.green;
        else if (Mathf.RoundToInt(_player.transform.position.y) < Mathf.RoundToInt(_opponent.transform.position.y))
            _text.color = Color.red;
        else
            _text.color = Color.white;
    }
}
