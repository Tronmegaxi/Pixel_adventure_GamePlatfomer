using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBreakPiece : MonoBehaviour
{
    [SerializeField] float _lifeTime = 1.5f;
    private void FixedUpdate()
    {
        StartCoroutine(AutoDestruct());
    }
    IEnumerator AutoDestruct()
    {
        yield return new WaitForSeconds(_lifeTime);
        this.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
