using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowPlayer : MonoBehaviour
{
    private Rigidbody2D _playerRB;

    [SerializeField] private float _followDistance;
    [SerializeField] private float _distCheck;
    private float _tempFollowDistance;

    private bool isFollowing = true;

    private Rigidbody2D _rb;

    Vector3 _velocity;
    float _smoothTime = .01f;

    void Awake()
    {
        _tempFollowDistance = _followDistance;
        _rb = GetComponent<Rigidbody2D>();
        _playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isFollowing)
        {
            Follow();
        }
    }

    void Follow()
    {
        // Distance between player and follower
        float dist = Vector2.Distance(_rb.position, _playerRB.position);

        if (dist < _distCheck)
        {
            // Follower is to close to the player
            StopAllCoroutines(); // Stop future movements
            _rb.velocity = Vector2.zero; // Stop in place
        }
        else
        {
            // Move closer to player
            StartCoroutine(ReplaceRigidBody(_playerRB.position)); 
        }
    }

    IEnumerator ReplaceRigidBody(Vector2 setTo)
    {
        yield return new WaitForSeconds(_tempFollowDistance);

        Vector3 _desiredPosition = Vector3.SmoothDamp(transform.position, setTo, ref _velocity, _smoothTime);
        transform.position = _desiredPosition;

        //transform.position = setTo;
    }

    void SetRandoms()
    {
        _tempFollowDistance = Random.Range(0, _followDistance);
    }
}
