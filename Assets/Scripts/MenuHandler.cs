using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;// interacting with scene change
using UnityEngine.UI;// interacting with GUI element
using UnityEngine.EventSystems; //control the event (button shiz)
public class MenuHandler : MonoBehaviour
{
    #region Variables
    public GameObject mainMenu, optionsMenu;
    public bool showOptions;
    public Slider volSlider;
    public Slider brightSlider;
    public Slider ambientSlider; // connect slider to slider game object
    public AudioSource mainAudio;
    public Light dirLight;
    public Vector2[] res = new Vector2[7];
    public int resIndex;
    public bool isFullScreen;
    public Dropdown resDropdown;
    public KeyCode Forward { get; set; }
    public KeyCode Backward { get; set; }
    public KeyCode Jump { get; set; }
    public KeyCode Down { get; set; }
    public KeyCode Left { get; set; }
    public KeyCode Right { get; set; }



    private void Awake()
    {
        Forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardkey", "w"));
        Backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardkey", "s"));
        Jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpkey", "Space"));
        Left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Leftkey", "a"));
        Right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Rightkey", "d"));
    }

    private void Start()
    {
        
        mainAudio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        dirLight = GameObject.Find("Directional Light").GetComponent<Light>();
        
    }
    public void Loadgame()
    {
        SceneManager.LoadScene("Walk");
    }
    public void Exitgame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    
    }

    public void ToggleOptions()
    {
        OptionToggle();
    }
    bool OptionToggle()
    {
        if(showOptions)//showOptions == true means showOptions is true
        {
            showOptions = false;
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            return false;
        }
        else
        {
            showOptions = true;
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            volSlider = GameObject.Find("AudioSlider").GetComponent<Slider>();
            brightSlider = GameObject.Find("Brightness").GetComponent<Slider>();
            resDropdown = GameObject.Find("Resolution").GetComponent<Dropdown>();
            volSlider.value = mainAudio.volume;
            brightSlider.value = dirLight.intensity;
            ambientSlider.value = RenderSettings.ambientIntensity;
            return true;
        }
                }

    public void Volume()
    {
        mainAudio.volume = volSlider.value;
    }
    public void Brightness()
    {
        dirLight.intensity = brightSlider.value;
    }
    public void Ambient()
    {
        RenderSettings.ambientIntensity = ambientSlider.value;
    }
    public void Resolution()
    {
        resIndex = resDropdown.value;
        Screen.SetResolution((int)res[resIndex].x,(int)res[resIndex].y,isFullScreen);
    }

}

#endregion