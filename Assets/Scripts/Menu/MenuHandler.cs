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
    [Header("Keys")]
    public KeyCode holdingKey;
    public KeyCode forward, backward, left, right, jump, crouch, sprint, interact;
    [Header("References")]
    public AudioSource mainAudio;
    public Light dirLight;
    public Dropdown resDropdown;
    public GameObject mainMenu, optionsMenu;
    public Slider volSlider, brightSlider, ambientSlider;


    [Header("KeyBind References")]
    public Text forwardText;
    public Text backwardText, leftText, rightText, jumpText, crouchText, sprintText, interactText;

    #endregion
    void Start()
    {
        mainAudio = GameObject.Find("MainMusic").GetComponent<AudioSource>();
        dirLight = GameObject.Find("Directional Light").GetComponent<Light>();
        #region Set Up Keys
        //set out keys to the preset keys we may have saved, else set the keys to default
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
        forwardText.text = forward.ToString();

        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl"));
        sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
        interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));
        #endregion
    }
    public void LoadGame()
    {
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
    public void Save()
    {
        PlayerPrefs.SetString("Forward", forward.ToString());
        PlayerPrefs.SetString("Backward", backward.ToString());
        PlayerPrefs.SetString("Left", left.ToString());
        PlayerPrefs.SetString("Right", right.ToString());
        PlayerPrefs.SetString("Jump", jump.ToString());
        PlayerPrefs.SetString("Crouch", crouch.ToString());
        PlayerPrefs.SetString("Sprint", sprint.ToString());
        PlayerPrefs.SetString("Interact", interact.ToString());
    }

    private void OnGUI()

    {
        
        Event e = Event.current;
        if (forward == KeyCode.None)
        {
            Debug.Log("KeyCode: " + e.keyCode);
            if (e.keyCode != KeyCode.None)
            {
                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    forward = e.keyCode;
                    holdingKey = KeyCode.None;
                    forwardText.text = forward.ToString();
                }
                else
                {
                    forward = holdingKey;
                    holdingKey = KeyCode.None;
                    forwardText.text = forward.ToString();
                }
            }
        }
        if (backward == KeyCode.None)
        {
            Debug.Log("KeyCode: " + e.keyCode);
            if (e.keyCode != KeyCode.None)
            {
                if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    backward = e.keyCode;
                    holdingKey = KeyCode.None;
                    backwardText.text = backward.ToString();
                }
                else
                {
                    backward = holdingKey;
                    holdingKey = KeyCode.None;
                    backwardText.text = backward.ToString();
                }
            }
        }
        if (left == KeyCode.None)
        {
            Debug.Log("KeyCode: " + e.keyCode);
            if (e.keyCode != KeyCode.None)
            {
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    left = e.keyCode;
                    holdingKey = KeyCode.None;
                    leftText.text = left.ToString();
                }
                else
                {
                    left = holdingKey;
                    holdingKey = KeyCode.None;
                    leftText.text = left.ToString();
                }
            }
        }
        if (right == KeyCode.None)
        {
            Debug.Log("KeyCode: " + e.keyCode);
            if (e.keyCode != KeyCode.None)
            {
                if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == backward || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    right = e.keyCode;
                    holdingKey = KeyCode.None;
                    rightText.text = right.ToString();
                }
                else
                {
                    right = holdingKey;
                    holdingKey = KeyCode.None;
                    rightText.text = right.ToString();
                }
            }
        }
        if (jump == KeyCode.None)
        {
            Debug.Log("KeyCode: " + e.keyCode);
            if (e.keyCode != KeyCode.None)
            {
                if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == backward || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    jump = e.keyCode;
                    holdingKey = KeyCode.None;
                    jumpText.text = jump.ToString();
                }
                else
                {
                    jump = holdingKey;
                    holdingKey = KeyCode.None;
                    jumpText.text = jump.ToString();
                }
            }
        }
        if (crouch == KeyCode.None)
        {
            Debug.Log("KeyCode: " + e.keyCode);
            if (e.keyCode != KeyCode.None)
            {
                if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == backward || e.keyCode == sprint || e.keyCode == interact))
                {
                    crouch = e.keyCode;
                    holdingKey = KeyCode.None;
                    crouchText.text = crouch.ToString();
                }
                else
                {
                    crouch = holdingKey;
                    holdingKey = KeyCode.None;
                    crouchText.text = crouch.ToString();
                }
            }
        }
        if (sprint == KeyCode.None)
        {
            Debug.Log("KeyCode: " + e.keyCode);
            if (e.keyCode != KeyCode.None)
            {
                if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == backward || e.keyCode == interact))
                {
                    sprint = e.keyCode;
                    holdingKey = KeyCode.None;
                    sprintText.text = sprint.ToString();
                }
                else
                {
                    sprint = holdingKey;
                    holdingKey = KeyCode.None;
                    sprintText.text = sprint.ToString();
                }
            }
        }
        if (interact == KeyCode.None)
        {
            Debug.Log("KeyCode: " + e.keyCode);
            if (e.keyCode != KeyCode.None)
            {
                if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == backward || e.keyCode == sprint))
                {
                    interact = e.keyCode;
                    holdingKey = KeyCode.None;
                    interactText.text = interact.ToString();
                }
                else
                {
                    interact = holdingKey;
                    holdingKey = KeyCode.None;
                    interactText.text = interact.ToString();
                }
            }
        }
    }
    public void Forward()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            holdingKey = forward;
            forward = KeyCode.None;
            forwardText.text = forward.ToString();
        }
    }
    public void Backward()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            holdingKey = forward;
            forward = KeyCode.None;
            forwardText.text = forward.ToString();
        }
    }
    public void Left()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            holdingKey = forward;
            forward = KeyCode.None;
            forwardText.text = forward.ToString();
        }
    }
    public void Right()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            holdingKey = forward;
            forward = KeyCode.None;
            forwardText.text = forward.ToString();
        }
    }
}