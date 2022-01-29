using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    [SerializeField] GameObject Player;

    public AudioClip[] Sounds;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if(distance < 1f)
        {
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
