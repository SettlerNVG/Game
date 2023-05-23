using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthController : MonoBehaviour
{
    public  int PlayerHealth;
    private Vector3 spawnPoint;
    [SerializeField] public Image[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();
    }
    private void Update()
    {
                
    }
    // Update is called once per frame
    public void UpdateHealth()
    {
        for (int i = 0; i<hearts.Length;i++)
        {
            if (i < PlayerHealth)
            {
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].color = Color.black;

            }
        }
    }
}
