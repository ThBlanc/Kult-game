using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    public Vector2 _velocity;
    public float _xvalue;
    public float _yvalue;
    public float _turnpercentage = 25;
    public float _speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _velocity = new Vector2(0, 0);
        _rigidbody = GetComponent<Rigidbody2D>();


    }

    void FixedUpdate()
    {
        int changedirectionchances = Random.Range(1, 100);
        if (changedirectionchances < _turnpercentage)
        {
            int directionid = Random.Range(0, 4);

            if (directionid == 0)
            {
                _velocity = Vector2.up * _speed;
            }

            if (directionid == 1)
            {
                _velocity = Vector2.left * _speed;
            }

            if (directionid == 2)
            {
                _velocity = Vector2.down * _speed;
            }

            if (directionid == 0)
            {
                _velocity = Vector2.right * _speed;
            }
        }

        _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);

    }
}
