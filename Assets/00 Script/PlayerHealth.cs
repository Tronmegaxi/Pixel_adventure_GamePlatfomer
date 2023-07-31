using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerMovement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject[] Hearts;
    [SerializeField] GameObject Heart_System;

    Rigidbody2D _rigi;
    [SerializeField] int _health = 1, _maxHealth = 10000;
    public int Health { get => _health; set => _health = value; }
    void Start()
    {
        _maxHealth = (int)(3 - PlayerPrefsController.GetDifficulty());
        _rigi = this.GetComponent<Rigidbody2D>();
        InvokeRepeating("CheckGethit", 0, 1f);
    }
    [SerializeField] bool _isHit;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.transform.CompareTag("Dangerous"))
        {
            GetHit();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.transform.CompareTag("Dangerous"))
        {
            _isHit = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _isHit = false;
    }
    private void Update()
    {
        CheckDeath();
        HeartSystem();
    }
    public void CheckDeath()
    {
        if (_health >= _maxHealth)
        {
            _health = _maxHealth;
        }
        if (_health <= 0)
        {
            _rigi.bodyType = RigidbodyType2D.Static;
            StartCoroutine(restartLevel());
        }
    }
    IEnumerator restartLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _health = 3;
    }
    void HeartSystem()
    {
        foreach (GameObject heart in Hearts)
        {
            heart.gameObject.SetActive(false);
        }
        for (int i = 0; i < _health; i++)
        {
            Hearts[i].gameObject.SetActive(true);
        }
    }
    void GetHit()
    {
       _health--;
        this.GetComponent<PlayerMovement>().GetHit();
        MusicPlayer.Instant.PlaySFX("GetHit");
    }
    void CheckGethit()
    {
        if (_isHit)
        {
            GetHit();
        }
    }
}
