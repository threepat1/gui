using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool paused ;
    public bool showOption;
    public GameObject pauseMenu, optionMenu;

    public GameObject mainCam;
    public GameObject player;


    // Use this for initialization
    void Start () {
     
        Time.timeScale = 1f;
        paused = false;
        pauseMenu.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");


    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
	}
    public void ToggleOption()
    {
        if (showOption)
        {
            showOption = false;
           
        }
        else
        {
            showOption = true;
            pauseMenu.SetActive(false);
            player.GetComponent<CharacterMovement>().enabled = true;
            player.GetComponent<MouseLook>().enabled = true;
            mainCam.GetComponent<MouseLook>().enabled = true;

        }
        optionMenu.SetActive(showOption);
    }
    public void Resume ()
    {
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        player.GetComponent<CharacterMovement>().enabled = true;
        player.GetComponent<MouseLook>().enabled = true;
        mainCam.GetComponent<MouseLook>().enabled = true;
        Debug.Log("Resume");
   

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
    public void TogglePause()
    {
        if(paused && !showOption && !Inventory.showInv)
        {
            Time.timeScale = 1;
            paused = false;
            pauseMenu.SetActive(false);
            player.GetComponent<CharacterMovement>().enabled = true;
            player.GetComponent<MouseLook>().enabled = true;
            mainCam.GetComponent<MouseLook>().enabled = true;

        }
        else if (paused && showOption)
        {
            ToggleOption();
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.GetComponent<CharacterMovement>().enabled = false;
            player.GetComponent<MouseLook>().enabled = false;
            mainCam.GetComponent<MouseLook>().enabled = false;
        }
        else if (paused && !showOption && Inventory.showInv)
        {
            paused = false;
            pauseMenu.SetActive(false);


        }
        else
        {
            Time.timeScale = 0;
            paused = true;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.GetComponent<CharacterMovement>().enabled = false;
            player.GetComponent<MouseLook>().enabled = false;
            mainCam.GetComponent<MouseLook>().enabled = false;

        }
    }
}
