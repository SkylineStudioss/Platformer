using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _enemy_2 : MonoBehaviour
{
    public float _speed;
    public Transform _playerCheck;
    public Transform _playerBehind;
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private Transform enemy;
    private bool _attackPlayer;
    private Vector2 _target;
    private bool facingRight = true;
    [HideInInspector]public bool b_enemyTwo;

    void Update()
    {
        _approachPlayer();
        _findPlayer();
        _findBack();
    }

    void _findPlayer()
    {
        RaycastHit2D _hitInfo = Physics2D.Raycast(_playerCheck.position, _playerCheck.right);//Checks if enemy sees player

        if(_hitInfo)
        {
            if(_hitInfo.transform.GetComponent<_TestMovement>())
            {
                _attackPlayer = true;
            }
            else
            {
                _attackPlayer = false;
            }
        }
    }

    void _findBack()
    {
        RaycastHit2D _behindInfo = Physics2D.Raycast(_playerBehind.position, _playerBehind.right);//Checks if player is behind enemy
        if(_behindInfo)
        {
        if(_behindInfo.transform.GetComponent<_TestMovement>())
        {
            Flip();
            _speed = _speed * -1;
            Debug.Log(_speed);
        }
        }
    }

    void _approachPlayer()
    {
        if(_attackPlayer)
        {
           _rb.velocity = new Vector2(_speed, _rb.velocity.y);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            b_enemyTwo = true;
        }
        else
        {
            b_enemyTwo = false;
                return;
        }
    }
}
