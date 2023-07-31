using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.GetComponent<Animator>().SetBool("isPlayer",true);
        }
        StartCoroutine(Exit_IdleState());
    }
    IEnumerator Exit_IdleState()
    {
        yield return new WaitForSeconds(0.17f);
        this.GetComponent<Animator>().SetBool("isPlayer", false);
    }
}

