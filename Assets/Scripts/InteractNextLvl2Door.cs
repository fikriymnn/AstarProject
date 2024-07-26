using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractNextLvl2Door : MonoBehaviour
{
    public GameObject hideText;
    public bool interactable;
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (gm.nxtLevel == true)
        {
            if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
            {
                hideText.SetActive(true);
                interactable = true;
            }
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if(gm.nxtLevel == true)
        {
            if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
            {
                hideText.SetActive(false);
                interactable = false;
            }
        }
        
    }

    public void level2()
    {
        SceneManager.LoadScene("MissionSuccess");
    }

    public void level3()
    {
        SceneManager.LoadScene("MissionSuccess2");
    }
}
