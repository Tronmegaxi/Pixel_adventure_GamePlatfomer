using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem _movementParticle;
    [SerializeField] ParticleSystem _fallParticle;

    [Range(0, 10)]
    [SerializeField] float _occurAfterVelocity;
    [Range(0, 0.2f)]
    [SerializeField] float _dustFomationPeriod;
    [SerializeField] Rigidbody2D _rigibody2D;
    float _counter;
    bool _isOnGround;
    private void Update()
    {
        _counter += Time.deltaTime;
        
        if (_isOnGround && Mathf.Abs(_rigibody2D.velocity.x) > _occurAfterVelocity)
        {
            if (_counter > _dustFomationPeriod)
            {
                _movementParticle.Play();
                _counter = 0;
            }
        }
        else
        {
            _movementParticle.Stop();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            _isOnGround = true;
            _fallParticle.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            _isOnGround = false;
        }
    }
}
