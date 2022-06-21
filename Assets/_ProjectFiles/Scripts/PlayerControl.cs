using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Update()
    {
        float horiMove = Input.GetAxisRaw("Horizontal");
        if (horiMove > 0)
        {
            _animator.SetBool("isRunning", true);
        }
        else if (horiMove < 0)
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }
}
