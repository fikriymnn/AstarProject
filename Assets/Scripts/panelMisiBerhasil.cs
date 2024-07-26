using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class panelMisiBerhasil : MonoBehaviour
{
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void level2()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void level3()
    {
        SceneManager.LoadScene("Level_3");
    }
}
