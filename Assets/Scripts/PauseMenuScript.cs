using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

    public static bool gameIsPaused = false;
    [SerializeField] GameObject pauseUI;

    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (gameIsPaused) {

                Resume();
            }
            else {
                Pause();
            }
        }
    }


    public void Resume() {

        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause() {

        pauseUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void LoadMenu() {

        SceneManager.LoadScene(0);
    }

    public void Quit() {
        Application.Quit();
    }
}
