using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//you will need to change Scenes
public class CustomisationSet : MonoBehaviour
{

    #region Variables
    [Header("Texture List")]
    //Texture2D List for skin,hair, mouth, eyes
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    [Header("Index")]
    //index numbers for our current skin, hair, mouth, eyes textures
    public int skinIndex;
    public int hairIndex;
    public int mouthIndex;
    public int eyesIndex;
    public int clothesIndex;
    public int armourIndex;
    [Header("Renderer")]
    //renderer for our character mesh so we can reference a material list
    public Renderer character;
    [Header("Max Index")]
    //max amount of skin, hair, mouth, eyes textures that our lists are filling with
    public int skinMax;
    public int  hairMax, mouthMax, eyesMax, clothesMax, armourMax;

    [Header("Character Name")]
    //name of our character that the user is making
    public string charName = "Adventurer";

    [Header("Status")]
    public int str;
    public int dex, chr, con, intel, wis;
    public string[] statArray = new string[6];
    public int[] stats = new int[6];
    public int[] tempStats = new int[6];
    // the points in which we use to increase our stats
    public int points = 10;
    public CharacterClass charClass = CharacterClass.Barbarian;
    public string[] className = new string[8];
    public int selectedIndex = 0;

    [Header("Dropdown Menu")]
    
    public int classIndex;
    public bool showDropdown;
    public Vector2 scrollPos;

    public string classButton = "";



    #endregion

    #region Start
    //in start we need to set up the following

    #region for loop to pull textures from file

    private void Start()
    {
        statArray = new string[] { "Strength", "Dexterity", "Constitution", "Wisdom", "Intelligence", "Charisma" };
        className = new string[] {" Barbarian","Bard","Druid","Monk","Paladin","Ranger","Sorcerer","Warlock"};
        //for loop looping from 0 to less than the max amount of skin textures we need
        for (int i = 0; i < skinMax; i++)
        {

            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Skin_#
            Texture2D temp = Resources.Load("Character/Skin_" + i) as Texture2D;
            //add our temp texture that we just found to the skin List
            skin.Add(temp);
        }

        //for loop looping from 0 to less than the max amount of hair textures we need
        for (int i = 0; i < hairMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Hair_#
            Texture2D temp = Resources.Load("Character/Hair_" + i) as Texture2D;
            //add our temp texture that we just found to the hair List
            hair.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of mouth textures we need    
        for (int i = 0; i < mouthMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Mouth_#
            Texture2D temp = Resources.Load("Character/Mouth_" + i) as Texture2D;
            //add our temp texture that we just found to the mouth List
            mouth.Add(temp);
        }

        //for loop looping from 0 to less than the max amount of eyes textures we need
        for (int i = 0; i < eyesMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Eyes_#
            Texture2D temp = Resources.Load("Character/Eyes_" + i) as Texture2D;
            //add our temp texture that we just found to the eyes List      
            eyes.Add(temp);
        }
        for (int i = 0; i < clothesMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Clothes_" + i) as Texture2D;
            clothes.Add(temp);
        }
        for (int i = 0; i < armourMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Armour_" + i) as Texture2D;
            armour.Add(temp);
        }
        #endregion
        //connect and find the SkinnedMeshRenderer thats in the scene to the variable we made for Renderer
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        #region do this after making the function SetTexture
        //SetTexture skin, hair, mouth, eyes to the first texture 0
        SetTexture("Skin", 0);
        SetTexture("Hair", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Clothes", 0);
        SetTexture("Armour", 0);
        ChooseClass(selectedIndex);
    }
    #endregion
    #endregion

    #region SetTexture
    //the string is the name of the material we are editing, the int is the direction we are changing
    //Create a function that is called SetTexture it should contain a string and int
    void SetTexture(string type, int dir)
    {



        //we need variables that exist only within this function
        //these are ints index numbers, max numbers, material index and Texture2D array of textures
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];

        //inside a switch statement that is swapped by the string name of our material

        #region Switch Material
        switch (type)
        {
            //case skin
            case "Skin":
                //index is the same as our skin index
                index = skinIndex;

                //max is the same as our skin max
                max = skinMax;
                //textures is our skin list .ToArray()
                textures = skin.ToArray();
                //material index element number is 1
                matIndex = 1;
                //break
                break;
            //now repeat for each material 

            //hair is 2
            case "Hair":
                //index is the same as our index
                index = hairIndex;
                //max is the same as our max
                max = hairMax;
                //textures is our list .ToArray()
                textures = hair.ToArray();
                //material index element number is 2
                matIndex = 2;

                //break
                break;
            //mouth is 3
            case "Mouth":
                //index is the same as our index
                index = mouthIndex;
                //max is the same as our max
                max = mouthMax;
                //textures is our list .ToArray()
                textures = mouth.ToArray();

                //material index element number is 3
                matIndex = 3;
                //break
                break;
            //eyes are 4
            case "Eyes":
                //index is the same as our index
                index = eyesIndex;
                //max is the same as our max
                max = eyesMax;
                //textures is our list .ToArray()
                textures = eyes.ToArray();
                //material index element number is 4
                matIndex = 4;
                //break
                break;
            case "Clothes":
                //index is the same as our index
                index = clothesIndex;
                //max is the same as our max
                max = clothesMax;
                //textures is our list .ToArray()
                textures = clothes.ToArray();
                //material index element number is 4
                matIndex = 5;
                //break
                break;
            case "Armour":
                //index is the same as our index
                index = armourIndex;
                //max is the same as our max
                max = armourMax;
                //textures is our list .ToArray()
                textures = armour.ToArray();
                //material index element number is 4
                matIndex = 6;
                //break
                break;

        }
        #endregion
        #region OutSide Switch
        //outside our switch statement
        //index plus equals our direction
        index += dir;
        //cap our index to loop back around if is is below 0 or above max take one
        if (index < 0)
        {

            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }

        //Material array is equal to our characters material list
        Material[] mat = character.materials;

        //our material arrays current material index's main texture is equal to our texture arrays current index
        mat[matIndex].mainTexture = textures[index];
        //our characters materials are equal to the material array
        character.materials = mat;
        //create another switch that is goverened by the same string name of our material
        #endregion
        #region Set Material Switch

        switch (type)
        { //case skin
            case "Skin":
                //skin index equals our index
                skinIndex = index;
                //break
                break;
            //case hair
            case "Hair":
                //index equals our index
                hairIndex = index;
                //break
                break;
            //case mouth
            case "Mouth":
                //index equals our index
                mouthIndex = index;
                //break
                break;
            //case eyes
            case "Eyes":
                //index equals our index
                eyesIndex = index;
                //break
                break;
            case "Clothes":
                clothesIndex = index;
                break;
            case "Armour":
                armourIndex = index;
                break;
        }
    }

    #endregion
    #endregion

    #region Save
    //Function called Save this will allow us to save our indexes to PlayerPrefs
    void Save()
    {



        //SetInt for SkinIndex, HairIndex, MouthIndex, EyesIndex
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        PlayerPrefs.SetInt("ClothesIndex", clothesIndex);
        PlayerPrefs.SetInt("ArmourIndex", armourIndex);


        //SetString CharacterName
        PlayerPrefs.SetString("CharacterName", charName);
    }
    #endregion

    #region OnGUI
    //Function for our GUI elements
    private void OnGUI()
    {

        //create the floats scrW and scrH that govern our 16:9 ratio
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;


        //create an int that will help with shuffling your GUI elements under eachother
        int i = 0;
        #region Skin
        //GUI button on the left of the screen with the contence <


        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Skin", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Button(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1.5f * scrW, 0.5f * scrH), "Skin");
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(2.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Skin", 1);
        }
        //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        //set up same things for Hair, Mouth and Eyes
        #region Hair
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Material and move the texture index in the direction  -1
            SetTexture("Hair", -1);
        }


        //GUI Box or Lable on the left of the screen with the contence material Name
        GUI.Button(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1.5f * scrW, 0.5f * scrH), "Hair");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(2.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the  Material and move the texture index in the direction  1
            SetTexture("Hair", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Mouth
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Material and move the texture index in the direction  -1
            SetTexture("Mouth", -1);
        }


        //GUI Box or Lable on the left of the screen with the contence material Name
        GUI.Button(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1.5f * scrW, 0.5f * scrH), "Mouth");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(2.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the  Material and move the texture index in the direction  1
            SetTexture("Mouth", 1);
        }


        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Eyes
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Material and move the texture index in the direction  -1
            SetTexture("Eyes", -1);
        }


        //GUI Box or Lable on the left of the screen with the contence material Name
        GUI.Button(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1.5f * scrW, 0.5f * scrH), "Eyes");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(2.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the  Material and move the texture index in the direction  1
            SetTexture("Eyes", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Clothes
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Material and move the texture index in the direction  -1
            SetTexture("Clothes", -1);

        }

        //GUI Box or Lable on the left of the screen with the contence material Name
        GUI.Button(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1.5f * scrW, 0.5f * scrH), "Clothes");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(2.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the  Material and move the texture index in the direction  1
            SetTexture("Clothes", 1);
        }
        i++;
        #endregion
        #region Armour
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Material and move the texture index in the direction  -1
            SetTexture("Armour", -1);

        }

        //GUI Box or Lable on the left of the screen with the contence material Name
        GUI.Button(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1.5f * scrW, 0.5f * scrH), "Armour");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(2.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the  Material and move the texture index in the direction  1
            SetTexture("Armour", 1);
        }
        i++;

        #endregion
        #region Random Reset
        //create 2 buttons one Random and one Reset
        //Random will feed a random amount to the direction
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Random"))
        {
            SetTexture("Skin", Random.Range(0, skinMax - 1));
            SetTexture("Hair", Random.Range(0, hairMax - 1));
            SetTexture("Mouth", Random.Range(0, mouthMax - 1));
            SetTexture("Eyes", Random.Range(0, eyesMax - 1));
            SetTexture("Clothes", Random.Range(0, clothesMax - 1));
            SetTexture("Armour", Random.Range(0, armourMax - 1));

        }


        //reset will set all to 0 both use SetTexture
        if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Reset"))
        {
            SetTexture("Skin", skinIndex = 0);
            SetTexture("Hair", hairIndex = 0);
            SetTexture("Mouth", mouthIndex = 0);
            SetTexture("Eyes", eyesIndex = 0);
            SetTexture("Clothes", clothesIndex = 0);
            SetTexture("Armour", armourIndex = 0);

        }
        i++;
        i++;
        i++;
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        #endregion
        #region Character Name and Save & Play
        //name of our character equals a GUI TextField that holds our character name and limit of characters
        charName = GUI.TextField(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), charName, 16);

        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;

        //GUI Button called Save and Play
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Save & Play"))
        {
            //this button will run the save function and also load into the game level
            Save();
            SceneManager.LoadScene("Game");

        }
        #endregion

        #region Character Class
        i = 0;
            
            
         if(GUI.Button(new Rect(3.75f * scrW, scrH, 2*scrW, 0.5f*scrH), classButton))
        {
            showDropdown = !showDropdown;
        }
        if (showDropdown)
        {
            scrollPos = GUI.BeginScrollView(new Rect(6f*scrW,1f*scrH,2.5f*scrW,2f*scrH),scrollPos, new Rect(0,0,2*scrW,4f*scrH),false,true);
        for(int d = 0; d < className.Length; d++)
            {
                if(GUI.Button(new Rect(0,0+(0.5f*scrH)*d, 1.75f*scrW, 0.5f*scrH), className[d]))
                {
                    selectedIndex = d;
                    ChooseClass(selectedIndex);
                    classButton = className[d];
                    showDropdown = false;
                    points = 10;
                    tempStats[0] = 0;
                    tempStats[1] = 0;
                    tempStats[2] = 0;
                    tempStats[3] = 0;
                    tempStats[4] = 0;
                    tempStats[5] = 0;

                }
            }
            GUI.EndScrollView();

        }

       
        GUI.Box(new Rect(3.75f * scrW, 2f * scrH, 2f * scrW, 0.5f * scrH), "Points:" + points);
        for(int s =0; s<6; s++)
        {
            if (points > 0)
            {
                if(GUI.Button(new Rect(5.75f*scrW, 2.5f*scrH + s *(0.5f*scrH), 0.5f*scrW, 0.5f*scrH), "+"))
                {
                    points --;
                    tempStats[s]++;
                }
            }
            GUI.Box(new Rect(3.75f * scrW, 2.5f * scrH + s * (0.5f * scrH), 2f * scrW, 0.5f * scrH), statArray[s] + ":" + (stats[s] + tempStats[s]));
            if(points <10 && tempStats[s] > 0)
            {
                if(GUI.Button(new Rect(3.25f*scrW, 2.5f*scrH + s *(0.5f*scrH), 0.5f*scrW, 0.5f * scrH), "-"))
                {
                    points++;
                    tempStats[s]--;
                   
                }
                
                }
           
            }
        if (points < 10)
        {
            if (GUI.Button(new Rect(3.75f * scrW, 5.5f * scrH + i * (scrH), scrW, 0.5f * scrH), "Reset"))
            {
                points = 10;
                tempStats[0] = 0;
                tempStats[1] = 0;
                tempStats[2] = 0;
                tempStats[3] = 0;
                tempStats[4] = 0;
                tempStats[5] = 0;
            }


        }
        
        #endregion
    }

    #endregion
    void ChooseClass(int classIndexNum)
    {
        switch (classIndexNum)
        {
            case 0:
                stats[0] = 15;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Barbarian;
                break;
            case 1:
                stats[0] = 5;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 15;
                charClass = CharacterClass.Bard;
                break;
            case 2:
                stats[0] = 10;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 10;
                charClass = CharacterClass.Druid;
                break;
            case 3:
                stats[0] = 5;
                stats[1] = 15;
                stats[2] = 15;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Monk;
                break;

            case 4:
                stats[0] = 15;
                stats[1] = 10;
                stats[2] = 15;
                stats[3] = 5;
                stats[4] = 5;
                stats[5] = 10;
                charClass = CharacterClass.Paladin;
                break;
            case 5:
                stats[0] = 5;
                stats[1] = 15;
                stats[2] = 10;
                stats[3] = 15;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Ranger;
                break;
            case 6:
                stats[0] = 10;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 15;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Sorcerer;
                break;
            case 7:
                stats[0] = 5;
                stats[1] = 5;
                stats[2] = 5;
                stats[3] = 15;
                stats[4] = 15;
                stats[5] = 15;
                charClass = CharacterClass.Warlock;
                break;
        }
    }
}


public enum CharacterClass
    {
        Barbarian,
        Bard,
        Druid,
        Monk,
        Paladin,
        Ranger,
        Sorcerer,
        Warlock
    }