using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _levelLoader : MonoBehaviour
{
    public Animator anim;
    [HideInInspector] public bool _isTrans;
    public _SceneManager s_scene;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void f_transition()
    {
        if(_isTrans)
            {
                anim.SetTrigger("isTrans");
                StartCoroutine(f_transTimer());
            }
    }

    public IEnumerator f_transTimer()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(s_scene.player._loadLevel);
    }

    void Update()
    {
     s_scene = player.sceneManager;
    }
}
