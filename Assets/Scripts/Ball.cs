using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed = 500;
    
    private Rigidbody2D _rb;

    public Rigidbody2D Rigidbody2D => _rb;

    public float Speed => _speed;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SetTrajectory), 1f);
    }


    private void SetTrajectory()
    {
        Vector2 force = Vector2.down;
        force.x = Random.Range(-1f, 1f);
        
        _rb.AddForce(force.normalized * _speed);
    }

    public void Reset()
    {
        _rb.position = Vector2.zero;
        _rb.velocity = Vector2.zero;
        Invoke(nameof(SetTrajectory), 1f);
    }
}
