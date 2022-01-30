using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Free moving variables
    public CharacterController controller;
    public float speed = 3f;
    public float runSpeed;
    public float turnSmoothTime = 0f;
    public Transform cam;
    float turnSmoothVelocity;

    bool ableToMove = false;
    public bool ableToListen = true;

    bool isRunning = false;

    [SerializeField] GameObject ThirdPersonCamera;

    Animator anim;

    Vector3 currentPos;
    Vector3 lastPos;

    float gravity = -9.81f;
    Vector3 velocity;

    public GameManager gameManager;

    public bool outOfSafezone = false;

    public GameObject Tapio;

    AudioSource audio;
    [SerializeField] AudioClip walkSound;
    [SerializeField] AudioClip RunSound;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        lastPos = transform.position;

        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

    }

    public IEnumerator PlayerIsStand(float gettingUpTime)
    {
        controller.enabled = false;
        yield return new WaitForSeconds(gettingUpTime);
        controller.enabled = true;
        ableToMove = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!ableToMove)
            return;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Move();
        Animations();
    }
    void Animations()
    {
        if (anim == null)
            return;

        currentPos = transform.position;
        if (currentPos != lastPos)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", isRunning);

            if(audio.clip != null && !audio.isPlaying)
                audio.Play();
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);

            if (audio.clip != null)
                audio.Stop();
        }
        lastPos = currentPos;

    }

    public void ListenTree(GameObject tree)
    {
        if (ableToMove)
        {

            ableToMove = false;
            ableToListen = false;

            StartCoroutine(StopListening());

            //Look at the tree
            var lookPos = tree.transform.position - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(lookPos);
            lookRot.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, lookRot.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 200);

            anim.SetBool("isListening", true);
        }

    }

    IEnumerator StopListening()
    {
        yield return new WaitForSeconds(3.5f);
        anim.SetBool("isListening", false);
        ableToMove = true;
        ableToListen = true;
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            audio.clip = RunSound;
        }
        else
        {
            isRunning = false;
            audio.clip = walkSound;
        }

        if (isRunning == true)
            runSpeed = speed * 2;
        else
            runSpeed = 1;
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * runSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Safezone")
        {
            outOfSafezone = true;
           // Tapio.gameObject.SetActive(true);
            Tapio.GetComponent<Tapio>().ScareTheChild(transform);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Safezone")
        {
            outOfSafezone = false;
            StartCoroutine(Tapio.GetComponent<Tapio>().Hide());
        }
    }
}