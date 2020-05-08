using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SignScript : MonoBehaviour
{
    //Public Variables
    public string signText;
    public GameObject textBox;
    public Text textText;
    //Private Variables
    private bool inRange;
    private GameObject player;
    private float playerSpeed;
    private PlayerController controller;
    private PlayerScript playerScript;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<PlayerController>();
        playerSpeed = controller.speed;
        playerScript = player.GetComponent<PlayerScript>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && inRange)
        {
            if (textBox.activeInHierarchy)
            {
                textBox.SetActive(false);
                playerScript.canMove = true;
                controller.speed = playerSpeed;
            } else
            {
                textText.text = signText;
                textBox.SetActive(true);
                playerScript.canMove = false;
                controller.speed = 0;
                controller.FreezePlayer();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            player.GetComponent<PlayerScript>().interactionText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            player.GetComponent<PlayerScript>().interactionText.enabled = false;
        }
    }
}