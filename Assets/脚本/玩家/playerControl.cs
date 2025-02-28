using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float runSpeed = 500.0f;
    public float jumpForce = 500f;
    public int jumpNumber = 0;
    private int MaxJump = 1;
    private float lvlUpTime = 10f;
    private float lvl = 0;

    [SerializeField] AudioClip levelUpSFX;
    [SerializeField] AudioClip ShootSFX;
    [SerializeField] AudioSource audioSource;


    [SerializeField] float shootForce = 2000f;
    private float spawnDistance = 1.5f;

    private float timer;

    private bool canShoot = false;

    [SerializeField] GameObject projectiles;
    [SerializeField] private Rigidbody2D myRigidbody;     //声明刚体组件对象，用于获取玩家刚体 ，[SerializeField]私有变量可观察
    [SerializeField] private Animator myAnim;           //声明动画控制器对象，用于获取玩家动画控制器
    public Collider2D myCollider2D;    //声明2D碰撞体对象，用于获取玩家的碰撞体
    public LayerMask ground;        //声明图层对象，用于获取地板

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();     //在代码中直接获取角色的刚体，也可以通过拖拽方式获取
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SwitchAnim();
        HandleShoot();
        timer += Time.deltaTime;
        if (timer >= lvlUpTime)
        {
            timer = 0f;
            lvlUp();
        }
        

        //————————————————控制角色跑动跳跃功能-——————————————————————————————————




    }
    void Movement()
    {



        float Horizontal_move = Input.GetAxis("Horizontal");  //获取Horizontal操作事件的输入值，从-1到1的浮点值，无输入为0

        float moveDir = Input.GetAxisRaw("Horizontal");  //获取Horizontal操作事件的输入值，只会有-1，0，1三个值

        if (Horizontal_move != 0)
        {
            myRigidbody.velocity = new Vector2(Horizontal_move * runSpeed * Time.deltaTime, myRigidbody.velocity.y);  //将x，y轴的速度赋予角色刚体的速度上
            myAnim.SetFloat("RunSpeed", Mathf.Abs(moveDir));         //通过操作事件来判断角色的移动状态
        }

        if (moveDir != 0)
        {
            transform.localScale = new Vector3(moveDir, 1, 1);  //通过改变缩放的正负值，实现图片的翻转
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpNumber < MaxJump)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce * Time.deltaTime);  //将x，y轴的速度赋予角色刚体的速度上
                myAnim.SetBool("Jumping", true);

                jumpNumber++;
            }

        }
    }

    void SwitchAnim()
    {
        myAnim.SetBool("idle", false);

        if (myAnim.GetBool("Jumping"))
        {
            if (myRigidbody.velocity.y <= 0)    //通过刚体Y轴的速度判断角色是否在下落状态
            {
                myAnim.SetBool("Jumping", false);
                myAnim.SetBool("Falling", true);
            }

        }
        else if (myCollider2D.IsTouchingLayers(ground))  //假如角色的碰撞体碰到目标图层里的所有对象
        {
            myAnim.SetBool("Falling", false);
            myAnim.SetBool("idle", true);

            jumpNumber = 0;
        }
    }

    void lvlUp()
    {
        MaxJump++;
        lvl++;
        if (levelUpSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(levelUpSFX);
        }

        if (lvl >= 3)
        {
            canShoot = true;
        }
    }
    void HandleShoot()
    {
        if (canShoot && Input.GetKeyDown(KeyCode.F))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            Vector3 shootDirection = (mousePos - transform.position).normalized;
            Vector3 spawnPosition = transform.position + shootDirection * spawnDistance;
            GameObject projectile = Instantiate(projectiles, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(shootDirection * shootForce);
            }

            if (ShootSFX != null && audioSource != null)
            {
                audioSource.PlayOneShot(ShootSFX);
            }

        }
    }
}


