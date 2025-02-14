using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float runSpeed = 500.0f;
    public float jumoForce = 500.0f;
    public int jumoNumber = 0;
    


    [SerializeField]  private Rigidbody2D myRigidbody;     //声明刚体组件对象，用于获取玩家刚体 ，[SerializeField]私有变量可观察
    [SerializeField]  private Animator myAnim;           //声明动画控制器对象，用于获取玩家动画控制器
    public Collider2D myCollider2D;    //声明2D碰撞体对象，用于获取玩家的碰撞体
    public LayerMask ground;        //声明图层对象，用于获取地板

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();     //在代码中直接获取角色的刚体，也可以通过拖拽方式获取
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()    //根据电脑实际运行速度，平滑更新运行速度
    {
        Movement();
        SwitchAnim();
    }

    //————————————————控制角色跑动跳跃功能-——————————————————————————————————
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
            if(jumoNumber == 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumoForce * Time.deltaTime);  //将x，y轴的速度赋予角色刚体的速度上
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
            if(myRigidbody.velocity.y <= 0)    //通过刚体Y轴的速度判断角色是否在下落状态
            {
                myAnim.SetBool("Jumping", false);
                myAnim.SetBool("Falling", true);
            }
            
        }
        else if (myCollider2D.IsTouchingLayers(ground))  //假如角色的碰撞体碰到目标图层里的所有对象
        {
            myAnim.SetBool("Falling", false);
            myAnim.SetBool("idle", true);

            jumoNumber = 0;
        }



    }



}
