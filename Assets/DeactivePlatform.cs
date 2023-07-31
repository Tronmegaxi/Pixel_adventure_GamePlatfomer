using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Health-=3;
            //collision.gameObject.GetComponent<Health>().CheckDeath();
        }
        else
        {
            collision.gameObject.SetActive(false);
        }
    }
}
