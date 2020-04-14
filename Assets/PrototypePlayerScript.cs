using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypePlayerScript : MonoBehaviour
{
    //Private Variables
    private Vector3 moveInput;
    private Vector3 moveVelocityTo;
    private Rigidbody prRB;
    private float jumpVelocity;

    private float jumpForce = 75f;
    private float groundedVal;

    private float maxHieght;
    private bool wasCrouching = false;

    private float speedReal;
    //Public Variables
    public float speed;
    public float gravity;

    public LayerMask whatIsGround;
    public Transform groundCheck;
    public Transform ceilingCheck1;
    public Transform ceilingCheck2;
    public Collider crouchDisableCollider;
    public Animator anim;
    public Transform landPosition;
    public GameObject landParticle;

    //Contant Variables
    const float groundedRadius = .1f;
    const float ceilingCheckDistance = 10f;

    [HideInInspector] public bool grounded;
    void Start()
    {
        prRB = GetComponent<Rigidbody>();   
    }
    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        groundedVal -= 0.25f;

        if (groundedVal < 0)
        {
            jumpVelocity -= Time.deltaTime * gravity;
            grounded = false;
        }
        else
        {
            grounded = true;
        }

            Collider[] colliders = Physics.OverlapSphere(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                if (!wasGrounded)
                {
                    Instantiate(landParticle, landPosition.position, Quaternion.identity);
                    wasGrounded = true;
                }
                groundedVal = 2;
                jumpForce = 50;
                maxHieght = 50;

            }
        }
    }
    public void Move(bool crouch, bool jump)
    {
        if (!crouch)
        {
            //Debug.DrawRay(ceilingCheck1.transform.position, Vector3.up, Color.white, ceilingCheckDistance);
            //Debug.DrawRay(ceilingCheck2.transform.position, Vector3.up, Color.white, ceilingCheckDistance);

            if (Physics.Raycast(ceilingCheck1.transform.position, Vector3.up, ceilingCheckDistance, whatIsGround))
            {
                crouch = true;
            }
            else if (Physics.Raycast(ceilingCheck2.transform.position, Vector3.up, ceilingCheckDistance, whatIsGround))
            {
                crouch = true;
            }
        }
        if (crouch)
        {
            anim.SetBool("Crouch", true);
            speedReal = speed / 2;
            if (!wasCrouching)
            {
                wasCrouching = true;
            }

            if (crouchDisableCollider != null)
            {
                crouchDisableCollider.enabled = false;
            }
        }
        else
        {
            anim.SetBool("Crouch", false);
            speedReal = speed;
            if (crouchDisableCollider != null)
            {
                crouchDisableCollider.enabled = true;
            }
            if (wasCrouching)
            {
                wasCrouching = false;
            }
        }

        if (grounded && jump)
        {
            groundedVal = 0;
            jumpVelocity = jumpForce;
        }

        moveVelocityTo = new Vector3(moveInput.normalized.x, 0, moveInput.normalized.z);

        prRB.velocity = new Vector3((moveVelocityTo.x * (speedReal * 100) * Time.deltaTime), Mathf.Clamp(jumpVelocity, -gravity * 2, maxHieght), (moveVelocityTo.z * (speedReal * 100) * Time.deltaTime));
    }

    public void freezePlayer()
    {
        Move(false, false);
    }
}
