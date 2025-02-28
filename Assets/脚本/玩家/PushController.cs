using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushController : MonoBehaviour
{


    public LayerMask bushlayer;//������ײ����Ĳ�
    public float bashRadius;//���ɵ���ķ�Χ�뾶
    public float bashForce;//��ҵ�����
    public float ballForce;//������
    public float bushTime;//������ײ��ʱ��ͣʱ��
    public Transform bashArrow;//�����ָ���ͷ

    private Rigidbody2D rb;
    private Vector2 bashDirection;//��ɫ���ڿɵ���ʱ�ĵ��䷽��
    private bool canBash;//�����ж��Ƿ���Ե���
    private Collider2D[] bashPoint;//�洢�ɵ�������Ϣ

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

    void OnDrawGizmos()     //���Կ��ӻ����Ѽ��Բ������
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bashRadius);
    }

    /// <summary>
    /// �ƶ���ɫ
    /// </summary>
    private void Movement()
    {

    }


    /// <summary>
    /// �°��յ���
    /// </summary>
    private void Bash()
    {
        //���ֵ�����������Ҽ���ͣ
        if (Input.GetMouseButtonDown(0))
        {
            bashPoint = Physics2D.OverlapCircleAll(transform.position, bashRadius, bushlayer);//�����ײ������
            if (bashPoint.Length != 0)
            {
                anim.SetTrigger("Attack");

                bashArrow.gameObject.SetActive(true);//��ʾ��ͷ
                bashArrow.position = bashPoint[0].transform.position;//�ı��ͷλ��Ϊ��ǰ�����
                StartCoroutine("BushTimeWaiting");//��ʼ��ʱ����ʱ��
                Time.timeScale = 0;//��ͣ
                canBash = true;
            }
        }
        //����Ҽ������������
        else if (Input.GetMouseButtonUp(0) && canBash)
        {
            bashArrow.gameObject.SetActive(false);
            Time.timeScale = 1;//�ָ�����
            bashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - bashPoint[0].transform.position;//�õ����䷽��
            bashDirection = bashDirection.normalized;
            transform.position = new Vector3(bashPoint[0].transform.position.x , bashPoint[0].transform.position.y , transform.position.z);//������ɫ��λ��
            rb.velocity = Vector2.zero;  //Vector2.zero == Vector2(0, 0),�൱�ٶ�����
            //�޸�x,��y����ĵ���ϵ����С
            bashDirection.y = bashDirection.y * 1f;
            bashDirection.x = bashDirection.x * 1f;
            rb.AddForce(bashDirection * bashForce, ForceMode2D.Impulse);//�����ɫ��


            foreach (Collider2D _bashPoint in bashPoint)
            {
                Transform _bashArrow = _bashPoint.GetComponent<Transform>();
                _bashArrow.position = new Vector3(bashPoint[0].transform.position.x + bashDirection.x, bashPoint[0].transform.position.y + bashDirection.y, transform.position.z);//������ɫ��λ��

                Rigidbody2D _rb = _bashPoint.GetComponent<Rigidbody2D>();
                _rb.AddForce(bashDirection * ballForce, ForceMode2D.Impulse);//����Ŀ��

                TheSource.PlayOneShot(TheSource.clip); //������Ч
            }





            StartCoroutine("SetBashTime");//��ʱ����ʱ��
        }
        //δ����ʱ����Ҽ�����ʱѡ���ͷ����
        else if (Input.GetMouseButton(0) && canBash)
        {
            Vector2 distance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - bashPoint[0].transform.position;
            distance.Normalize();
            float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;//�õ�����뵯����������ĽǶ�
            bashArrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    /// <summary>
    /// ʹ��Э�̴�����������ͣ��ʱ��
    /// </summary>
    /// <returns>The time waiting.</returns>
    private IEnumerator BushTimeWaiting()
    {
        float waitTime = Time.realtimeSinceStartup + bushTime;
        while (Time.realtimeSinceStartup < waitTime)
        {
            yield return null;   //��ʾ�ݻ�һ֡������һ֡�������´���
        }
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            canBash = false;
            bashArrow.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ��������в�����ˮƽ�������ƶ�
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
