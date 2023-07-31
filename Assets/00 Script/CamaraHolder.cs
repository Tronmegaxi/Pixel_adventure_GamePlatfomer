using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraHolder : MonoBehaviour
{
    [SerializeField] float _offsetX, _offsetY;
    [SerializeField] public Transform Player;
    float _smoothtime=0.2f;
    Vector3 _velocity=Vector3.zero;
    private void FixedUpdate()
    {
        CameraMove();
    }
    void CameraMove()
    {
        Vector3 Pos=new Vector3(Player.position.x + _offsetX, Player.position.y + _offsetY, -10);
        this.transform.position = Vector3.SmoothDamp(this.transform.position, Pos, ref _velocity, _smoothtime);
    }

}
