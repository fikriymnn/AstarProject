using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PickKey : MonoBehaviour
{
    public GameObject pickBtn, keyObject;
    public bool interactable;
    public TextMeshProUGUI keyCollected;
    public int key = 0;
    public GameManager gm;
    int num = 0;

    void Start()
    {
        interactable = false;
        keyCollected.text = "0";
        key = 0;
        keyObject.SetActive(true);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            pickBtn.SetActive(true);
            interactable = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            pickBtn.SetActive(false);
            interactable = false;
        }
    }

    void Update()
    {
        //keyCollected.text = key.ToString();
        
    }

    public void PickUpKey()
    {
        Debug.Log("picked");
        gm.totalKey += 1;
        keyObject.SetActive(false);
        interactable = false;
        pickBtn.SetActive(false);
    }
}

