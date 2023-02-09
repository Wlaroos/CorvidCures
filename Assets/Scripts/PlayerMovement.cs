using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Vector2 _input;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    [SerializeField] private float _moveSpeed = 5f;

    public bool allowInput = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (allowInput)
        {
            Inputs();
        }
    }

    void Inputs()
    {
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");

        moveCharacter(_input);
    }

    void moveCharacter(Vector2 direction)
    {
        if(_input.x > 0)
        {
            _sr.flipX = true;
        }
        else if (_input.x < 0)
        {
            _sr.flipX = false;
        }
        _rb.velocity = direction * _moveSpeed;
    }

    public void Force(Vector2 dir)
    {
        // Camera Shake
        _rb.AddForce(dir * 750);

    }
}
