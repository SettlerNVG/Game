using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHurts : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Fireball"))
        {
            HurtsEnemy();
        }
    }
    void HurtsEnemy()
    {

        animator.SetTrigger("Hurts");
    }
}
