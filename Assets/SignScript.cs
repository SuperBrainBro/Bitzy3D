using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SignScript : MonoBehaviour
{
    public string signText;

    public GameObject textBox;
    public Text textText;
    private bool inRange;

    private GameObject player;
    private float playerSpeed;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerSpeed = player.GetComponent<PrototypePlayerScript>().speed;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && inRange)
        {
            if (textBox.activeInHierarchy)
            {
                textBox.SetActive(false);

                player.GetComponent<PrototypePlayerScriptJump>().canMove = true;
                player.GetComponent<PrototypePlayerScript>().speed = playerSpeed;
            } else
            {
                textText.text = signText;
                textBox.SetActive(true);

                player.GetComponent<PrototypePlayerScriptJump>().canMove = false;
                player.GetComponent<PrototypePlayerScript>().speed = 0;
                player.GetComponent<PrototypePlayerScript>().freezePlayer();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player In Range Of Sign");
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Left Range Of Sign");
            inRange = false;
        }
    }
}