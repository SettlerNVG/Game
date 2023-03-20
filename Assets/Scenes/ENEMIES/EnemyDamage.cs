using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] HealthController healthController;
    bool atack = false;
    Animator animator;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        // point = gameObject.transform.position.x;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            DoDamage();
        }
    }

    void Update()
    {
        
    }
    void DoDamage()
    {
        healthController.PlayerHealth = healthController.PlayerHealth - damage;
        healthController.UpdateHealth();
        // gameObject.SetActive(false);//удаляет объект
    }
    
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DoDamage();
        }
    }
    */
}
