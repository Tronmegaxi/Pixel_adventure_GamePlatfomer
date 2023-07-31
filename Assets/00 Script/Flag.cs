using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] bool _isWin=false;
    public bool IsWin { get => _isWin; set => _isWin = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isWin = true;
            MusicPlayer.Instant.PlaySFX("Checkpoint_2");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isWin = false;
        }
    }
}
