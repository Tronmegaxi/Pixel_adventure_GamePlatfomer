using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region SINGLETON
    private static ObjectPool _instant;
    public static ObjectPool Instant { get => _instant; }
    private void Awake()
    {
        if (_instant == null) { _instant = this; }
        if (_instant.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Debug.Log(this.gameObject.name + "has been destroy");
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region OBJECT_POOLING
    Dictionary<GameObject, List<GameObject>> _poolObject = new Dictionary<GameObject, List<GameObject>>();
    public GameObject Get_Obj(GameObject Key)
    {
        List<GameObject> _itemPool = new List<GameObject>();
        if (!_poolObject.ContainsKey(Key))
        {
            _poolObject.Add(Key, _itemPool);
        }
        else
        {
            _itemPool = _poolObject[Key];
        }
        foreach (GameObject g in _itemPool)
        {
            if (g.activeSelf) { continue; }
            else { return g; }
        }
        GameObject g2 = Instantiate(Key, this.transform.position, Quaternion.identity,this.transform);
        _poolObject[Key].Add(g2);
        return g2;
    }
    #endregion
}
