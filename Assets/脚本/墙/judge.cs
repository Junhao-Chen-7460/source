using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judge : MonoBehaviour
{
    public GameObject P_Mune;           //Ԥ������h��������
    public AudioSource TheSource;
    public AudioSource TheSource2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {

            Destroy(collision.gameObject);                             //�����ײĿ�����
            Time.timeScale = 0.0f;
            P_Mune.SetActive(true);                                    //���������

        }
        if (collision.tag.Equals("Monster"))          //ע����ײʱ������������
        {

            Destroy(collision.gameObject);                             //�����ײĿ�����
            TheSource.PlayOneShot(TheSource.clip); //������Ч

            target.target1Number = target.target1Number - 1;

        }
        if (collision.tag.Equals("WBall"))
        {

            Destroy(collision.gameObject);                             //�����ײĿ�����
            TheSource2.PlayOneShot(TheSource2.clip); //������Ч


        }
        if (collision.tag.Equals("BBall"))
        {

            Destroy(collision.gameObject);                             //�����ײĿ�����
            TheSource2.PlayOneShot(TheSource2.clip); //������Ч

        }
    }
}
