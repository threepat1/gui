using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;
//this script can be found in the Component section under the option Character Set Up 
//Character Handler
public class CharacterHandler : MonoBehaviour
{
    #region Variables
    [Header("Character")]

    #region Character 
    //bool to tell if the player is alive
    public bool alive;
    //connection to players character controller
    public CharacterController controller;
    #endregion
    [Header("Health")]
    #region Health
    //max and min health
    public static float maxHealth;
    public static float curHealth;
    public GUIStyle healthBar;
    #endregion
    //[Header("Levels and Exp")]
    #region Level and Exp
    //players current level
    public int level;
    //max and min experience 
    public int maxExp, curExp;
    #region Status
   
    public string[] statArray = new string[6];
    public int[] stats = new int[6];
    public int[] tempStats = new int[6];
    public CharacterClass charClass = CharacterClass.Barbarian;

    public int points = 0;
  
    public bool levelUp;





    #endregion

    #endregion
    [Header("Camera Connection")]
    #region MiniMap
    //render texture for the mini map that we need to connect to a camera
    public RenderTexture miniMap;
    #endregion
    #region Start
    public void Start()
    {
        //set up status
        statArray = new string[] { "Strength", "Dexterity", "Constitution", "Wisdom", "Intelligence", "Charisma" };
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = PlayerPrefs.GetInt(statArray[i], (stats[i] + tempStats[i]));
        }
        charClass = (CharacterClass)System.Enum.Parse(typeof(CharacterClass), PlayerPrefs.GetString("CharacterClass", "Barbarian"));

        // level =  PlayerPrefs.GetInt("Level",1);
        // maxExp =  PlayerPrefs.GetInt("MaxEXP", 60);
        // curExp = PlayerPrefs.GetInt("CurrentEXP", 0);
        //set max health to 100
        maxHealth = 100;
        maxHealth += stats[2] * 5;
        //set current health to max
        curHealth = maxHealth - 10;
        level = 1;
        //make sure player is alive
        alive = true;
        //max exp starts at 60
        maxExp = 60;
        //connect the Character Controller to the controller variable
        controller = this.GetComponent<CharacterController>();

        

        
        
    }
    #endregion
    #region Update
    private void Update()
    {
    
        //if our current experience is greater or equal to the maximum experience
        if (curExp >= maxExp)
        {
            //then the current experience is equal to our experience minus the maximum amount of experience
            curExp -= maxExp;
            //our level goes up by one
            level++;

            levelUp = true;
            points += 5;
            //write

            //the maximum amount of experience is increased by 50
            maxExp += 50;


        }
        curHealth = (int)curHealth;
    }
    #endregion
    #region LateUpdate
    private void LateUpdate()
    {
        //if our current health is greater than our maximum amount of health
        if (curHealth > maxHealth)
        {
            //then our current health is equal to the max health
            curHealth = maxHealth;
        }
        //if our current health is less than 0 or we are not alive
        if (curHealth < 0 || !alive)
        {
            //current health equals 0
            curHealth = 0;
            Debug.Log("if less than 0 = 0");
        }
        //if the player is alive
        if (alive && curHealth == 0)
        {
            //and our health is less than or equal to 0

            //alive is false
            alive = false;

            //controller is turned off
            controller.enabled = false;
            Debug.Log("Disable on death");

        }

    }
    #endregion
    #region OnGUI
    private void OnGUI()
    {


        //set up our aspect ratio for the GUI elements
        float scrW = Screen.width / 16;
        //scrW - 16
        float scrH = Screen.height / 9;

        /*for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                GUI.Box(new Rect(scrW * x,scrH*y,scrW,scrH),"");
            }
        }
        */
        if (!Inventory.showInv)
        {
            //GUI Box on screen for the healthbar background
            GUI.Box(new Rect(6 * scrW, 0.25f * scrH, 4 * scrW, 0.5f * scrH), "");
            //GUI Box for current health that moves in same place as the background bar
            GUI.Box(new Rect(6 * scrW, 0.25f * scrH, curHealth * (4 * scrW) / maxHealth, 0.5f * scrH), "", healthBar);
            //current Health divided by the posistion on screen and timesed by the total max health
            //GUI Box on screen for the experience background
            GUI.Box(new Rect(6 * scrW, 0.75f * scrH, 4 * scrW, 0.5f * scrH), "");
            //GUI Box for current experience that moves in same place as the background bar
            GUI.Box(new Rect(6 * scrW, 0.75f * scrH, curExp * (4 * scrW) / maxExp, 0.5f * scrH), "");
            //current experience divided by the posistion on screen and timesed by the total max experience


            //GUI Draw Texture on the screen that has the mini map render texture attached
            GUI.DrawTexture(new Rect(13.75f * scrW, 0.25f * scrH, 2 * scrW, 2 * scrH), miniMap);
        }
        //Text

        if (levelUp) 
        {
            Cursor.lockState = CursorLockMode.Confined;
            //hide cursor
            Cursor.visible = true;

            GUI.Box(new Rect(3.75f * scrW, 2f * scrH, 2f * scrW, 0.5f * scrH), "Points:" + points);

        for (int s = 0; s < statArray.Length; s++)
        {
            if (points > 0)
            {
                if (GUI.Button(new Rect(5.75f * scrW, 2.5f * scrH + s * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+"))
                {
                    points--;
                    tempStats[s]++;
                }
            }
            GUI.Box(new Rect(3.75f * scrW, 2.5f * scrH + s * (0.5f * scrH), 2f * scrW, 0.5f * scrH), statArray[s] + ":" + (stats[s] + tempStats[s]));
            if (points < 100 && tempStats[s] > 0)
            {
                if (GUI.Button(new Rect(3.25f * scrW, 2.5f * scrH + s * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-"))
                {
                    points++;
                    tempStats[s]--;

                }
                    
            }
            

        }
 
               if (GUI.Button(new Rect(0.25f * scrW, scrH + 1 * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Save"))
                {
                    
                    Save();
                for (int i = 0; i < 6; i++)
                {
                    tempStats[i] = 0;
                }

                levelUp = false;
            }
            
        }
        #endregion
       
    }
    #endregion
    void Save()
    {
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("MaxEXP", maxExp);
        PlayerPrefs.SetInt("CurrentEXP", curExp);

        for (int i = 0; i < stats.Length; i++)
        {
            PlayerPrefs.SetInt(statArray[i], (stats[i]+ tempStats[i]));

        }
        

    }

}