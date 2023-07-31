using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float _jumpForce;
    GameObject _gameOBJ;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _gameOBJ = collision.gameObject;
            Invoke("Jump", 1);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
            Invoke("ExitJump", 0.3f);
        //}
    }
    void Jump()
    {
        _gameOBJ.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        this.GetComponent<Animator>().SetBool("Jump", true);
    }
    void ExitJump()
    {
        this.GetComponent<Animator>().SetBool("Jump", false);
    }
}
