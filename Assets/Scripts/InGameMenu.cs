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

    public GameObject helpBoxes;
    public bool help;

    public GameObject returnOption;

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
        inventory.SetActive(!inventory.activeSelf); options.SetActive(false);
        startItem = 0;
        ShowItemList();
    }
    public void DisplayInventory(bool close) { inventory.SetActive(!close); options.SetActive(false); }
    public void DisplayOptions() { options.SetActive(!options.activeSelf); inventory.SetActive(false); }
    public void DisplayOptions(bool close) { options.SetActive(!close); inventory.SetActive(false); }

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
    public void Help()
    {
        help = !help;
        helpBoxes.SetActive(help);
        if (help == true)
        {//This will only start to wait if the boxes are visible
            StartCoroutine(HideHelp());
        }
        else { StopAllCoroutines(); }
    }

    IEnumerator HideHelp()
    {//This will wait 5 seconds before hiding the help boxes
        yield return new WaitForSeconds(8);
        if (help == true)
        {
            Help();
        }
    }

    public void DisplayReturnOption()
    {
        returnOption.SetActive(!returnOption.activeSelf);
        if (help) { help = false;helpBoxes.SetActive(help); }
        if (returnOption.activeSelf)
        {
            returnOption.transform.GetChild(0).GetComponent<TMP_Text>().text = "Are you sure you wish to return to the main menu? Doing so will cost you "
                + player.characters[player.player].charSheet.returnPenalty + " power points. This increases every time you use this option.";
        }
    }

    public void ReturnToMenu()
    {
        player.characters[player.player].charSheet.powerLevel -= player.characters[player.player].charSheet.returnPenalty;
        //this limits the power level to zero
        if (player.characters[player.player].charSheet.powerLevel <= 0) { player.characters[player.player].charSheet.powerLevel = 0; }
        player.characters[player.player].charSheet.returnPenalty += 10;
        LevelSelectScript.ReturnToMainMenu();
    }


}
