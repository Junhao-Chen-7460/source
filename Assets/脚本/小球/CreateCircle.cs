using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCircle : MonoBehaviour
{
    public GameObject ballPrefab_w;           //声明一个白色小球预制体
    public GameObject ballPrefab_b;           //声明一个黑色小球预制体

    public float randomPoint1 = 0.0f;   //存储 x随机值
    public float randomPoint2 = 0.0f;   //存储 x随机值



    public AudioSource BGMSource; //BGM1

    void Start()
    {
        Application.targetFrameRate = 60;//设置游戏帧率为60
        BGMSource.Play();


        InvokeRepeating("fallBall_W", 2f, 4f);                              //设定白色小球自动掉落时间
        InvokeRepeating("fallBall_B", 4f, 4f);                              //设定黑色小球自动掉落时间
    }

    // Update is called once per frame
    void Update()
    {
 

    }

    private void fallBall_W()      //功能：生成小球
    {
        randomPoint1 = Random.Range(-10, 10);

        Vector3 pos1 = new Vector3(randomPoint1, 5.0f, 0);
        Instantiate(ballPrefab_w, pos1, transform.rotation);



    }


    private void fallBall_B()      //功能：生成小球
    {
        randomPoint2 = Random.Range(-10, 10);

        Vector3 pos2 = new Vector3(randomPoint2, 5.0f, 0);
        Instantiate(ballPrefab_b, pos2, transform.rotation);

    }



}
