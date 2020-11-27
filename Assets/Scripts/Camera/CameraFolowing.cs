using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolowing : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;
    [SerializeField] private float _height;
    [SerializeField] private float _distance;

    private Vector3 _nextPosition;

    private void FixedUpdate()
    {
        _nextPosition = _player.position + Vector3.up * _height + Vector3.back * _distance;
        transform.position = Vector3.Lerp(transform.position, _nextPosition, _speed * Time.deltaTime);
    }
}
