using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject showPanelLevel;

    public void goToLevel1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void pilihanLevel()
    {
        showPanelLevel.SetActive(true);
    }
}
