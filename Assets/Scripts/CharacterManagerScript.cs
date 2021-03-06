﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*EAS12337350
 * This script will allow for the creation of a new CharacterSheet and handle the character select options 
 */

public class CharacterManagerScript : MonoBehaviour
{
    public List<CharacterSheet> characters;

    public CharacterSheet currentCharacter;

    GameManager manager;
    public int numOfPlayers;
    int currentPlayer = -1;
    public TMP_Text playerCounter;

    int fileSaveNum = -1;

    public GameObject charSelectHolder;
    public GameObject tokenSelectHolder;

    public TMP_InputField charNameInput;

    public TMP_Text charPower;
    public TMP_Text charHealth;
    public TMP_Text charAttack;
    public TMP_Text charDefence;

    public Transform abilityBoxHolder;

    public Image charTokenImage;
    float tokenAnimNum;

    public int currentToken;
    public List<GameObject> tokenObject;
    public List<Sprite> tokenImage;

    private void Start()
    {
        manager = gameObject.GetComponent<GameManager>();
        if (GameManager.playerCharacters.Count == 0)
        {
            CreateNewCharacterSheet();
            GameManager.playerCharacters.Add(currentCharacter);
        }
        else { currentCharacter = GameManager.playerCharacters[0]; }
        DisplayStats();
        tokenAnimNum = Time.time;
        ChooseToken(0);
        manager.LoadCharacters();
        CharacterSelectBoxes();
    }
    public void CreateNewCharacterSheet()
    {//The preset characters will be loaded here //Requires unique abilities and tokens!!
        if (manager.SaveCheck())
        {//The presets are only loaded the first time // Requires review!!
            CharacterSheet presetArcher = new CharacterSheet("Archer", 0, 10, 6, 3, 0, tokenObject[0]);
            currentCharacter = presetArcher; SaveCharacter();
            CharacterSheet presetMage = new CharacterSheet("Mage", 0, 8, 5, 4, 0, tokenObject[0]);
            currentCharacter = presetMage; SaveCharacter();
            List<int> list = new List<int>();
            list.Add(300);
            CharacterSheet presetStealth = new CharacterSheet("Stealth", 0, 9, 8, 2, 0, tokenObject[0], list);
            Debug.Log(presetStealth.abilityList[0]);
            currentCharacter = presetStealth; SaveCharacter();
            CharacterSheet presetWarrior = new CharacterSheet("Warrior", 0, 12, 4, 6, 0, tokenObject[0]);
            currentCharacter = presetWarrior; SaveCharacter();
        }
        //Uses the CharacterSheet constructor to make a new character
        CharacterSheet newCharacter = new CharacterSheet("CharName", 0, 10, 4, 4, 0, tokenObject[0]);
        currentCharacter = newCharacter;
        DisplayStats();
    }

    void CharacterSelectBoxes()
    {
        for (int i = 0; i < manager.savedCharacters.Count; i++)
        {//This will change the name of the button to the name of the save file
            charSelectHolder.transform.GetChild(i).gameObject.SetActive(true);
            charSelectHolder.transform.GetChild(i).gameObject.GetComponentInChildren<TMP_Text>().text = manager.savedCharacters[i].characterName;
        }
        for (int i = manager.savedCharacters.Count; i < charSelectHolder.transform.childCount; i++)
        {//This will set all boxes not being used to inactive
            charSelectHolder.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void SwitchPlayers()
    {
        Debug.Log(GameManager.playerCharacters.Count);
        //This allows several players to choose their characterSheet
        if (currentPlayer == -1)
        {//This runs the first time this button is clicked
            GameManager.playerCharacters.Clear();
            for (int i = 0; i < numOfPlayers; i++)
            {//Lets the first player be the current character, but creates new characters for later players
                if (i < 0) { GameManager.playerCharacters.Add(new CharacterSheet("CharName", 0, 10, 4, 4, 0, tokenObject[0])); }
                GameManager.playerCharacters.Add(currentCharacter);
            }
            currentPlayer++;
        }
        //Cycles through player numbers
        GameManager.playerCharacters[currentPlayer] = currentCharacter;
        currentPlayer = (currentPlayer + 1) % numOfPlayers;
        currentCharacter = GameManager.playerCharacters[currentPlayer];
        DisplayStats();
        playerCounter.text = "" + (currentPlayer + 1);
    }

    public void DisplayCharacterSheet(int saveFile)
    {
        //When the character is selected from the list, the corresponding character sheet will be displayed
        CharSave sheet = manager.savedCharacters[saveFile];

        if (GameManager.playerCharacters.Count > 1)
        {
            for (int i = 0; i < GameManager.playerCharacters.Count; i++)
            {//This check runs for all but the current player
                if (i != currentPlayer)
                {//This checks the name of the load file against all other player names, if the same, the load fails
                    if (sheet.characterName == GameManager.playerCharacters[i].characterName) { return; }
                }
            }
        }
        if (sheet.abilityIndex.Count > 0)
        {
            List<int> abilities = new List<int>(sheet.abilityIndex);
            currentCharacter = new CharacterSheet(sheet.characterName, sheet.charLevel, sheet.charMaxHealth, sheet.charAttack, sheet.charDefence, sheet.charPenalty, tokenObject[sheet.tokenIndex], abilities);
        }
        else { currentCharacter = new CharacterSheet(sheet.characterName, sheet.charLevel, sheet.charMaxHealth, sheet.charAttack, sheet.charDefence, sheet.charPenalty, tokenObject[sheet.tokenIndex]); }
        currentCharacter.charVisual = tokenImage[sheet.tokenIndex];

        DisplayStats();
        if (currentPlayer == -1) { GameManager.playerCharacters[0] = currentCharacter; }
        else//The player becomes the playable character, though if multiplayer is enabled, the current player is used
        {
            GameManager.playerCharacters[currentPlayer] = currentCharacter;
        }
        charTokenImage.sprite = tokenImage[sheet.tokenIndex];
    }

    public void DisplayStats()
    {
        int level;
        //This modifies the stats if level is above 1
        level = Mathf.CeilToInt(currentCharacter.powerLevel / 50);
        charNameInput.text = currentCharacter.characterName;
        charPower.text = "" + currentCharacter.powerLevel;
        charHealth.text = "" + (currentCharacter.maxHealth + (5 * level));
        charAttack.text = "" + (currentCharacter.attack + (2 * level));
        charDefence.text = "" + (currentCharacter.defence + (2 * level));

        for (int i = 0; i < abilityBoxHolder.childCount; i++)
        {
            abilityBoxHolder.GetChild(i).gameObject.SetActive(false);
            if (currentCharacter.abilityList.Count > i)
            {
                abilityBoxHolder.GetChild(i).gameObject.SetActive(true);
                abilityBoxHolder.GetChild(i).GetComponentInChildren<TMP_Text>().text = AbilityScript.GetAbility(currentCharacter.abilityList[i]).abilityName;
            }
        }
    }

    public void ShowTokenSelection()
    {
        tokenSelectHolder.GetComponent<Animator>().SetBool("Menu", true);
        GetComponent<GameManager>().SoundEffect(2);
    }

    public void ChooseToken(int tokenIndex)
    {
        //The selected token is chosen as the current character sheet's token
        if (tokenObject[tokenIndex] != null)
        {
            currentCharacter.token = tokenObject[tokenIndex];
            charTokenImage.sprite = tokenImage[tokenIndex];
            currentCharacter.charVisual = tokenImage[tokenIndex];
            currentToken = tokenIndex;
        }
        //This allows the start code to run, without the animator component being active
        if (Time.time>tokenAnimNum+ 3)
        {
            //This closes the menu, if it is open to begin with
            tokenSelectHolder.GetComponent<Animator>().SetBool("Menu", false);
            GetComponent<GameManager>().SoundEffect(2);
        }
    }

    public void ChangeName()
    {
        currentCharacter.characterName = charNameInput.text;
    }

    bool NameCheck(string nameToCheck)
    {//This will compare the name in the charSelectHolder to the one given, and return true or false
        if (manager.savedCharacters.Count > 0)
        {
            for (int i = 0; i < charSelectHolder.transform.childCount; i++)
            {
                string compareName = charSelectHolder.transform.GetChild(i).gameObject.GetComponentInChildren<TMP_Text>().text;
                if (compareName == nameToCheck)
                {
                    fileSaveNum = i;
                    return true;
                }
            }
        }
        return false;
    }

    public void SaveCharacter()
    {
        //Changes the data to save format and calls the save function from game manager
        CharSave save = new CharSave(currentCharacter.characterName, currentCharacter.powerLevel, currentCharacter.maxHealth, 
            currentCharacter.attack, currentCharacter.defence, currentCharacter.returnPenalty, currentToken,currentCharacter.itemList, currentCharacter.abilityList, currentCharacter.moveVar,currentCharacter.rollFix);

        if (NameCheck(currentCharacter.characterName)) { }
        else { fileSaveNum = manager.savedCharacters.Count; }
        //This creates a new save, reloads the saved files and diplays the buttons for the saved files
        manager.SaveCharacter(save, fileSaveNum);

        CharacterSelectBoxes();
    }

    public void DeleteCharacter()
    {
        //This checks that the current character is saved, then tells the manager to delete it
        if (NameCheck(currentCharacter.characterName))
        {
            Debug.Log(manager.savedCharacters.Count);
            manager.DeleteCharacter(fileSaveNum);
            CharacterSelectBoxes();
        }
    }

    public void ChangeNumOfPlayers(Transform toggle)
    {//This changes the num of players by using the toggle's position in the toggle group
        numOfPlayers = toggle.GetSiblingIndex() +1;
    }
}

public class CharSave
{
    public int saveFile;

    public string characterName;

    public int charLevel;

    public int charMaxHealth;
    public int charAttack;
    public int charDefence;

    public int charPenalty;

    public int tokenIndex;

    public List<int> itemList;
    public List<int> abilityIndex;

    public int moveVar;
    public int rollFix;

    public CharSave(string charName, int charLvl, int health, int attk, int def,int penalty, int token, List<int>  items, List<int> abilities, int extraMove, int diceFix)
    {
        characterName = charName;
        charLevel = charLvl;
        charMaxHealth = health;
        charAttack = attk;
        charDefence = def;
        charPenalty = penalty;
        tokenIndex = token;
        itemList = items;
        abilityIndex = abilities;
        moveVar = extraMove;
        rollFix = diceFix;
    }
}
