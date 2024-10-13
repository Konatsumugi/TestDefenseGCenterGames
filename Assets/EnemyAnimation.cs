using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IsHurt();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            IsDeath();
        }
    }

    public void IsDeath()
    {
        animator.SetBool("IsDeath", true);
        if (GameManager.Ins.enemy <= 0) return;
        GameManager.Ins.enemy--;
    }

    public void IsHurt()
    {
        animator.SetBool("IsHurt", true);
    }

    public void IsDeathOff()
    {
        animator.SetBool("IsDeath", false);
    }

    public void IsHurtOff()
    {
        animator.SetBool("IsHurt", false);
    }
}