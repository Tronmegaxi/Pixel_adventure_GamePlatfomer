using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : MonoBehaviour
{
    [SerializeField] float _jumpForce;
    GameObject _gameOBJ;
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
            _gameOBJ= collision.gameObject;
            AddForce();
            //this.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        this.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
    void AddForce()
    {
        _gameOBJ.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(_jumpForce * this.transform.localScale.x,0), ForceMode2D.Impulse);
    }
}
