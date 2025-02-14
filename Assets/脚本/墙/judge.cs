using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judge : MonoBehaviour
{
    public GameObject P_Mune;           //预先声明h画布对象
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

            Destroy(collision.gameObject);                             //清除碰撞目标对象
            Time.timeScale = 0.0f;
            P_Mune.SetActive(true);                                    //激活画布对象

        }
        if (collision.tag.Equals("Monster"))          //注意碰撞时会调用两次这个
        {

            Destroy(collision.gameObject);                             //清除碰撞目标对象
            TheSource.PlayOneShot(TheSource.clip); //播放音效

            target.target1Number = target.target1Number - 1;

        }
        if (collision.tag.Equals("WBall"))
        {

            Destroy(collision.gameObject);                             //清除碰撞目标对象
            TheSource2.PlayOneShot(TheSource2.clip); //播放音效


        }
        if (collision.tag.Equals("BBall"))
        {

            Destroy(collision.gameObject);                             //清除碰撞目标对象
            TheSource2.PlayOneShot(TheSource2.clip); //播放音效

        }
    }
}
