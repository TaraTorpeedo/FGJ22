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
    public Tapio tapio;

    public GameObject DeadPanel;

    private void Start()
    {
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.gameObject.transform.position, tapio.gameObject.transform.position);
        if(distance < 5)
        {
            float alpha = (distance * -1 + 5);
            DeadPanel.GetComponent<Image>().color = new Color(0,0,0, alpha);

            if(distance < 1)
            {
                //Game Over

            }
        }
        else
            DeadPanel.GetComponent<Image>().color = new Color(0,0,0, 0);

    }

    public void StartGame()
    {
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
