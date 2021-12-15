using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Entity
{
    //звуки
    //private AudioSource jumpSound;
    //[SerializeField] private AudioSource DamageSound;

    [SerializeField] private float speed = 3f; //скорость
    //[SerializeField] private int lives = 5; // количество жизней
    [SerializeField] private int health; // количество жизней
    [SerializeField] private float jumpForce = 13f; //сила прыжка
    private bool isGrounded = false; //проверка земли

    [SerializeField] private Image[] hearts;//массив сердец

    [SerializeField] private Sprite aliveHeart;//полное сердце
    [SerializeField] private Sprite deadHeart;//разбитое сердце:(
    [SerializeField] private GameObject losePanel; //lose panel

    public bool isAttacking = false;//атакуем ли мы сейчас
    public bool isRecharged = true;//перезарядились ли мы

    public Transform attackPos;//позиция атаки
    public float attackRange;//дальнотсь атаки
    public LayerMask enemy;//слой с врагами

    [SerializeField] GameObject itogi;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        lives = 5;
        health = lives;
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        isRecharged = true;//на начало игры мы перезаряжены
    }

    private void FixedUpdate()
    {
        CheckGrounded();
    }

    private void Update()
    {
        if (isGrounded && !isAttacking) State = States.idle;

        if (!isAttacking && Input.GetButton("Horizontal"))
            Run();
        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
        if (Input.GetButtonDown("Fire1")) //бьем на лкм
            Attack();

        if (health > lives)
            health = lives;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = aliveHeart;
            else
                hearts[i].sprite = deadHeart;

            if (i < lives)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }

    private void Run()
    {
        if (isGrounded) State = States.run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        //jumpSound.Play();
    }

    //Атака
    private void Attack()
    {
        if (isGrounded && isRecharged)
        {
            State = States.attack;
            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }

    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Entity>().GetDamage();
        }
    }

    //отображение радиуса атаки меча
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    //проверка земли
    private void CheckGrounded()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;

        if (!isGrounded) State = States.jump;
    }

    public void GetDamage()
    {
        health -= 1;
        //damageSound.Play();
        if (health == 0)
        {
            foreach (var h in hearts)
                h.sprite = deadHeart;
            Die();
        }
    }

    //появление панели смерти
    public override void Die()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }


    private IEnumerator AttackAnimation()//время атаки
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()//перезарядка
    {
        yield return new WaitForSeconds(0.5f);
        isRecharged = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            itogi.SetActive(true);
            Time.timeScale = 0;
        }

    }
}

public enum States
{
    idle, //0
    run,
    jump,
    attack
}
