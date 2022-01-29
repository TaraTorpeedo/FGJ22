using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tapio : MonoBehaviour
{
    public NavMeshAgent agent;
    Rigidbody rb;

    [SerializeField] GameObject Player;
    [SerializeField] GameObject TestDest;
    public bool DebugMode;
    public float distanceToApproach;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (DebugMode)
        {
            agent.SetDestination(TestDest.transform.position);
            anim.SetBool("isWalking", true);

        }
        else
        {
            float distance = Vector3.Distance(transform.position, Player.transform.position);


            if (distance < distanceToApproach - agent.stoppingDistance)
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
    }
}
