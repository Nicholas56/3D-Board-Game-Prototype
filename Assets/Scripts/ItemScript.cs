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
        {
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

            items.Add(new Item(Item.itemType.InstaKill, "InstaKill", 0, 0, 50));
        }
        //Adds the items to the static list
        gameItems.AddRange(items);
    }

    public static Item GetItem(int id)
    {
        for (int i = 0; i < gameItems.Count; i++)
        {
            if(gameItems[i].itemID == id)
            {
                return gameItems[i];
            }
        }
        return null;
    }
}

public class Item
{
    public enum itemType { Stop, Move, InstaKill, TempHP, TempAtk, TempDef }
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
