using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TestMovement : MonoBehaviour
{
    private float _moveDirection;
    [SerializeField]
    private Rigidbody2D _rb;
    public float _speed;
    private float _jumpAmount = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _walk();
         if (Input.GetKeyDown(KeyCode.Space))
    {
        _rb.AddForce(Vector2.up * _jumpAmount, ForceMode2D.Impulse);
    }
    }

    void _walk()
    {
        _moveDirection = Input.GetAxisRaw("Horizontal");

        _rb.velocity = new Vector2(_moveDirection * _speed, _rb.velocity.y);
    }
   
}
