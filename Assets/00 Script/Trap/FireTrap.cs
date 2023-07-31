using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] GameObject _gameObj;
    bool _fire;
    Animator _anim;
    private void Start()
    {
        _anim = this.GetComponent<Animator>();
    }
    private void Update()
    {
        if (_fire)
        {
            Invoke("TurnOn", 0.5f);
        }
        else
        {
            TurnOff();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _anim.SetBool("isHit", true);
            _anim.SetBool("fire", true);
            _fire = true;
            //Invoke("TurnOn", 0.5f);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Invoke("TurnOff", 0.1f);
        _anim.SetBool("isHit", false);
        _anim.SetBool("fire", false);
        _fire = false;
    }
    void TurnOn()
    {
        _gameObj.SetActive(true);
    }
    void TurnOff()
    {
        _gameObj.SetActive(false);
    }
}
