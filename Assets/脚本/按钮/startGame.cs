using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }


    void Update()
    {

    }
    void OnClick()
    {

        SceneManager.LoadScene("SampleScene");
    }

}
