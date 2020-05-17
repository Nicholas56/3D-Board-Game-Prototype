using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript: MonoBehaviour
{
    public static List<Item> gameItems = new List<Item>();

    private void Start()
    {
        gameItems.Clear();
        List<Item> items = new List<Item>();
        {//Number at the end is the item ID
            items.Add(new Item(Item.itemType.Stop, "Stop", 0, 0, 00));
            //MOVE boosters
            items.Add(new Item(Item.itemType.Move, "Move + 1", 1, 0, 10));
            items.Add(new Item(Item.itemType.Move, "Move + 2", 2, 0, 11));
            items.Add(new Item(Item.itemType.Move, "Move + 3", 3, 0, 12));
            items.Add(new Item(Item.itemType.Move, "Move + 4", 4, 0, 13));
            items.Add(new Item(Item.itemType.Move, "Move + 5", 5, 0, 14));
            //HP BOOST
            items.Add(new Item(Item.itemType.TempHP, "HP Boost:1", 1, 1, 20));
            items.Add(new Item(Item.itemType.TempHP, "HP Boost:2", 2, 1, 21));
            items.Add(new Item(Item.itemType.TempHP, "HP Boost:5", 5, 1, 22));
            items.Add(new Item(Item.itemType.TempHP, "HP Boost:5*2", 5, 2, 23));
            items.Add(new Item(Item.itemType.TempHP, "HP Boost:10", 10, 1, 24));
            //ATK BOOST
            items.Add(new Item(Item.itemType.TempAtk, "Atk Boost:1", 1, 1, 30));
            items.Add(new Item(Item.itemType.TempAtk, "Atk Boost:2", 2, 1, 31));
            items.Add(new Item(Item.itemType.TempAtk, "Atk Boost:2*2", 2, 2, 32));
            items.Add(new Item(Item.itemType.TempAtk, "Atk Boost:3", 3, 1, 33));
            items.Add(new Item(Item.itemType.TempAtk, "Atk Boost:3*2", 3, 2, 34));
            //DEF BOOST
            items.Add(new Item(Item.itemType.TempDef, "Def Boost:1", 1, 1, 40));
            items.Add(new Item(Item.itemType.TempDef, "Def Boost:2", 2, 1, 41));
            items.Add(new Item(Item.itemType.TempDef, "Def Boost:2*2", 2, 2, 42));
            items.Add(new Item(Item.itemType.TempDef, "Def Boost:3", 3, 1, 43));
            items.Add(new Item(Item.itemType.TempDef, "Def Boost:3*2", 3, 2, 44));
            //InstaKill Item
            items.Add(new Item(Item.itemType.InstaKill, "InstaKill", 0, 0, 50));
            //ABILITY BOONS
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: s. HP Boon", 001, 0, 60));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: m. HP Boon", 002, 0, 61));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: l. HP Boon", 003, 0, 62));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: s. ATK Boon", 101, 0, 63));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: m. ATK Boon", 102, 0, 64));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: l. ATK Boon", 103, 0, 65));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: s. DEF Boon", 201, 0, 66));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: m. DEF Boon", 202, 0, 67));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: l. DEF Boon", 203, 0, 68));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: s. MV Boon", 401, 0, 69));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: m. MV Boon", 402, 0, 70));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: l. MV Boon", 403, 0, 71));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: Fix 1", 501, 0, 72));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: Fix 2", 502, 0, 73));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: Fix 3", 503, 0, 74));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: Fix 4", 504, 0, 75));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: Fix 5", 505, 0, 76));
            items.Add(new Item(Item.itemType.AbilityAdd, "Learn: Fix 6", 506, 0, 77));
            //Heal Item
            items.Add(new Item(Item.itemType.Heal, "Heal", 5, 0, 78));
        }
        //Adds the items to the static list
        gameItems.AddRange(items);
    }

    public static Item GetItem(int id)
    {
        return gameItems.Find(x => x.itemID == id);
    }
}

public struct Item
{
    public enum itemType { Stop, Move, InstaKill, TempHP, TempAtk, TempDef, AbilityAdd, Heal }
    public itemType iType;

    public string itemName;
    public int itemID;
    public int value;
    public int tempDuration;

    public Item(itemType type,string iName, int itemValue, int tempNum, int id)
    {
        iType = type;
        itemName = iName;
        itemID = id;
        value = itemValue;
        tempDuration = tempNum;
    }
}
