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

    private void checkLocation()      //功能：检查自身有没有掉出屏幕外
    {
        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);// 获取目标对象当前的世界坐标系位置，并将其转换为屏幕坐标系的点，将坐标组（向量）存进sp里

        if (sp.y < -Screen.height)                                       //当转换后的屏幕坐标大于屏幕的最低点
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Monster"))       //注意碰撞时会调用两次这个
        {

            Destroy(collision.gameObject);                             //清除碰撞目标对象
            TheSource.PlayOneShot(TheSource.clip); //播放音效

            target.target1Number = target.target1Number - 1;

        }
    }
}
