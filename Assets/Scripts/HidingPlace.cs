using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HidingPlace : MonoBehaviour
{
    public GameObject hideText, stopHideText;
    public GameObject normalPlayer, hidingPlayer;
    public EnemyAI monsterScript1, monsterScript2;
    public Transform monsterTransform1, monsterTransform2;
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
            float distance = Vector3.Distance(monsterTransform1.position, normalPlayer.transform.position);
            float distance2 = Vector3.Distance(monsterTransform2.position, normalPlayer.transform.position);
            if (distance > loseDistance)
            {
                if (monsterScript1.chasing == true)
                {
                    monsterScript1.stopChase();
                }
            }

            if (distance2 > loseDistance)
            {
                if (monsterScript2.chasing == true)
                {
                    monsterScript2.stopChase();
                }
            }
        }

        if(hiding == true)
        {
            stopHideText.SetActive(true);
            hideText.SetActive(false);
            
        }
    }

    public void Hide()
    {
        if (interactable == true)
        {
            hideText.SetActive(false);
            hidingPlayer.SetActive(true);
            float distance = Vector3.Distance(monsterTransform1.position, normalPlayer.transform.position);
            float distance2 = Vector3.Distance(monsterTransform2.position, normalPlayer.transform.position);
            if (distance > loseDistance)
            {
                if (monsterScript1.chasing == true)
                {
                    monsterScript1.stopChase();
                }
            }
            if (distance2 > loseDistance)
            {
                if (monsterScript2.chasing == true)
                {
                    monsterScript2.stopChase();
                }
            }
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
