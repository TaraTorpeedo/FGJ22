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


    [SerializeField] GameObject gameManager;

    AudioSource audio;
    [SerializeField] AudioClip walkSound;

    [SerializeField] ParticleSystem particleSystem;


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

        Player = GameObject.Find("Tyllero").gameObject;
        gameManager = GameObject.Find("GameManager");

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
        
        transform.position = playerTransfrom.position + playerTransfrom.forward;

        //Instantiate(particleSystem, transform.position, transform.rotation);
        //particleSystem.Play();

    }
    public IEnumerator Hide()
    {

        //Instantiate(particleSystem, transform.position, transform.rotation);
        //particleSystem.Play();

        yield return new WaitForSeconds(1f);

    }


}
