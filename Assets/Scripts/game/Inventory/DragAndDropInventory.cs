﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropInventory : MonoBehaviour
{

    #region Variables
    [Header("Inventory")]
    public bool showInv; //toggle UI
    public static List<Item> inventory = new List<Item>();
    public int slotX, slotY;
    private Rect inventorySize;
    [Header("Dragging")]
    public bool isDragging; // are we dragging and item
    public Item draggedItem;
    public int draggedfrom;
    public GameObject droppedItem;
    [Header("Tool Tip")]
    public int toolTipItem; // index reference
    public bool showToolTip;
    private Rect toolTipRect;
    [Header("Other References")]
    private Vector2 scr;

    public GameObject player;
    public GameObject mainCam;
    #endregion

    #region Clamp to Screen
    private Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
    }

    #endregion
    #region AddItem
    public static void AddItem(int ItemID)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].Name == null)
            {
                inventory[i] = ItemData.CreateItem(ItemID);
                Debug.Log("Add Item:" + inventory[i].Name);
                return;
            }
        }
    }

    #endregion
    #region Drop Item
    public void DropItem(int ItemID)
    {
        // getting the Prefab of the item
        droppedItem = Resources.Load("Prefab/" + ItemData.CreateItem(ItemID).MeshName) as GameObject;
        //spawn in the item to the world and remember what item that was
        Instantiate(droppedItem, transform.position + transform.forward * 3, Quaternion.identity);
        //empty the dropped item.. done
        droppedItem = null;
    }
    #endregion
    #region DrawItem
    void DrawItem(int windowID)
    {
        if(draggedItem.Icon !=null)
        {
            GUI.DrawTexture(new Rect(0, 0, scr.x * 0.5f, scr.y * 0.5f), draggedItem.Icon);
        }
    }


    #endregion
    #region ToolTip
    #region ToolTip Content
    private string ToolTipText(int ItemID)
    {
        string toolTipText = "Name:"+inventory[ItemID].Name + "\n" +
            "Description:"+inventory[ItemID].Description + "\n" +
            "Type:" + inventory[ItemID].Type + "\n" +
            "Value:" + inventory[ItemID].Value; 

        return toolTipText;
    }
    #endregion
    #region ToolTip Window
    void DrawToolTip(int windowID)
    {
        GUI.Box(new Rect(0, 0, scr.x * 3, scr.y * 3), ToolTipText(toolTipItem));
    }

    #endregion
    #endregion
    #region ToggleInventory
    public bool ToggleInv()
    {
        if(showInv)
        {
            showInv = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<MouseLook>().enabled = true;
            mainCam.GetComponent<MouseLook>().enabled = true;
            Cursor.visible = false;
            return (false);
        }
        else
        {
            showInv = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player.GetComponent<MouseLook>().enabled = false;
            mainCam.GetComponent<MouseLook>().enabled = false;
            return (true);
        }
    }
    #endregion
    #region Drag Inventory
    void InventoryDrag(int windowID)
    {
        GUI.Box(new Rect(0, scr.y * 0.25f, scr.x * 6, scr.y * 0.5f), "Banner");
        GUI.Box(new Rect(0, scr.y * 4.25f, scr.x * 6, scr.y * 0.5f), "Gold n EXP");
        showToolTip = false;
        #region Nested for loop
        Event e = Event.current;
        int i = 0;
        for ( int y = 0; y < slotY; y++)
        {
            for (int x =0; x < slotX; x++)
            {
                Rect slotLocation = new Rect(scr.x * 0.125f + x * (scr.x * 0.75f), scr.y * 0.75f + y * (scr.y * 0.65f), scr.x * 0.75f, scr.y * 0.65f);
                GUI.Box(slotLocation, "");
                #region Pickup Item
                /* if we are interacting with leftmouse button and the interaction is click down and the mouse
                 cursor is over a item slot while we are not already holding and item and the item slot isn't 
                 empty as well as we aren't hitting the change inventory key left shift
                 */
                if(e.button == 0 && e.type == EventType.MouseDown && slotLocation.Contains(e.mousePosition)&&!isDragging && inventory[i].Name != null && !Input.GetKey(KeyCode.LeftShift))
                {
                    // we pick up item
                    draggedItem = inventory[i];

                    // teh inventory slot is now empty
                    inventory[i] = new Item();
                    // we are holding an item
                    isDragging = true;

                    // we remember where this item come from
                    draggedfrom = i;
                    // debug
                    Debug.Log("Dragging:" + draggedItem.Name);

                }
                #endregion
                #region Swap Item
                if(e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition)&& isDragging && inventory[i].Name != null)
                {
                    Debug.Log("Swapping:" + draggedItem.Name + "with:" + inventory[i].Name);
                    //the slot that is full now moves to where our dragged item come from
                    inventory[draggedfrom] = inventory[i];
                    //the slot we are dropping into is now filled with our dragged item
                    inventory[i] = draggedItem;
                    //the dragged item is now empty
                    draggedItem = new Item();
                    // we are no longer dragging
                    isDragging = false;

                }
                #endregion
                #region Place Item
                /*
                 * if we lift up left mouse button
                 * and we have a draggable item over a slot that is empty
                 */
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inventory[i].Name != null)
                {
                    Debug.Log("Place:" + draggedItem.Name + "Into:" + i);
                    // the slot we are dropping the item into is now filled with the draggedItem
                    inventory[i] = draggedItem;
                    // the item we use to drag is empty
                    draggedItem = new Item();
                    // we are no longer holding an item
                    isDragging = false;
                }
                    #endregion
                    #region Return Item
                if(e.button == 0 && e.type == EventType.MouseUp && i == ((slotX*slotY)-1)&& isDragging)
                {
                    //put the item back where you got it from.
                    inventory[draggedfrom] = draggedItem;
                    //dragged item is now empty
                    draggedItem = new Item();

                    // we are no longer dragging
                    isDragging = false;
                }
                    #endregion
                    #region Draw Item Icon
                if(inventory[i].Name != null)
                {
                    GUI.DrawTexture(slotLocation, inventory[i].Icon);
                    #region Set ToolTip on Mouse Hover
                    if(slotLocation.Contains(e.mousePosition)&& !isDragging && showInv)
                    {
                        toolTipItem = i;
                        showToolTip = true;
                    }
                    #endregion

                }

                #endregion
                i++;
            }
        }

        #endregion
        #region Drag Window
        GUI.DragWindow(new Rect(0,0,scr.x*6,scr.y*0.5f)); // Top Drag
        GUI.DragWindow(new Rect(0,scr.y*0.5f,scr.x*0.25f,scr.y*3.5f)); // Left Drag
        GUI.DragWindow(new Rect(scr.x*5.75f,scr.y*0.5f,scr.x*0.25f,scr.y*3.5f)); // Right Drag
        GUI.DragWindow(new Rect(0,scr.y*4,scr.x*0.25f,scr.y*0.5f)); // Bottom Drag
        #endregion
    }
    #endregion
    #region Start
    private void Start()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;
        inventorySize = new Rect(scr.x, scr.y, 6 * scr.x, 4.5f * scr.y);
        for (int i = 0; i < slotX * slotY; i++)
        {
            inventory.Add(new Item());
        }
        AddItem(0);
        AddItem(2);
        AddItem(102);
        AddItem(201);
        AddItem(202);
        AddItem(302);
    }

    #endregion
    #region Update
    private void Update()
    {
        if(scr.x != Screen.width / 16 || scr.y != Screen.height / 9)
        {
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;          
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInv();
        }
    }

    #endregion
    #region OnGUI
    private void OnGUI()
    {
        Event e = Event.current;
        #region Inventory if showInv is true
        if (showInv)
        {
            inventorySize = ClampToScreen(GUI.Window(1, inventorySize, InventoryDrag, "Drag Inventory"));
        }
        #endregion
        #region ToolTip
        if(showToolTip && showInv)
        {
            toolTipRect = new Rect(e.mousePosition.x + 0.01f, e.mousePosition.y + 0.001f, scr.x * 3, scr.y * 3);
            GUI.Window(15, toolTipRect, DrawToolTip, "");
        }

        #endregion
        #region Drop Item(Mouse Up || !showInv)
        if (e.button == 0 && e.type == EventType.MouseUp && isDragging || isDragging && !showInv)
        {
            DropItem(draggedItem.Id);
            Debug.Log("Dropped: " + draggedItem.Name);
            draggedItem = new Item();
            isDragging = false;
        }

        #endregion

        #region Draw Item on Mouse
        if (isDragging)
        {
            if(draggedItem != null)
            {
                Rect mouseLocation = new Rect(e.mousePosition.x + 0.125f, e.mousePosition.y + 0.125f, scr.x * 0.5f, scr.y * 0.5f);
                GUI.Window(2, mouseLocation, DrawItem, "");
                // mouseLocation = ClampToScreen(GUI.Window(2,mouseLocatin,DramItem,""));
            }
        }
        #endregion
    }

    #endregion


}
