using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad_Horizontal : MonoBehaviour
{
    [SerializeField] float _jumpForce;
    GameObject _gameOBJ;
    int _dir=0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _gameOBJ = collision.gameObject;
            Invoke("Jump", 0.5f);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Invoke("ExitJump", 0.5f);
    }
    void Jump()
    {
        if(this.transform.rotation.z*Mathf.Rad2Deg < 0) { _dir = 1; } else { _dir = -1; }
        _gameOBJ.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(_jumpForce* _dir, 0), ForceMode2D.Impulse);
        this.GetComponent<Animator>().SetBool("Jump", true);
    }
    void ExitJump()
    {
        this.GetComponent<Animator>().SetBool("Jump", false);
    }
}
