using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPortal : MonoBehaviour {

    public static bool gameIsWon = false;
    [SerializeField] GameObject victoryUI;

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.tag == "Player") {

            victoryUI.SetActive(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}