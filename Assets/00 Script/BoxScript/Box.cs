using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class Box : MonoBehaviour
{
    [SerializeField] int _healthBox = 2;
    [SerializeField] GameObject _boxObject;
    [SerializeField] GameObject[] _boxBreak;
    [SerializeField] float _offsetX = 0, _offsetY = 0,_rotation=0;
    GameObject _player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _player = collision.gameObject;
        if (collision != null && collision.gameObject.CompareTag("Player"))
        {
            _healthBox--;
            MusicPlayer.Instant.PlaySFX("HitBox");
        }
        addjump();
    }
    private void Update()
    {
        BoxBreak();
    }
    void addjump()
    {
        _player.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3);
    }
    void BoxPiece()
    {
        for (int i = 0; i < _boxBreak.Length; i++)
        {
            GameObject BoxBreak = ObjectPool.Instant.Get_Obj(_boxBreak[i]);
            BoxBreak.transform.position = _boxBreak[i].transform.position;
            BoxBreak.transform.rotation = this.transform.rotation;
            BoxBreak.SetActive(true);
        }
    }
    void BoxBreak()
    {
        Vector3 Pos = this.transform.position;
        if (_healthBox <= 0)
        {
            this.gameObject.SetActive(false);
            GameObject BoxObject = ObjectPool.Instant.Get_Obj(_boxObject);
            BoxObject.transform.position = new Vector3(Pos.x + _offsetX, Pos.y + _offsetY, 0);
            BoxObject.transform.rotation = this.transform.rotation;
            BoxObject.SetActive(true);
            BoxPiece();
        }
    }
}
