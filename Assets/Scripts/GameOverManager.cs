using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("GameOverText is not assigned in the inspector!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("InDoor"))
        {
            Debug.Log("Trigger with Indoor object detected!");
            ShowGameOver();
        }
    }

    void ShowGameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            Debug.Log("Game Over text set to active.");
        }
        else
        {
            Debug.LogError("GameOverText is not assigned in the inspector!");
        }
    }
}
