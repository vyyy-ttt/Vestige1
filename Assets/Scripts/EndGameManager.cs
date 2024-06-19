using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    public Text endGameText;

    void Start()
    {
        // Hide the end game message at the start
        endGameText.gameObject.SetActive(false);
    }

    public void ShowEndGameMessage(string message)
    {
        endGameText.text = message;
        endGameText.gameObject.SetActive(true);
        StartCoroutine(PauseGame());
    }

    IEnumerator PauseGame()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0; 
    }
}