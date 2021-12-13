using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameOverScreen : MonoBehaviour
{

    public GameObject gameOverCanvas;
    public Text gameOverText;

    public void EndGame(string name)
    {
        gameOverCanvas.SetActive(true);
        gameOverText.text = name + " Loses!";
        Debug.Log(name);
    }
}