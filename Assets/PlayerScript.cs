using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	//Private Variables
	private bool jump = false;
	private bool crouch = false;
	private Rigidbody rb;
	private float dashTime;
	//Public Variables
	public PlayerController controller;
	public Animator anim;
	public bool canMove = true;
    public SpriteRenderer interactionText;
    
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		dashTime = Time.time;
	}
	void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			if (jump == false)
			{
				StartCoroutine(waitJump(.15f));
				jump = true;
			}
		}
		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}
		if (Input.GetButtonDown("Dash"))
		{
			if (Time.time > dashTime)
			{
				controller.Dash();
				dashTime = Time.time + .75f;
				Debug.Log("Dash");
			}
		}		
	}
	void FixedUpdate()
	{
		if (canMove)
		{
			controller.Move(crouch, jump);
		}
	}
	private IEnumerator waitJump(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		jump = false;
	}
}
