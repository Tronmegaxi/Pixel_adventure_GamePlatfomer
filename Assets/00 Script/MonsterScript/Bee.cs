using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    float _posY;
    void Start()
    {
        _posY = this.transform.position.y;
    }
    void Update()
    {
        Vector3 pos = this.transform.position;
        this.transform.position = new Vector3(pos.x, _posY, pos.z);
    }
}
