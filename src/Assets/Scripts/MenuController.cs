using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void OnMouseDown()
    {
        if(gameObject.tag == "play_button")
        {
            SceneManager.LoadScene("How");
        } else if (gameObject.tag == "credits_button")
        {
            SceneManager.LoadScene("Credits");
        }
        else if (gameObject.tag == "exit_button") {
            Application.Quit();
        }
        else if(gameObject.tag == "menu_button")
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadSceneAsync("SampleScene");
        }
    }
}
