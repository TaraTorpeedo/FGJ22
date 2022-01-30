using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject CreditsPanel;
    public Animator playerAnimator;
    public Player player;

    public GameObject DeadPanel;

    private void Start()
    {

    }

    private void Update()
    {
        float distance = Vector3.Distance(player.gameObject.transform.position, player.newTapio.gameObject.transform.position);
        if (distance < 1)
        {
            //Game Over


        }
    }

    public void StartGame()
    {
        GameObject.Find("Timelines").GetComponent<End>().StartTimeline_Start();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerAnimator.SetBool("GameStarted", true);
        MainMenuPanel.SetActive(false);
        StartCoroutine(player.PlayerIsStand(2.7f));

        DeadPanel.SetActive(true);
    }

    public void ShowCredits()
    {
        MainMenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }
    public void HideCredits()
    {
        MainMenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }
}
