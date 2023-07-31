using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] GameObject _gun, _buletPrefab;
    public bool _isPlayer, _isAttack;
    [SerializeField] float _fireSpeed = 1;
    float _timefireCount;
    void Update()
    {
        _timefireCount -= Time.deltaTime;
        _isAttack = this.GetComponent<Monster_Movement>()._isPlayer;
        this.GetComponent<Monster_Movement>().IsAttack = _isAttack;
        if (_isAttack)
        {
            Fire();
        }
        else if(this.GetComponent<Monster_Movement>()._isHit == false )
        {
            this.GetComponent<Monster_Movement>()._isIdle = true;
        }
    } 
    void Fire()
    {
        if (_timefireCount > 0)
        {
            return;
        }
        _timefireCount = _fireSpeed;
        GameObject Bullet = ObjectPool.Instant.Get_Obj(_buletPrefab);
        Bullet.transform.position = _gun.transform.position;
        Bullet.transform.rotation = Quaternion.Euler(0, (this.transform.localScale.x == 1 ? 180 : 0), 0);
        Bullet.SetActive(true);
    }
}
