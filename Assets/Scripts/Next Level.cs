using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Door;
    public int nextSceneNum;
    private bool isPlayerNearDoor = false;

    void Start()
    {
        Door.SetActive(false);
    }

    void Update()
    {
        // Check if the boss is destroyed
        if (Boss == null)
        {
            Debug.Log("Boss is destroyed. Activating the door.");
            Door.SetActive(true);
        }

        // Check if the player presses 'W' and is near the door
        if (isPlayerNearDoor && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Player is near the door and pressed 'W'. Loading next level.");
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        // Load the next scene
        SceneManager.LoadSceneAsync(nextSceneNum);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the door trigger area.");
            isPlayerNearDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited the door trigger area.");
            isPlayerNearDoor = false;
        }
    }
}
