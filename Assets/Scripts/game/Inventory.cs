using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Variables
    public static List<Item> inv = new List<Item>(); // list of items
    public static bool showInv; // show or hide inventory
    public Item selectedItem; // the item we are interacting with
    public static int money; // how much moolah we have
    public GameObject mainCam;
    public GameObject player;

    
    public Vector2 scr = Vector2.zero; // 16:9
    public Vector2 scrollpos = Vector2.zero; //scroll bar position

    public string sortType = "All";

    public Transform dropLocation;
    public Transform[] equippedLocation;
    public GameObject curWeapon;
    public GameObject curHelm;
    // 0 = right hand // weapon
    // 1 = head // helmet
    #endregion
    // Use this for initialization
    void Start()
    {
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(102));
        inv.Add(ItemData.CreateItem(201));
        inv.Add(ItemData.CreateItem(202));
        inv.Add(ItemData.CreateItem(302));

        for (int i = 0; i < inv.Count; i++)
        {
            Debug.Log(inv[i].Name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!PauseMenu.paused)
            { 
            ToggleInv();
            }
        }
       /* if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (showInv)
            {

                inv.Add(ItemData.CreateItem(0));
                inv.Add(ItemData.CreateItem(2));
                inv.Add(ItemData.CreateItem(102));
                inv.Add(ItemData.CreateItem(201));
                inv.Add(ItemData.CreateItem(202));
                inv.Add(ItemData.CreateItem(302));
                inv.Add(ItemData.CreateItem(404));

            }
        }*/
    }
    public bool ToggleInv()
    {
        if (showInv)
        {
            showInv = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.GetComponent<CharacterMovement>().enabled = true;
            player.GetComponent<MouseLook>().enabled = true;
            mainCam.GetComponent<MouseLook>().enabled = true;
         

            return (false);
            

        }
        else
        {
            showInv = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player.GetComponent<CharacterMovement>().enabled = false;
            player.GetComponent<MouseLook>().enabled = false;
            mainCam.GetComponent<MouseLook>().enabled = false;
            return (true);
        }
    }
    private void OnGUI()
    {
        if (!PauseMenu.paused) // only display is not paused
        {
            if (showInv) // and it toggled on;
            {
                if (scr.x != Screen.width / 16 || scr.y != Screen.height / 9) // update screen when needed
                {
                    scr.x = Screen.width / 16;
                    scr.y = Screen.height / 9;
                }
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Inventory");
                if(GUI.Button(new Rect(5.5f*scr.x, 0.25f*scr.y,scr.x,0.25f*scr.y),"All"))
                {
                    sortType = "All";
                }
                if (GUI.Button(new Rect(6.5f * scr.x, 0.25f * scr.y, scr.x, 0.25f * scr.y), "Consumables"))
                {
                    sortType = "Consumables";
                }
                if (GUI.Button(new Rect(7.5f * scr.x, 0.25f * scr.y, scr.x, 0.25f * scr.y), "Craftable"))
                {
                    sortType = "Craftable";
                }
                if (GUI.Button(new Rect(8.5f * scr.x, 0.25f * scr.y, scr.x, 0.25f * scr.y), "Weapon"))
                {
                    sortType = "Weapon";
                }
                if (GUI.Button(new Rect(9.5f * scr.x, 0.25f * scr.y, scr.x, 0.25f * scr.y), "Armour"))
                {
                    sortType = "Armour";
                }
                if (GUI.Button(new Rect(10.5f * scr.x, 0.25f * scr.y, scr.x, 0.25f * scr.y), "Misc"))
                {
                    sortType = "Misc";
                }
                if (GUI.Button(new Rect(11.5f * scr.x, 0.25f * scr.y, scr.x, 0.25f * scr.y), "Quest"))
                {
                    sortType = "Quest";
                }

                DisplayInv(sortType);
                if (selectedItem != null)
                {
                    GUI.DrawTexture(new Rect(11 * scr.x, 1.5f * scr.y, 2 * scr.x, 2 * scr.y), selectedItem.Icon);
                    if (GUI.Button(new Rect(14 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Discard"))
                    { if(curWeapon != null && selectedItem.MeshName == curWeapon.name)
                        {
                            Destroy(curWeapon);
                        }
                        else if(curHelm != null && selectedItem.MeshName == curHelm.name)
                        {
                            Destroy(curHelm);
                        }

                        {
                        //spawn item on ground
                        GameObject clone = Instantiate(Resources.Load("Prefab/" + selectedItem.MeshName) as GameObject, dropLocation.position, Quaternion.identity);
                        clone.AddComponent<Rigidbody>().useGravity = true;

                        if (selectedItem.Amount > 1)
                        {
                            selectedItem.Amount--;
                        }
                        else
                        {
                            inv.Remove(selectedItem);
                            selectedItem = null;
                        }
                        return;
                    }
                    }

                    switch (selectedItem.Type)
                    {
                        case ItemTypes.Consumables:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue:" + selectedItem.Value + "\nHeal: " + selectedItem.Heal + "\nAmount: " + selectedItem.Amount);
                            if (CharacterHandler.curHealth < CharacterHandler.maxHealth)
                            { 
                                if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Eat"))
                                {
                                    CharacterHandler.curHealth += selectedItem.Heal;
                                    if(selectedItem.Amount > 1)
                                    {
                                        selectedItem.Amount--;
                                    }
                                    else
                                    {
                                        inv.Remove(selectedItem);
                                        selectedItem = null;
                                    }
                                    
                                    return;
                                }
                            }
                            if (GUI.Button(new Rect(14 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Discard"))
                            {
                                //spawn item on groud
                                Instantiate(Resources.Load("Prefab/" + selectedItem.MeshName) as GameObject, dropLocation.position,Quaternion.identity);
                               
                                if (selectedItem.Amount > 1)
                                {
                                    selectedItem.Amount--;
                                }
                                else
                                {
                                    inv.Remove(selectedItem);
                                    selectedItem = null;
                                }
                                return;
                            }
                            break;
                        case ItemTypes.Craftable:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue:" + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                            if (GUI.Button(new Rect(15 * scr.x, 8.752f * scr.y, scr.x, 0.25f * scr.y), "Use"))
                            {
                                //Craft System
                            }
                            break;
                        case ItemTypes.Armour:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue:" + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                            if (curHelm == null || selectedItem.MeshName != curHelm.name)
                            {
                                if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Wear"))
                                {
                                    //use and spawn to character
                                    if (curHelm != null)
                                    {
                                        Destroy(curHelm);
                                    }
                                    curHelm = Instantiate(Resources.Load("Prefab/" + selectedItem.MeshName) as GameObject, equippedLocation[1]);

                                    curHelm.GetComponent<ItemHandler>().enabled = false;
                                    curHelm.name = selectedItem.MeshName;
                                }
                            }

                            break;
                        case ItemTypes.Weapon:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue:" + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                            if (curWeapon == null || selectedItem.MeshName != curWeapon.name)
                            {
                                if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Equip"))
                                {
                                    //use and spawn to character
                                    if (curWeapon != null)
                                    {
                                        Destroy(curWeapon);
                                    }
                                    curWeapon = Instantiate(Resources.Load("Prefab/" + selectedItem.MeshName) as GameObject, equippedLocation[0]);

                                    curWeapon.GetComponent<ItemHandler>().enabled = false;
                                    curWeapon.name = selectedItem.MeshName;
                                }
                            }
                            break;
                        case ItemTypes.Misc:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue:" + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                            break;


                        case ItemTypes.Quest:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue:" + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                            break;
                    }
                }


            }
        }

    }
    void DisplayInv(string sortType)
    {
        if (!(sortType == "All" || sortType == ""))
        {

            ItemTypes type = (ItemTypes)System.Enum.Parse(typeof(ItemTypes), sortType);
            int a = 0;//amount of that type
            int s = 0;//slot position of ui item
            for (int i = 0; i < inv.Count; i++) // loop throw all items
            {
                if (inv[i].Type == type) // find our type
                {
                    a++; //increase the amount of this type
                }
            }

            if (a <= 35) // if the amount of this type is less than 35
            {
                for (int i = 0; i < inv.Count; i++) //we filter through all items
                {
                    if (inv[i].Type == type) //if it is of type
                    {
                        //we display a button that is of this type and slot it under the last using s as our height
                        if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + s * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                        {
                            selectedItem = inv[i]; //this button is our selected item from our inventory
                            Debug.Log(selectedItem.Name); //tell us its name

                        }
                        s++; //once added increase our s
                        //each new thing goes under the last
                    }

                }
            }
            else //we have more than 35 in this type
            {
                // we need a scroll view
                //remove the previous 35 from our type amount a
                scrollpos = GUI.BeginScrollView(new Rect(0, 0.25f * scr.y, 3.75f * scr.x, 8.75f * scr.y), scrollpos, new Rect(0, 0, 0, 8.75f * scr.y + ((a - 35) * (0.25f * scr.y))), false, true);
                #region Items in Viewing Area
                for (int i = 0; i < inv.Count; i++)//loop throw all items
                {
                    if (inv[i].Type == type)//if it is of type
                    {
                        //we display a button that is of this type and slot it under the last using s as our height
                        if (GUI.Button(new Rect(0.5f * scr.x, 0 * scr.y + s * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                        {
                            selectedItem = inv[i]; //this button is our selected item from our inventory
                            Debug.Log(selectedItem.Name); //tell us its name
                        }
                        s++;
                        //once added increase our s
                        //each new thing goes under the last
                    }
                }

                #endregion
                GUI.EndScrollView();
            }

        }

        else
        {

            #region Non Scroll Inventory

            if (inv.Count <= 35)
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, 0.5f * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                    {
                        selectedItem = inv[i];
                        Debug.Log(selectedItem.Name);
                    }
                }
            }


            #endregion
            #region Scroll Inventory

            else // we are greater than 35
            {
                // our moved position in scolling            // our view window
                scrollpos = GUI.BeginScrollView(new Rect(0, 0.25f * scr.y, 6 * scr.x, 8.75f * scr.y),
                    // our current position in the scrolling
                    scrollpos,
                    // the viewable area 
                    new Rect(0, 0, 0, 8.75f * scr.y + ((inv.Count - 35) * (0.25f * scr.y))),
                    // can we see the horizontal bar?
                    false,
                    // can we see the vertical bar?
                    true);
                #region Item in Viewing area
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, 0 * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                    {
                        selectedItem = inv[i];
                        Debug.Log(selectedItem.Name);
                    }
                }
                #endregion


                GUI.EndScrollView();
            }

            #endregion
        }     
    }
}
