using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 7.5f;
    private int currentPositionIndex = 1;
    public Vector3[] targetPositions;
    [SerializeField]
    bool isJumping = false;

    bool isGrounded = true;
    Rigidbody rb;
    [SerializeField]
    float jumpForce = 20f;
    [SerializeField]
    float smoothTime = 0.5f;
    Animator anim;

    private Vector3 velocity = Vector3.zero;

    public GameObject player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveToPosition(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveToPosition(1);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y < 1f && isGrounded)
        {
            isJumping = true;
        }

        // anim.SetBool("isGrounded", isGrounded);
        // anim.SetBool("isJump", isJumping);

        Vector3 targetPosition = new Vector3(targetPositions[currentPositionIndex].x, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        transform.Translate(transform.parent.forward * movementSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (isJumping && isGrounded)

        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJumping = false;
            isGrounded = false;
        }
    }

    void MoveToPosition(int direction)
    {
        currentPositionIndex += direction;
        currentPositionIndex = Mathf.Clamp(currentPositionIndex, 0, targetPositions.Length - 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "RampTop" || collision.gameObject.tag == "RampBot")
        {
            isGrounded = true;
        }
    }
}
