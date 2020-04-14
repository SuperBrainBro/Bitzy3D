using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypePlayerScriptJump : MonoBehaviour
{

	//Private Variables

	private bool jump = false;
	private bool crouch = false;
	private Rigidbody2D rb;

	//Public Variables
	public PrototypePlayerScript controller;
	public Animator anim;

	public bool canMove = true;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
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
