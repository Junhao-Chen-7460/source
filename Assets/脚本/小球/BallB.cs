using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallB : MonoBehaviour
{
    public AudioSource TheSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkLocation();
    }

    private void checkLocation()      //���ܣ����������û�е�����Ļ��
    {
        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);// ��ȡĿ�����ǰ����������ϵλ�ã�������ת��Ϊ��Ļ����ϵ�ĵ㣬�������飨���������sp��

        if (sp.y < -Screen.height)                                       //��ת�������Ļ���������Ļ����͵�
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Monster"))       //ע����ײʱ������������
        {

            Destroy(collision.gameObject);                             //�����ײĿ�����
            TheSource.PlayOneShot(TheSource.clip); //������Ч

            target.target1Number = target.target1Number - 1;

        }
    }
}
