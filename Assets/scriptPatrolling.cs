using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class scriptPatrolling : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    public float point;//точка самого врага
    [SerializeField] private float range;
    [SerializeField] public Transform enemy;
    public Transform[] points;
    private int i;//логика
    SpriteRenderer sprite;
    EnemyHp health;
    Animator animator;

    [Header("Player settings")]
    Transform target;
    [SerializeField] private CircleCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] Transform atackPoint;

    public void Start()
    {
        enemy = gameObject.GetComponent<Transform>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        health = gameObject.GetComponent<EnemyHp>();
        animator = gameObject.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center - gameObject.transform.position / range * -transform.localScale.x,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center - gameObject.transform.position / range * -transform.localScale.x,
          new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }


    void Update()
    {
        if (gameObject.transform.position.x > point + 0.5f)
        {

            sprite.flipX = false;

            point = animator.transform.position.x;

        }
        else if (animator.transform.position.x < point - 0.5f)
        {
            sprite.flipX = true;
            point = animator.transform.position.x;
            point += 0.2f;
        }
        if (health.enemyHp != 0)
        {
            if(PlayerInSight())
            {
                speed = 3f;
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
                animator.SetBool("Run", true);
                if (Vector2.Distance(transform.position, points[i].position) < 1f)
                {
                    if (i > 0)
                    {
                        sprite.flipX = true;

                        i = 0;
                    }
                    else          //логика под патрулирование
                    {
                        sprite.flipX = false;
                        i = 1;
                    }
                }
            }
            
        }

        else
        {
            speed = 0;
        }
    }



}
