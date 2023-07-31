using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("Falling", 1);
            Invoke("Deactive", 5);

        }
    }
    void Falling()
    {
        this.gameObject.AddComponent<Rigidbody2D>().gravityScale = 1;
    }
    void Deactive()
    {
        this.gameObject.SetActive(false);
    }
}
