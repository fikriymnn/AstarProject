using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI keyCollected;
    public int totalKey = 0;
    public GameObject lightDoor, particleDoor;
    public bool nxtLevel;


    // Start is called before the first frame update
    void Start()
    {
        keyCollected.text = "0";
        totalKey = 0;
        lightDoor.SetActive(false);
        particleDoor.SetActive(false);
        nxtLevel = false;
    }

    // Update is called once per frame
    void Update()
    {
        keyCollected.text = totalKey.ToString();
        if (keyCollected.text == "2")
        {
            lightDoor.SetActive(true);
            particleDoor.SetActive(true);
            nxtLevel = true;
        }
    }
}
