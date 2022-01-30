using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    [SerializeField] GameObject Player;

    public AudioClip[] Sounds;
    AudioSource audio;

    public GameObject panel;
    bool showPanel = false;

    GameObject theTree;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceX = Mathf.Abs(transform.position.x - Player.transform.position.x);
        float distanceZ = Mathf.Abs(transform.position.z - Player.transform.position.z);
        if(distanceX < 1f && distanceZ < 1f)
        {

            if (Player.GetComponent<Player>().ableToListen)
                panel.SetActive(true);
            else
                panel.SetActive(false);


            if (Input.GetKeyDown(KeyCode.E) && Player.GetComponent<Player>().ableToListen)
            {
                Player.GetComponent<Player>().ListenTree(gameObject);

                if (Sounds.Length > 0)
                {
                    int rnd = Random.Range(0, Sounds.Length);
                    AudioClip clip = Sounds[rnd];

                    audio.clip = clip;
                    audio.Play();
                }
            }

        }

    }

}
