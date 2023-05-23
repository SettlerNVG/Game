using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPlayer : MonoBehaviour
{
    public GameObject textForHelp;
    public GameObject levelEnd;
    public GameObject controlUI;

    bool canGo = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player")&&GameObject.FindGameObjectWithTag("Skeleton")==null)
        {
            textForHelp.SetActive(true);
            canGo = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textForHelp.SetActive(false);
        }
        canGo = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canGo)
        {
            Time.timeScale = 0f;//остановка игры при паузе
            textForHelp.SetActive(false);
            controlUI.SetActive(false);

            levelEnd.SetActive(true);
        }
    }
}
