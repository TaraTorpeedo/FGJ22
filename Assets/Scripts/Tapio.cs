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

    public Renderer body;
    public GameObject horns;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();


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
        }
        else
        {
            agent.ResetPath();
            anim.SetBool("isWalking", false);

        }
    }

    public IEnumerator Hide()
    {
        //body.material.shader = Shader.Find("Dissolve");
        //body.material.SetFloat("_Dissolve", 3);
        yield return new WaitForSeconds(1);
        
        if (!Player.GetComponent<Player>().outOfSafezone)
        {
            gameObject.SetActive(false);
        }
    }
    public void ScareTheChild(Transform playerTransfrom)
    {
        transform.position = playerTransfrom.position + playerTransfrom.forward * 30;
    }
}
