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



    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        lastPos = transform.position;

        anim = GetComponent<Animator>();

        StartCoroutine(PlayerIsStand(2.7f));
    }

    IEnumerator PlayerIsStand(float gettingUpTime)
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
            
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }
        lastPos = currentPos;
    }

    public void ListenTree(GameObject tree)
    {
        ableToMove = false;
        ableToListen = false;
        
        StartCoroutine(StopListening());

        //Look at the tree
        transform.LookAt(tree.transform);
        anim.SetBool("isListening", true);

    }

    IEnumerator StopListening()
    {
        yield return new WaitForSeconds(3.25f);
        anim.SetBool("isListening", false);
        ableToMove = true;
        ableToListen = true;
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            isRunning = true;    
        else
            isRunning = false;

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
            Tapio.gameObject.SetActive(true);
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