using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    public Vector2 _velocity;
    public float _xvalue;
    public float _yvalue;
    public float _speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _velocity = new Vector2(0, 0);
        _rigidbody = GetComponent<Rigidbody2D>();
        _xvalue = Input.GetAxis("Vertical");
        _yvalue = Input.GetAxis("Horizontal");

    }

    void FixedUpdate()
    {
        _xvalue = Input.GetAxis("Horizontal");
        _yvalue = Input.GetAxis("Vertical");
        _velocity = new Vector2(_xvalue * _speed, _yvalue * _speed);
        _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);
    }
}
