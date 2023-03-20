using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public int enemyHp;
    Rigidbody2D rb;
    AIPath aIPath;
    
    void Start()
    {
        
    }

    void Update()
    {
        if(enemyHp==0)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<Rigidbody2D>().mass = 200;
            GetComponent<Animator>().SetTrigger("Death");
            //aIPath.gravity.Set(1, 1, 1);
        }
    }
}
