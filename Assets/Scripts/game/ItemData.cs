using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemData
{
    public static Item CreateItem(int ItemID)
    {
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

        switch (ItemID)
        {
            #region Consumables 0-99
            case 0:
                name = "Apple";
                description = "Munchies and Crunchies";
                value = 10;
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
                mesh = "Chesse_Mesh";
                type = ItemTypes.Consumables;
                break;
            case 2:
                name = "Meat";
                description = "Delicious Meat";
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
                name = "WoodenHelmet";
                description = "";
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
                name = "LeatherHelmet";
                description = "";
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
                name = "IronHelmet";
                description = "Yellow Gold";
                value = 50;
                damage = 0;
                armour = 25;
                amount = 1;
                heal = 0;
                icon = "IronHelmet_Icon";
                mesh = "IronHelmet_Mesh";
                type = ItemTypes.Consumables;
                break;

            #endregion
            #region Weapon 200-299
            case 200:
                name = "Wooden Sword";
                description = "Wooden Sword";
                value = 1;
                damage = 5;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "WoodenSword_Icon";
                mesh = "WoodenSword_Icon_Mesh";
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
                description = "For destroy enemies";
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
            #region Craftable 300-399
            case 300:
                name = "Oak Brunch";
                description = "Brunch of Oak";
                value = 1;
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "OakBrucnch_Icon";
                mesh = "OakBrucnch_Mesh";
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
                description = "";
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
            case 402:
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
            Meshname = mesh
        };
        return temp;

       

       
        



    }
}