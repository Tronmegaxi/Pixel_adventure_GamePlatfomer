using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreak : MonoBehaviour
{
    [SerializeField] GameObject _sfx;
    void Start()
    {
        StartCoroutine(DestroyObj());
    }
    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        this.gameObject.SetActive(false);
    }
}
