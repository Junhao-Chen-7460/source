using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public static float target1Number = 4;

    public  float Number;

    public GameObject P_Mune;           //预先声明h画布对象
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
            P_Mune.SetActive(true);                             //激活画布对象
        }

    }

    private void check_target1Number()   //功能：检查血量，并销毁自身
    {
        if (target1Number <= 0.0f)
        {

            Time.timeScale = 0.0f;
            P_Mune.SetActive(true);                             //激活画布对象
        }

    }
}
