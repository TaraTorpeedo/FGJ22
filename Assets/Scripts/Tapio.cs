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

    AudioSource audio;
    [SerializeField] AudioClip walkSound;

    [SerializeField] ParticleSystem particleSystem;


    ParticleSystem ps = null;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        audio = GetComponent<AudioSource>();
        audio.clip = walkSound;

        Player = GameObject.Find("Tyllero").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (ps!=null){
            ps.transform.position = transform.position;
        }
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
        
        transform.position = playerTransfrom.position + playerTransfrom.forward * 20;

        ps = Instantiate(particleSystem, transform.position + transform.forward, transform.rotation);
        ps.transform.position = transform.position;
        ps.Play();


    }
    public void Hide()
    {
        Instantiate(particleSystem, transform.position, transform.rotation);
        particleSystem.Play();

    }


}
