using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float _speed = 30f;
    [SerializeField] private float _maxAngle = 75f;
    
    private Rigidbody2D _rb;

    private Vector2 _direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Operations.MoveLeft.Doing)
        {
            _direction = Vector2.left;
        }else if (Operations.MoveRight.Doing)
        {
            _direction = Vector2.right;
        }
        else
        {
            _direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (_direction != Vector2.zero)
        {
            _rb.AddForce(_direction * _speed);
        }
    }

    // 触发器能够完成这个反弹操作吗？甚至都不会损失动能？
    private void OnCollisionEnter2D(Collision2D other)
    {
        Ball ball = other.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            float contactX = other.GetContact(0).point.x;
            float paddleX = transform.position.x;
            float offset = paddleX - contactX;
            float width = other.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.Rigidbody2D.velocity);
            float bounceAngle = (offset / width) * _maxAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -_maxAngle, _maxAngle);
            
            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            // ball.Rigidbody2D.velocity = rotation * Vector3.up * ball.Rigidbody2D.velocity.magnitude;
            ball.Rigidbody2D.velocity = Vector2.zero;
            ball.Rigidbody2D.AddForce(rotation * Vector3.up * ball.Speed);
        }
    }

    public void Reset()
    {
        _rb.position = new Vector2(0, _rb.position.y);
        _rb.velocity = Vector2.zero;
    }
}
