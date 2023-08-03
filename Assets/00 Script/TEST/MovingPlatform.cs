using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] _wayPoints;
    [SerializeField] float _speed;
    [SerializeField] int _currentPoint = 0;
    [SerializeField] int i = 0;
    void MovingWay()
    {
        if (_currentPoint >= _wayPoints.Length-1) { i = -1; }
        else if (_currentPoint == 0) { i = 1; }
        if (Vector2.Distance(this.transform.position, _wayPoints[_currentPoint].position) < 0.3f)
        {
            _currentPoint = _currentPoint + i;
        }
        this.transform.position = Vector2.MoveTowards(this.transform.position, _wayPoints[_currentPoint].position, Time.deltaTime * _speed);
    }
    void Update()
    {
        MovingWay();
    }
}

