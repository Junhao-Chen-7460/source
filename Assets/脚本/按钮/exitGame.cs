using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;           //¼ÇµÃ¼Ó¿â

public class exitGame : MonoBehaviour
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
        Application.Quit();
    }
}
