using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : MonoBehaviour
{
    public GameObject hideText, stopHideText;
    public GameObject normalPlayer, hidingPlayer;
    public EnemyAI monsterScript;
    public Transform monsterTransform;
    bool interactable, hiding;
    public float loseDistance;

    void Start()
    {
        interactable = false;
        hiding = false;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            hideText.SetActive(true);
            interactable = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            hideText.SetActive(false);
            interactable = false;
        }
    }
    void Update()
    {
        if (interactable == true)
        {
            hideText.SetActive(true);
            stopHideText.SetActive(false);
            
        }

        if(hiding == true)
        {
            stopHideText.SetActive(true);
            hideText.SetActive(false);
            float distance = Vector3.Distance(monsterTransform.position, normalPlayer.transform.position);
            if (distance > loseDistance)
            {
                if (monsterScript.chasing == true)
                {
                    monsterScript.stopChase();
                }
            }
        }
    }

    public void Hide()
    {
        if (interactable == true)
        {
            hideText.SetActive(false);
            hidingPlayer.SetActive(true);
            stopHideText.SetActive(true);
            hiding = true;
            normalPlayer.SetActive(false);
            interactable = false;
        }
    }

    public void ExitHide()
    {
        if (hiding == true)
        {
            stopHideText.SetActive(false);
            normalPlayer.SetActive(true);
            hidingPlayer.SetActive(false);
            hiding = false;
        }
    }
}
