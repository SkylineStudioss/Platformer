using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _enemy_2 : MonoBehaviour
{
    public float _speed;
    public Transform _playerCheck;
    [SerializeField]
    private Rigidbody2D _rb;
    private bool _attackPlayer;
    private Vector2 _target;

    void Update()
    {
        _approachPlayer();
        _findPlayer();
    }

    void _findPlayer()
    {
        RaycastHit2D _hitInfo = Physics2D.Raycast(_playerCheck.position, _playerCheck.right);//Checks if enemy sees player
        //_target = _hitInfo.transform.position;
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

    void _approachPlayer()
    {
        if(_attackPlayer)
        {
           // Vector2.MoveTowards(transform.GetComponent<_TestMovement>());
           _rb.velocity = new Vector2(_speed, _rb.velocity.y);
           Debug.Log("Hello There!");
        }
    }
}
