using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour
{
    public string _sceneToLoad;
    private bool _isInputing;
    public _levelLoader s_level;
   // [HideInInspector]
    public PlayerController player;
    
    void Start()
    {
        s_level = FindObjectOfType<_levelLoader>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            s_level._isTrans = true;
            _isInputing = true;
        }
        else
        {
             s_level._isTrans = false;
            _isInputing = false;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && _isInputing)
        {
            s_level.f_transition();
        }
        player = col.gameObject.GetComponent<PlayerController>();
    }

    
}
