using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public static float target1Number = 4;

    public  float Number;

    public GameObject P_Mune;           //Ԥ������h��������
    public float time_count = 0.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        check_target1Number();
        Number = target1Number;
        //Debug.Log(Number);

        time_count = Time.deltaTime + time_count;

        if (time_count >= 60.0f)
        {
            Time.timeScale = 0.0f;
            P_Mune.SetActive(true);                             //���������
        }

    }

    private void check_target1Number()   //���ܣ����Ѫ��������������
    {
        if (target1Number <= 0.0f)
        {

            Time.timeScale = 0.0f;
            P_Mune.SetActive(true);                             //���������
        }

    }
}
