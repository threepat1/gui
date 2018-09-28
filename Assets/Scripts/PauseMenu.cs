using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject mainCam;
    public GameObject player;


    // Use this for initialization
    void Start () {
     
            Time.timeScale = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");


    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            } 
            else
            {
                Pause();
            }
        }
	}
    public void Resume ()
    {
        
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        player.GetComponent<CharacterMovement>().enabled = true;
        player.GetComponent<MouseLook>().enabled = true;
        mainCam.GetComponent<MouseLook>().enabled = true;
        GameIsPaused = false;
        

    }
    void Pause()
    {
        
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        player.GetComponent<CharacterMovement>().enabled = false;
        player.GetComponent<MouseLook>().enabled = false;
        mainCam.GetComponent<MouseLook>().enabled = false;
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
    public void LoadMenu ()
    {
        Time.timeScale= 1f;
        SceneManager.LoadScene(0);
    }
    public void Exitmenu()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
