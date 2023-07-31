using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    Monster_Movement MonsterMovement;
    void Start()
    {
        Monster_Movement MonsterMovement = this.GetComponent<Monster_Movement>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.transform.CompareTag("FeetAttack"))
        {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            MonsterMovement.IsHit = true;
            MonsterMovement.CurrentSpeed = MonsterMovement.RestSpeed;
            Invoke("Death", 0.3f);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            MonsterMovement.CurrentSpeed = MonsterMovement.RestSpeed;
        }
        else
        {
            MonsterMovement.CurrentSpeed = MonsterMovement.Speed;
        }
    }
    void Death()
    {
        this.gameObject.SetActive(false);
    }
}
