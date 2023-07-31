using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerState = PlayerMovement.PlayerState;
public class AnimationControllerBase : MonoBehaviour
{
    Animator _animator;
    void Start()
    {
        _animator = this.GetComponent<Animator>();
    }
    public void UpdateAnimation(PlayerState playerState)
    {
        for (int i = 0; i <= (int)PlayerState.DASH; i++)
        {
            string stateName = ((PlayerState)i).ToString();
            if (playerState == (PlayerState)i)
            {
                _animator.SetBool(stateName, true);
            }
            else
            {
                _animator.SetBool(stateName, false);
            }
            
        }
    }
}
