using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    [SerializeField] float _speed = 10f, _lifeTime = 4;
    [SerializeField] GameObject[] _bulletBreak;
    Rigidbody2D _rigi;
    private void Start()
    {
        _rigi = this.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        StartCoroutine(AutoDestructBullet(this.gameObject));
    }
    private void Update()
    {
        _rigi.velocity = _speed * Time.deltaTime * this.transform.right ;
    }
    IEnumerator AutoDestructBullet(GameObject g)
    {
        yield return new WaitForSeconds(_lifeTime);
        g.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("bullet collision "+collision.gameObject.name);
        this.gameObject.SetActive(false);
        BulletBreakPiece();
    }
    void BulletBreakPiece()
    {
        for (int i = 0; i < _bulletBreak.Length; i++)
        {
           // Instantiate(_bulletBreak[i], this.transform.position + new Vector3(i * 0.4f, 0, 0), Quaternion.identity);
            GameObject BulletPiece = ObjectPool.Instant.Get_Obj(_bulletBreak[i]);
            BulletPiece.transform.position = this.transform.position + new Vector3(i * 0.4f, 0, 0);
            BulletPiece.transform.rotation = Quaternion.identity;
            BulletPiece.SetActive(true);
        }
    }
}
