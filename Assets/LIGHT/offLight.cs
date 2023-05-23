using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offLight : MonoBehaviour
{
    public GameObject light;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            light.SetActive(false);
        }
    }
}
