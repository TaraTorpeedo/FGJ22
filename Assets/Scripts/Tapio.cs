using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tapio : MonoBehaviour
{
    public Animator anim;
    Rigidbody rb;

    [SerializeField] GameObject Player;

    NavMeshAgent agent;

    public bool isWaiting = false;

    [SerializeField] GameObject TapiosNest;


    [SerializeField] GameManager gameManager;

    public GameObject body;
    public GameObject horns;
    public GameObject Poncho;
    public GameObject Lantern;

    AudioSource audio;
    [SerializeField] AudioClip walkSound;


    bool isHiding = false;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        audio = GetComponent<AudioSource>();
        audio.clip = walkSound;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    void Move()
    {
        if (Player.GetComponent<Player>().outOfSafezone)
        {
            agent.SetDestination(Player.transform.position);
            anim.SetBool("isWalking", true);

            if (!audio.isPlaying)
                audio.Play();
        }
        else
        {
            agent.ResetPath();
            anim.SetBool("isWalking", false);

            audio.Stop();
        }
    }

    public void ScareTheChild(Transform playerTransfrom)
    {
        transform.localScale = new Vector3(1,1,1);
        body.GetComponent<shadertest>().ShowUp();
        transform.position = playerTransfrom.position + playerTransfrom.forward * 20;

        Poncho.SetActive(true);
        Lantern.SetActive(true);
    }
    public IEnumerator Hide()
    {
        body.GetComponent<shadertest>().Hide();

        yield return new WaitForSeconds(1f);

        Poncho.SetActive(false);
        Lantern.SetActive(false);

    }


}
