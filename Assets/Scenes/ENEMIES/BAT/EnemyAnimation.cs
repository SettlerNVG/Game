using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAnimation : StateMachineBehaviour
{
    public float attackRange;
    Rigidbody2D rb;
    Transform player;
    public float point;
    SpriteRenderer sr;
    Transform bat;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        sr = animator.GetComponent<SpriteRenderer>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bat = GameObject.FindGameObjectWithTag("Bat").GetComponent<CircleCollider2D>().transform;

        if (bat != null)
        {
            if (Vector2.Distance(new Vector2(player.position.x, player.position.y), new Vector2(animator.gameObject.transform.position.x, animator.gameObject.transform.position.y)) <= attackRange)///логика с атакой врага
            {
                animator.SetTrigger("Attack");
            }
        }
        


        ///СЛОЖНАЯ ЛОГИКА С ПЕРЕВОРОТОМ МОДЕЛИ ПО КООРДИНАТАМ
        
        if (animator.transform.position.x > point + 1)
        {
            sr.flipX = false;
            point = animator.transform.position.x;

        }
        else if (animator.transform.position.x < point - 1)
        {
            sr.flipX = true;
            point = animator.transform.position.x;
            point += 0.2f;
        }



    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Atack");


    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
