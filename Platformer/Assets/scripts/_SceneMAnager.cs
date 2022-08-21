using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneMAnager : MonoBehaviour
{
    public string _sceneToLoad;
    bool _isInputing;
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            _isInputing = true;
        }
        else
        {
            _isInputing = false;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && _isInputing)
        {
                SceneManager.LoadScene(_sceneToLoad);
        }
    }
}
