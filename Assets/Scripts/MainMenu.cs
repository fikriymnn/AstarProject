using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject showPanelLevel;

    public void goToLevel1()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void goToLevel2()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void goToLevel3()
    {
        SceneManager.LoadScene("Level_3");
    }

    public void pilihanLevel()
    {
        showPanelLevel.SetActive(true);
    }
}
