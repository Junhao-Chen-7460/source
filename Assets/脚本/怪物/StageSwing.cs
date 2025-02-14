using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSwing : MonoBehaviour
{
    private float _distance;
    private const float _maxDistance = 1f;
    private float _speed = 0.0f;
    private const float _addSpeed = 1f;
    private const float _maxSpeed = 10f;
    private Vector3 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _speed = _maxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_distance > 0f && _speed > 0f)
        {
            _speed -= _addSpeed;
        }
        else if (_distance > 0f && _speed < 0f)
        {
            _speed -= _addSpeed;
        }
        else if (_distance <= 0f && _speed < 0f)
        {
            _speed += _addSpeed;
        }
        else if (_distance <= 0f && _speed > 0f)
        {
            _speed += _addSpeed;
        }
        else if (_speed == 0f)
        {
            if (_distance > 0)
            {
                _speed -= _addSpeed;
            }
            if (_distance < 0)
            {
                _speed += _addSpeed;
            }
        }
        //Debug.Log("_distance: " + _distance);
        //Debug.Log("_speed: " + _speed);
        transform.Translate(Vector3.right * Time.deltaTime * _speed);
        Vector3 tmp = transform.position - _startPos;
        _distance = tmp.x + tmp.z;
    }
}