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
    [SerializeField] private Rigidbody2D myRigidbody;     //������������������ڻ�ȡ��Ҹ��� ��[SerializeField]˽�б����ɹ۲�
    [SerializeField] private Animator myAnim;           //���������������������ڻ�ȡ��Ҷ���������
    public Collider2D myCollider2D;    //����2D��ײ��������ڻ�ȡ��ҵ���ײ��
    public LayerMask ground;        //����ͼ��������ڻ�ȡ�ذ�

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();     //�ڴ�����ֱ�ӻ�ȡ��ɫ�ĸ��壬Ҳ����ͨ����ק��ʽ��ȡ
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
        

        //�����������������������������������ƽ�ɫ�ܶ���Ծ����-��������������������������������������������������������������������




    }
    void Movement()
    {



        float Horizontal_move = Input.GetAxis("Horizontal");  //��ȡHorizontal�����¼�������ֵ����-1��1�ĸ���ֵ��������Ϊ0

        float moveDir = Input.GetAxisRaw("Horizontal");  //��ȡHorizontal�����¼�������ֵ��ֻ����-1��0��1����ֵ

        if (Horizontal_move != 0)
        {
            myRigidbody.velocity = new Vector2(Horizontal_move * runSpeed * Time.deltaTime, myRigidbody.velocity.y);  //��x��y����ٶȸ����ɫ������ٶ���
            myAnim.SetFloat("RunSpeed", Mathf.Abs(moveDir));         //ͨ�������¼����жϽ�ɫ���ƶ�״̬
        }

        if (moveDir != 0)
        {
            transform.localScale = new Vector3(moveDir, 1, 1);  //ͨ���ı����ŵ�����ֵ��ʵ��ͼƬ�ķ�ת
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpNumber < MaxJump)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce * Time.deltaTime);  //��x��y����ٶȸ����ɫ������ٶ���
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
            if (myRigidbody.velocity.y <= 0)    //ͨ������Y����ٶ��жϽ�ɫ�Ƿ�������״̬
            {
                myAnim.SetBool("Jumping", false);
                myAnim.SetBool("Falling", true);
            }

        }
        else if (myCollider2D.IsTouchingLayers(ground))  //�����ɫ����ײ������Ŀ��ͼ��������ж���
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


