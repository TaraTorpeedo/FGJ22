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
        transform.position = playerTransfrom.position + playerTransfrom.forward * 10;
        Instantiate(particleSystem, transform.position, transform.rotation);
        particleSystem.Play();

    }
    public IEnumerator Hide()
    {

        Instantiate(particleSystem, transform.position, transform.rotation);
        particleSystem.Play();
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);


    }


}
