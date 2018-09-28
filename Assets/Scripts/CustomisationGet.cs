﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//you will need to change Scenes
public class CustomisationGet : MonoBehaviour {

    [Header("Character")]
    //public variable for the Skinned Mesh Renderer which is our character reference
    public Renderer charMesh;
    public CharacterHandler charH;


    #region Start
    public void Start()
    {


        //our character reference connected to the Skinned Mesh Renderer via finding the Mesh
        charMesh = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        //Run the function LoadTexture	

        LoadTexture();
    }
    #endregion

    #region LoadTexture Function
    void LoadTexture()
    {
        //check to see if PlayerPrefs (our save location) HasKey (has a save file...you will need to reference the name of a file)
        if (!PlayerPrefs.HasKey("CharacterName"))
        {
            //if it doesnt then load the CustomSet level
            SceneManager.LoadScene(1);
        }
            //if it does have a save file then load and SetTexture Skin, Hair, Mouth and Eyes from PlayerPrefs
            SetTexture("Skin", PlayerPrefs.GetInt("SkinIndex"));
            SetTexture("Hair", PlayerPrefs.GetInt("HairIndex"));
            SetTexture("Mouth", PlayerPrefs.GetInt("MouthIndex"));
            SetTexture("Eyes", PlayerPrefs.GetInt("EyesIndex"));
            SetTexture("Clothes", PlayerPrefs.GetInt("ClothesIndex"));
            SetTexture("Armour", PlayerPrefs.GetInt("ArmourIndex"));
            //grab the gameObject in scene that is our character and set its Object name to the Characters name
            gameObject.name = PlayerPrefs.GetString("CharacterName");
            
        
    }
    #endregion
    #region SetTexture
    //Create a function that is called SetTexture it should contain a string and int
    void SetTexture(string type, int dir) //the string is the name of the material we are editing, the int is the direction we are changing
    {
        //we need variables that exist only within this function
        //these are int material index and Texture2D array of textures
        Texture2D tex = null;
        int matIndex = 0;
        //inside a switch statement that is swapped by the string name of our material
        switch (type)
        {
            //case skin      
            case "Skin":
                //textures is our Resource.Load Character Skin save index we loaded in set as our Texture2D
                tex = Resources.Load("Character/Skin_" + dir.ToString()) as Texture2D;
                //material index element number is 1
                matIndex = 1;
                //break
                break;
            //now repeat for each material 
            case "Hair":
                //hair is 2
                tex = Resources.Load("Character/Hair_" + dir.ToString()) as Texture2D;
                matIndex = 2;
                break;
            //mouth is 3
            case "Mouth":
                tex = Resources.Load("Character/Mouth_" + dir.ToString()) as Texture2D;
                matIndex = 3;
                break;
            //eyes are 4
            case "Eyes":
                tex = Resources.Load("Character/Eyes_" + dir.ToString()) as Texture2D;
                matIndex = 4;
                break;
            case "Clothes":
                tex = Resources.Load("Character/Clothes_" + dir.ToString()) as Texture2D;
                matIndex = 5;
                break;
            case "Armour":
                tex = Resources.Load("Character/Armour_" + dir.ToString()) as Texture2D;
                matIndex = 6;
                break;

        }
      

        //Material array is equal to our characters material list
        Material[] mats = charMesh.materials;
        //our material arrays current material index's main texture is equal to our texture arrays current index
        mats[matIndex].mainTexture = tex;
        //our characters materials are equal to the material array
        charMesh.materials = mats;
        #endregion

    }
  
}
