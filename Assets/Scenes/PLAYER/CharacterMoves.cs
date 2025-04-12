using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CharacterMoves : MonoBehaviour
{
    [Header("Player settings")]
    [SerializeField] private float runSpeed;//скорость движения
    [SerializeField] private int lives;//количество жизней
    [SerializeField] private float jumpForce;//сила прыжка
    [SerializeField] public static bool canMove = true;//может ли проигрываться след анимация
    [SerializeField] private bool facing = true;//переменная для остлеживания поворота игрока
    [SerializeField] private int maxMana = 2;
    [SerializeField] private int currentMana;
    [SerializeField] public int collectedKeys = 0;



    [Space]
    [Header("Fireball settings")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireBall;


    [Space]
    [Header("Ground check settings")]
    public bool isGrounded = true;
    public float checkGroundOffsetY = -1.8f;
    public float checkGroundRadius = 0.3f;
    public EnemyHp EnemyHp;

    [Space]
    [Header("Setting of enemies push")]
    [SerializeField] private float powForce = 3f;
    [SerializeField] private float directionOfPush = 1f;

    public Animator animator;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Vector3 dir;
    float jump = 0;//переменные для анимации
    float horizontalMove = 0f;//переменные для анимации
//    bool keysActive = false;//переменная для проверки зажатых клавиш
    [Space]
    [Header("HealthController")]
    [SerializeField] HealthController healthController;//создание не статичного объекта класса

    private Vector3 spawnPoint;// спавн
    public GameObject fallDetector;//площадка выпада за мир
    public ManabarScript manabar;//slider с маной
    public int value;

    private void Flip()//метод для поворота
    {
        facing = !facing;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Start()
    {
        InvokeRepeating("regenMana", 0, 2f);

        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        spawnPoint = transform.position;
        //скрипт для маны
        currentMana = maxMana;
        manabar.SetMaxMana(maxMana);
        value = healthController.PlayerHealth;


    }


    public void Run(float move)//бег
    {
        dir = new Vector3(Input.GetAxis("Horizontal"), 0);
        if (move > 0 && !facing)
        {
            //поворот
            Flip();
        }
        else if (move < 0 && facing)
        {
            Flip();
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, runSpeed * Time.deltaTime);
        //spriteRenderer.flipX = dir.x > 0.0f ? false : true;//смена направления
    }

    private void Atack()
    {
        animator.SetFloat("Atacking", 1);
        currentMana -= 1;//при атаке убавляется мана
        manabar.SetMana(currentMana);
    }
    public void OnLanding()//изменение анимации
    {
        animator.SetBool("IsJumping", false);
    }


    public void Jump()//прыжок
    {
        rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            rigidbody2D.AddForce(transform.up * 10, ForceMode2D.Impulse);//толчок от огня
        }
    }
    */
    private void CheckGround()//проверка нахождения на земле
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll
            (new Vector2(transform.position.x, transform.position.y + checkGroundOffsetY), checkGroundRadius);
        if (colliders.Length > 1)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    public void FixedUpdate()
    {
        CheckGround();
        Hurts();

    }
    void OnTriggerEnter2D(Collider2D collision)//смерть fallDetector
    {

        if (collision.CompareTag("FallDetector"))//при падении смерть
        {
            transform.position = spawnPoint;
            healthController.PlayerHealth = 0;
        }

    }

    private IEnumerator WaitDeath()//вызывается только с метода IEnumerator & можно использовать несколько задержек
    {
        yield return new WaitForSeconds(1f);
        transform.position = spawnPoint;
        healthController.PlayerHealth = 3;
        Destroy(this.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        canMove = true;

    }

    private void regenMana()//регенерация маны
    {
        if(currentMana<maxMana)

        {
            currentMana += 1;
            manabar.SetMana(currentMana);//проверка восстановления
        }
        
    }
    public void Hurts()
    {
        if (healthController.PlayerHealth < value)
        {
            animator.SetBool("Hurts", true);
        }
        value = healthController.PlayerHealth;

    }
    public void Update()
    {
        /*
        for (int i = 0; i < healthController.hearts.Length; i++)//изменение анимации на получение урона
        {
            if (i < healthController.PlayerHealth)
            {
                animator.SetTrigger("Hurts");
            }
        }
        */
        //АНИМАЦИЯ ДЛЯ ПОЛУЧЕНИЯ УРОНА ПЕРСОНАЖЕМ




        if (healthController.PlayerHealth <= 0)//счет жизней//смерть
        {
            canMove = false;
            animator.SetFloat("Die", 1);
            Destroy(GameObject.Find("Player").GetComponent<CircleCollider2D>());//удаление коллайдера при смерти
            StartCoroutine(WaitDeath());
        }


        if (Input.GetKeyDown(KeyCode.F) && currentMana != 0)//атака
        {
            Atack();
            Instantiate(fireBall, firePoint.position, firePoint.rotation);
            Input.GetKeyUp(KeyCode.F);
        }
        else if (Input.GetKeyUp(KeyCode.F))//если кнопку отпустили не атакуем
        {
            animator.SetFloat("Atacking", -1);
        }

        if (Input.GetButton("Horizontal") && canMove == true)//бег
        {
            Run(horizontalMove * Time.fixedDeltaTime);
        }

        //jump = Input.GetAxisRaw("Jump") * jumpForce;//прыжок
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;//переменная для анимации 


        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));//изменение на бег под повороты


        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);//проваливании под землю


        /*
        if (Input.GetButtonDown("Jump"))
        {
              jump = true;
            animator.SetBool("IsJumping", true);
            animator.SetFloat("IsJumping", Mathf.Abs(jump));
            rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        animator.SetBool("IsJumping", false);

        OnLanding();
        */

        if (isGrounded == true && Input.GetButtonDown("Jump") && canMove == true)
        {
            Jump();

            animator.SetFloat("IsJumping", 1);
        }
        else if (isGrounded == true)
        {
            animator.SetFloat("IsJumping", -1);
        }
        else if (canMove == false)// когда разжата клавиша атаки
        {
            animator.SetFloat("IsJumping", -1);
        }
        else if (isGrounded == false && canMove == true)//если не на земле и нажата клавиша атаки
        {
            animator.SetFloat("IsJumping", 1);
        }
    }

}