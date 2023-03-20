using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpHeal : MonoBehaviour
{
    [SerializeField] private int heal;
    [SerializeField] HealthController healthController;
    public Animator animator;
    public bool isHealed = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (healthController.PlayerHealth < 3)
            {
                isHealed = true;
                DoHeal();
                Destroy(animator.gameObject.GetComponent<BoxCollider2D>());//удаление коллайдера, что бы не хиляло несколько раз

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isHealed == true)
        {
            animator.SetFloat("Collected", 1);
        }
        else
        {
            animator.SetFloat("Collected", 0);

        }
    }
    void DoHeal()
    {

        healthController.PlayerHealth = healthController.PlayerHealth + heal;
        healthController.UpdateHealth();
        //gameObject.SetActive(false);//удаляет объект


    }
}
