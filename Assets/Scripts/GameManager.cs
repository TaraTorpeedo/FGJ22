using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject CreditsPanel;
    public Animator playerAnimator;
    public Player player;
    public Tapio tapio;

    public GameObject DeadPanel;

    void Awake()
    {

        DeadPanel.SetActive(false);
    }

    private void Start()
    {
        DeadPanel.SetActive(false);

    }

    private void Update()
    {
        float distance = Vector3.Distance(player.gameObject.transform.position, tapio.gameObject.transform.position);
        if (distance < 1)
        {
            //Game Over
            //DeadPanel.SetActive(true);
            GameObject.Find("Timelines").GetComponent<End>().StartTimeline_GameOver();
            player.GetComponent<Player>().ableToMove = false;
            StartCoroutine(Dead());
        }
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        GameObject.Find("Timelines").GetComponent<End>().StartTimeline_Start();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerAnimator.SetBool("GameStarted", true);
        MainMenuPanel.SetActive(false);
        StartCoroutine(player.PlayerIsStand(2.7f));

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
