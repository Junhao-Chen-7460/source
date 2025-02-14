using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCircle : MonoBehaviour
{
    public GameObject ballPrefab_w;           //����һ����ɫС��Ԥ����
    public GameObject ballPrefab_b;           //����һ����ɫС��Ԥ����

    public float randomPoint1 = 0.0f;   //�洢 x���ֵ
    public float randomPoint2 = 0.0f;   //�洢 x���ֵ



    public AudioSource BGMSource; //BGM1

    void Start()
    {
        Application.targetFrameRate = 60;//������Ϸ֡��Ϊ60
        BGMSource.Play();


        InvokeRepeating("fallBall_W", 2f, 4f);                              //�趨��ɫС���Զ�����ʱ��
        InvokeRepeating("fallBall_B", 4f, 4f);                              //�趨��ɫС���Զ�����ʱ��
    }

    // Update is called once per frame
    void Update()
    {
 

    }

    private void fallBall_W()      //���ܣ�����С��
    {
        randomPoint1 = Random.Range(-10, 10);

        Vector3 pos1 = new Vector3(randomPoint1, 5.0f, 0);
        Instantiate(ballPrefab_w, pos1, transform.rotation);



    }


    private void fallBall_B()      //���ܣ�����С��
    {
        randomPoint2 = Random.Range(-10, 10);

        Vector3 pos2 = new Vector3(randomPoint2, 5.0f, 0);
        Instantiate(ballPrefab_b, pos2, transform.rotation);

    }



}
