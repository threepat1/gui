using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;//interacting with scene change
using UnityEngine.UI;//interacting with GUI elements
using UnityEngine.EventSystems;//control the event (button shiz)
[AddComponentMenu("Skyrim2.0/Menus/Main")]
public class MenuHandler : MonoBehaviour
{
    #region Variables
    [Header("OPTIONS")]
    public bool showOptions;
    public Vector2[] res = new Vector2[7];
    public int resIndex;
    public bool isFullScreen;
    
    [Header("References")]
    public AudioSource mainAudio;
    public Light dirLight;
    public Dropdown resDropdown;
    public GameObject mainMenu, optionsMenu;
    public Slider volSlider, brightSlider, ambientSlider;



    private void Awake()
    {
        volSlider.value = PlayerPrefs.GetFloat("Audio Source");
        brightSlider.value = PlayerPrefs.GetFloat("Directional Light");

    }
    #endregion
    void Start()
    {
        mainAudio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        dirLight = GameObject.Find("Directional Light").GetComponent<Light>();

    }
    public void LoadGame()
    {
        PlayerPrefs.SetFloat("Audio Source", mainAudio.volume);
        PlayerPrefs.SetFloat("Directional Light", dirLight.intensity);
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
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
        if (showOptions)//showOptions == true means showOptions is true
        {
            showOptions = false;//
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

            brightSlider = GameObject.Find("BrightSlider").GetComponent<Slider>();

            ambientSlider = GameObject.Find("AmSlider").GetComponent<Slider>();

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
        Screen.SetResolution((int)res[resIndex].x, (int)res[resIndex].y, isFullScreen);
    }
   
    public void Back()
    {
        OptionToggle();
    }

}