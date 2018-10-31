using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Variables
    public List<Item> inv = new List<Item>(); // list of items
    public static bool showInv; // show or hide inventory
    public Item selectedItem; // the item we are interacting with
    public int money; // how much moolah we have


    public Vector2 scr = Vector2.zero; // 16:9
    public Vector2 scrollpos = Vector2.zero; //scroll bar position
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
        inv.Add(ItemData.CreateItem(404));

        for (int i = 0; i < inv.Count; i++)
        {
            Debug.Log(inv[i].Name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleInv();
        }

    }
    public bool ToggleInv()
    {
        if (showInv)
        {
            showInv = false;
            Time.timeScale = 1;
            return (false);
        }
        else
        {
            showInv = true;
            Time.timeScale = 0;
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

                    #endregion
                    #region Scroll Inventory



                    #endregion

                }
            }
        }

    }
}
