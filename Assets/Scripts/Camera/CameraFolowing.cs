using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolowing : MonoBehaviour
{
    [SerializeField] private Rigidbody _player;
    [SerializeField] private float _speed;
    [SerializeField] private float _angle;
    [SerializeField] private float _distance;

    private Vector3 _nextPosition;

    private void OnValidate()
    {
        transform.position = _player.position;
        transform.position += Vector3.up * Mathf.Cos(Mathf.Deg2Rad * _angle) * _distance;
        transform.position += Vector3.back * Mathf.Sin(Mathf.Deg2Rad * _angle) * _distance;
    }

    private void FixedUpdate()
    {
        _nextPosition = _player.position + _player.velocity.normalized * 2f;
        _nextPosition += Vector3.up * Mathf.Cos(Mathf.Deg2Rad * _angle) * _distance;
        _nextPosition += Vector3.back * Mathf.Sin(Mathf.Deg2Rad * _angle) * _distance;
        transform.position = Vector3.Lerp(transform.position, _nextPosition, _speed * Time.deltaTime);
    }
}
