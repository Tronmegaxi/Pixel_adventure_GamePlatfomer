using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject _turtleSpikes;
    Monster_Movement _monsterMovement;
    void Start()
    {
        _monsterMovement = this.GetComponent<Monster_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        _turtleSpikes.transform.position = this.transform.position;
        SpikeController();

    }
    void SpikeController()
    {
        if (_monsterMovement._isAttack == true)
        {
            Invoke("SpikeOn", 0.1f);
        }
        else if (_monsterMovement._isAttack == false)
        {
            Invoke("SpikeOff", 0.1f);
        }
    }
    void SpikeOn()
    {
        _turtleSpikes.SetActive(true);
    }

    void SpikeOff()
    {
        _turtleSpikes.SetActive(false);
    }

}
