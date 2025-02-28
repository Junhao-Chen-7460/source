using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushController : MonoBehaviour
{


    public LayerMask bushlayer;//可以碰撞弹射的层
    public float bashRadius;//检测可弹射的范围半径
    public float bashForce;//玩家弹射力
    public float ballForce;//球弹射力
    public float bushTime;//遇到碰撞点时暂停时间
    public Transform bashArrow;//弹射点指向箭头

    private Rigidbody2D rb;
    private Vector2 bashDirection;//角色处于可弹射时的弹射方向
    private bool canBash;//用来判断是否可以弹射
    private Collider2D[] bashPoint;//存储可弹射点的信息

    private Animator anim;

    public AudioSource TheSource; 

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Bash();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canBash)
            Movement();
    }

    void OnDrawGizmos()     //调试可视化，把检测圆画出来
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bashRadius);
    }

    /// <summary>
    /// 移动角色
    /// </summary>
    private void Movement()
    {

    }


    /// <summary>
    /// 仿奥日弹射
    /// </summary>
    private void Bash()
    {
        //发现弹射点后按下鼠标右键暂停
        if (Input.GetMouseButtonDown(0))
        {
            bashPoint = Physics2D.OverlapCircleAll(transform.position, bashRadius, bushlayer);//获得碰撞点数组
            if (bashPoint.Length != 0)
            {
                anim.SetTrigger("Attack");

                bashArrow.gameObject.SetActive(true);//显示箭头
                bashArrow.position = bashPoint[0].transform.position;//改变箭头位置为当前弹射点
                StartCoroutine("BushTimeWaiting");//开始计时空闲时间
                Time.timeScale = 0;//暂停
                canBash = true;
            }
        }
        //鼠标右键弹起后弹射物体
        else if (Input.GetMouseButtonUp(0) && canBash)
        {
            bashArrow.gameObject.SetActive(false);
            Time.timeScale = 1;//恢复正常
            bashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - bashPoint[0].transform.position;//得到弹射方向
            bashDirection = bashDirection.normalized;
            transform.position = new Vector3(bashPoint[0].transform.position.x , bashPoint[0].transform.position.y , transform.position.z);//修正角色的位置
            rb.velocity = Vector2.zero;  //Vector2.zero == Vector2(0, 0),相当速度清零
            //修改x,和y方向的弹力系数大小
            bashDirection.y = bashDirection.y * 1f;
            bashDirection.x = bashDirection.x * 1f;
            rb.AddForce(bashDirection * bashForce, ForceMode2D.Impulse);//弹射角色·


            foreach (Collider2D _bashPoint in bashPoint)
            {
                Transform _bashArrow = _bashPoint.GetComponent<Transform>();
                _bashArrow.position = new Vector3(bashPoint[0].transform.position.x + bashDirection.x, bashPoint[0].transform.position.y + bashDirection.y, transform.position.z);//修正角色的位置

                Rigidbody2D _rb = _bashPoint.GetComponent<Rigidbody2D>();
                _rb.AddForce(bashDirection * ballForce, ForceMode2D.Impulse);//弹射目标

                TheSource.PlayOneShot(TheSource.clip); //播放音效
            }





            StartCoroutine("SetBashTime");//计时弹射时间
        }
        //未弹射时鼠标右键按着时选择箭头跟随
        else if (Input.GetMouseButton(0) && canBash)
        {
            Vector2 distance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - bashPoint[0].transform.position;
            distance.Normalize();
            float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;//得到鼠标与弹射点间的向量的角度
            bashArrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    /// <summary>
    /// 使用协程处理跟弹射点暂停的时间
    /// </summary>
    /// <returns>The time waiting.</returns>
    private IEnumerator BushTimeWaiting()
    {
        float waitTime = Time.realtimeSinceStartup + bushTime;
        while (Time.realtimeSinceStartup < waitTime)
        {
            yield return null;   //表示暂缓一帧，在下一帧接着往下处理
        }
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            canBash = false;
            bashArrow.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 弹射过程中不能在水平方向有移动
    /// </summary>
    /// <returns>The bash time.</returns>
    private IEnumerator SetBashTime()
    {
        float waitTime = Time.realtimeSinceStartup + 1;
        while (Time.realtimeSinceStartup < waitTime)
        {
            yield return null;
        }
        canBash = false;
    }
}
