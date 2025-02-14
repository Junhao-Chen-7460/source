using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float runSpeed = 500.0f;
    public float jumoForce = 500.0f;
    public int jumoNumber = 0;
    


    [SerializeField]  private Rigidbody2D myRigidbody;     //������������������ڻ�ȡ��Ҹ��� ��[SerializeField]˽�б����ɹ۲�
    [SerializeField]  private Animator myAnim;           //���������������������ڻ�ȡ��Ҷ���������
    public Collider2D myCollider2D;    //����2D��ײ��������ڻ�ȡ��ҵ���ײ��
    public LayerMask ground;        //����ͼ��������ڻ�ȡ�ذ�

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();     //�ڴ�����ֱ�ӻ�ȡ��ɫ�ĸ��壬Ҳ����ͨ����ק��ʽ��ȡ
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()    //���ݵ���ʵ�������ٶȣ�ƽ�����������ٶ�
    {
        Movement();
        SwitchAnim();
    }

    //�����������������������������������ƽ�ɫ�ܶ���Ծ����-��������������������������������������������������������������������
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
            if(jumoNumber == 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumoForce * Time.deltaTime);  //��x��y����ٶȸ����ɫ������ٶ���
                myAnim.SetBool("Jumping", true);

                jumoNumber++;
            }
                
        }
    }

    void SwitchAnim()
    {
        myAnim.SetBool("idle", false);

        if (myAnim.GetBool("Jumping"))
        {
            if(myRigidbody.velocity.y <= 0)    //ͨ������Y����ٶ��жϽ�ɫ�Ƿ�������״̬
            {
                myAnim.SetBool("Jumping", false);
                myAnim.SetBool("Falling", true);
            }
            
        }
        else if (myCollider2D.IsTouchingLayers(ground))  //�����ɫ����ײ������Ŀ��ͼ��������ж���
        {
            myAnim.SetBool("Falling", false);
            myAnim.SetBool("idle", true);

            jumoNumber = 0;
        }



    }



}
