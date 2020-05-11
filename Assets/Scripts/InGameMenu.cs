using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*EAS12337350
 * This script will handle the options from the menu used in game
 */

public class InGameMenu : MonoBehaviour
{
    PlayerTurnScript player;

    public GameObject options;

    public GameObject inventory;
    GameObject[] inventorySlots;
    int startItem = 0;

    private void Start()
    {
        GetItemList();
        player = gameObject.GetComponent<PlayerTurnScript>();
        gameObject.GetComponent<EventEffects>().SetPlayerTurn(player);
    }

    public void DisplayInventory()
    {//This toggles the inventory on button click
        inventory.SetActive(!inventory.activeSelf);
        startItem = 0;
        ShowItemList();
    }
    public void DisplayInventory(bool close) { inventory.SetActive(!close); }
    public void DisplayOptions() { options.SetActive(!options.activeSelf); }

    public void GetItemList()
    {//This sets up the inventory for use
        Transform itemSpace = inventory.transform.GetChild(2);
        inventorySlots = new GameObject[itemSpace.childCount];
        for (int i = 0; i < itemSpace.childCount; i++)
        {
            inventorySlots[i] = itemSpace.GetChild(i).gameObject;
        }
    }
    void ShowItemList()
    {//This will show the filled inventory slots and the item names
        CharacterSheet sheet = player.characters[player.player].charSheet;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].SetActive(false);
            if (sheet.itemList.Count > i)
            {
                inventorySlots[i].SetActive(true);
                inventorySlots[i].GetComponentInChildren<TMP_Text>().text = ItemScript.GetItem(sheet.itemList[i + startItem]).itemName;
            }
        }
    }

    public void ItemUse(int itemIndex)
    {//This activates the effect of the item in the inventory from the static list in ItemScript
        CharacterSheet sheet = player.characters[player.player].charSheet;
        InventoryEffects.ItemEffect(ItemScript.GetItem(sheet.itemList[itemIndex + startItem]), player);
        //Then the item is removed and the list updated
        player.characters[player.player].charSheet.itemList.RemoveAt(itemIndex + startItem);
        ShowItemList();
    }

    public void ScrollUp()
    {//If the inventory is showing the first item, the list won't scroll up anymore
        if (startItem > 0) { startItem--; ShowItemList(); }
    }
    public void ScrollDown()
    {//The list will scroll down until the last item is shown at the bottom
        if(player.characters[player.player].charSheet.itemList.Count> inventorySlots.Length + startItem) { startItem++; ShowItemList(); }
    }
}
