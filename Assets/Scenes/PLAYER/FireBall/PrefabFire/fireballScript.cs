using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class fireballScript : MonoBehaviour
{
    [SerializeField] private float fireballSpeed;
    [SerializeField] private GameObject fireEffect;
    private Rigidbody2D rigidbody;
    BoxCollider2D skeleton;
    // Start is called before the first frame update
    void Start()
    {
        skeleton = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        //Hp = GetComponent<EnemyHp>();
        rigidbody.velocity = transform.right * fireballSpeed;
        StartCoroutine(Wait());//карутин то бишь задержка 
        //Destroy(this.gameObject, 1f);
    }


    private IEnumerator Wait()//вызывается только с метода IEnumerator & можно использовать несколько задержек
    {
        yield return new WaitForSeconds(1f);
        Instantiate(fireEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        var enemy = collision.GetComponent<EnemyDamage>();
        if (enemy != null)
        {
            Instantiate(fireEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        var ground = collision.GetComponent<BoxCollider2D>();
        if(ground != null)
        {
            Instantiate(fireEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Bat"))
        {
            EnemyHp Hp = collision.GetComponent<EnemyHp>();//обращение на объект класса Хп Врага
            if(Hp.enemyHp != 0)
            {
                Hp.enemyHp -= 1;
            }
            if(Hp.enemyHp ==0)
            {
                
                Destroy(GameObject.Find("batGRFX").GetComponent<CircleCollider2D>());//удаление коллайдера врага, что бы не била несколько раз
            }
        }
        if (collision.CompareTag("Skeleton"))
        {
            EnemyHp Hp = collision.GetComponent<EnemyHp>();//обращение на объект класса Хп Врага
            if (Hp.enemyHp != 0)
            {
                Hp.enemyHp -= 1;
            }
            if (Hp.enemyHp == 0)
            {

                Destroy(GameObject.Find("Skeleton").GetComponent<CircleCollider2D>());//удаление коллайдера врага, что бы не била несколько раз
            }
        }
    }


    
}
