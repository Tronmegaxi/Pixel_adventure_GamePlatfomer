using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region SINGLETON
    private InputManager _instant;
    public InputManager Instant { get => _instant; }
    private void Awake()
    {
        if (_instant == null) _instant = this;
        if (_instant.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Debug.Log(this.gameObject.name + "has been destroy");
            Destroy(this.gameObject);
        }
    }
    #endregion

   
}
