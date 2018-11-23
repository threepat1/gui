﻿using UnityEngine;

public static class ItemData
{
    public static Item CreateItem(int ItemID)
    {
        //This is what we need to make an item
        string name = "";
        string description = "";
        int value = 0;
        int damage = 0;
        int armour = 0;
        int amount = 0;
        int heal = 0;
        string icon = "";
        string mesh = "";
        ItemTypes type = ItemTypes.Armour;

        //This is where we set the item data
        switch (ItemID)
        {
            #region Consumables 0-99
            case 0:
                name = "Apple";
                description = "Munchies and Crunchies";
                value = 5;
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 10;
                icon = "Apple_Icon";
                mesh = "Apple_Mesh";
                type = ItemTypes.Consumables;
                break;
            case 1:
                name = "Cheese";
                description = "Yellow Gold";
                value = 25;
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 50;
                icon = "Cheese_Icon";
                mesh = "Cheese_Mesh";
                type = ItemTypes.Consumables;
                break;
            case 2:
                name = "Meat";
                description = "Mystery Meat";
                value = 15;
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 17;
                icon = "Meat_Icon";
                mesh = "Meat_Mesh";
                type = ItemTypes.Consumables;
                break;
            #endregion
            #region Armour 100-199
            case 100:
                name = "Wooden Helmet";
                description = "More of a bucket really";
                value = 1;
                damage = 0;
                armour = 1;
                amount = 1;
                heal = 0;
                icon = "WoodenHelmet_Icon";
                mesh = "WoodenHelmet_Mesh";
                type = ItemTypes.Armour;
                break;
            case 101:
                name = "Leather Helmet";
                description = "Hemlet made from hide";
                value = 25;
                damage = 0;
                armour = 10;
                amount = 1;
                heal = 0;
                icon = "LeatherHelmet_Icon";
                mesh = "LeatherHelmet_Mesh";
                type = ItemTypes.Armour;
                break;
            case 102:
                name = "Iron Helmet";
                description = "He seems abit think dont he";
                value = 50;
                damage = 0;
                armour = 25;
                amount = 1;
                heal = 0;
                icon = "IronHelmet_Icon";
                mesh = "IronHelmet_Mesh";
                type = ItemTypes.Armour;
                break;
            #endregion
            #region Weapon 200-299
            case 200:
                name = "Wooden Sword";
                description = "Practice Sword";
                value = 1;
                damage = 5;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "WoodenSword_Icon";
                mesh = "WoodenSword_Mesh";
                type = ItemTypes.Weapon;
                break;
            case 201:
                name = "Iron Axe";
                description = "For chopping wood or enemies";
                value = 25;
                damage = 25;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "IronAxe_Icon";
                mesh = "IronAxe_Mesh";
                type = ItemTypes.Weapon;
                break;
            case 202:
                name = "Iron Sword";
                description = "Which end do I hold?";
                value = 50;
                damage = 50;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "IronSword_Icon";
                mesh = "IronSword_Mesh";
                type = ItemTypes.Weapon;
                break;
            #endregion
            #region Crafting 300-399
            case 300:
                name = "Oak Branch";
                description = "Thick Branch of Oak";
                value = 1;
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "OakBranch_Icon";
                mesh = "OakBranch_Mesh";
                type = ItemTypes.Craftable;
                break;
            case 301:
                name = "Iron Ore";
                description = "Iron...kinda";
                value = 2;
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "IronOre_Icon";
                mesh = "IronOre_Mesh";
                type = ItemTypes.Craftable;
                break;
            case 302:
                name = "Iron Ingot";
                description = "Bar of Iron";
                value = 10;
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "IronIngot_Icon";
                mesh = "IronIngot_Mesh";
                type = ItemTypes.Craftable;
                break;
            #endregion
            #region Misc 400-499
            case 400:
                name = "Jug";
                description = "Holds water";
                value = 1;
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "Jug_Icon";
                mesh = "Jug_Mesh";
                type = ItemTypes.Misc;
                break;
            case 401:
                name = "Sword Fragment";
                description = "Broken Sword";
                value = 0;
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "SwordFragment_Icon";
                mesh = "SwordFragmen_Mesh";
                type = ItemTypes.Quest;
                break;
            case 402:
                name = "Vape";
                description = "Cloud Maker";
                value = 0;
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "Vape_Icon";
                mesh = "Vape_Mesh";
                type = ItemTypes.Quest;
                break;
            #endregion
            default:
                ItemID = 0;
                name = "Apple";
                description = "Munchies and Crunchies";
                value = 5;
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 10;
                icon = "Apple_Icon";
                mesh = "Apple_Mesh";
                type = ItemTypes.Consumables;
                break;
        }

        //this is where we create the item
        Item temp = new Item
        {
            Name = name,
            Description = description,
            Id = ItemID,
            Value = value,
            Damage = damage,
            Armour = armour,
            Amount = amount,
            Heal = heal,
            Type = type,
            Icon = Resources.Load("Icons/" + icon) as Texture2D,
            MeshName = mesh
        };
        return temp;
    }

}