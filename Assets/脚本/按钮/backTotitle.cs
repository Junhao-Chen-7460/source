using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class backTotitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnClick()
    {
        Time.timeScale = 1;
        target.target1Number = 4.0f;
        SceneManager.LoadScene("New Scene");
    }
}
