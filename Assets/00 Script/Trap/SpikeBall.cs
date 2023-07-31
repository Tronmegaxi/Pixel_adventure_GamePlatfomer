using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    float _speedROT = 1f;
    [SerializeField] float _speed = 1;
    Rigidbody2D _rigi;
    // Start is called before the first frame update
    void Start()
    {
        _rigi = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_speedROT >= 360)
        {
            _speedROT = 0;
        }
        _speedROT += _speed;
        this.transform.rotation = Quaternion.Euler(0, 0, (_speedROT ));
    }

}
