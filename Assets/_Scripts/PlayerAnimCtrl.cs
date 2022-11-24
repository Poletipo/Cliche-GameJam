using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimCtrl : MonoBehaviour
{
    public Animator animator;

    public float MovementVelocity = 0;


    private void Update()
    {
        animator.SetFloat("Velocity", MovementVelocity);
    }

    public void AttackAnim()
    {
        animator.SetTrigger("Attack");
    }


    public void HurtAnim()
    {
        animator.SetTrigger("Hurt");
    }


}
