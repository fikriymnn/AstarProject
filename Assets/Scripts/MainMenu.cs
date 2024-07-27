using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject showPanelLevel, showPanelPetunjuk, showPanelPlayerController, showPanelObjective;

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

    public void petunjuk()
    {
        showPanelPetunjuk.SetActive(true);
    }

    public void objective()
    {
        showPanelObjective.SetActive(true);
    }

    public void playerController()
    {
        showPanelPlayerController.SetActive(true);
    }

    public void closePanelLevel()
    {
        showPanelLevel.SetActive(false);
    }

    public void closePanelPetunjuk()
    {
        showPanelPetunjuk.SetActive(false);
    }

    public void closePanelPlayerController()
    {
        showPanelPlayerController.SetActive(false);
    }

    public void closePanelObjective()
    {
        showPanelObjective.SetActive(false);
    }

    public void quitApp()
    {
        Application.Quit();
    }
}
