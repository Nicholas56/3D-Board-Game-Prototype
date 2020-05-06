using System.Collections;
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
    int currentPlayer=-1;
    public TMP_Text playerCounter;

    int fileSaveNum = -1;

    [Tooltip("This is a button to allow the player to select previously saved character sheets")]
    public GameObject charSelectButton;
    public GameObject charSelectHolder;

    public GameObject charSheetPanel;

    public TMP_InputField charNameInput;

    public TMP_Text charPower;
    public TMP_Text charHealth;
    public TMP_Text charAttack;
    public TMP_Text charDefence;

    public Transform abilityBoxHolder;
    public GameObject abilityBox;

    public Image charTokenImage;

    public int currentToken;
    public List<GameObject> tokenObject;
    public List<Image> tokenImage;

    private void Start()
    {
        manager = gameObject.GetComponent<GameManager>();
        CreateNewCharacterSheet();
        GameManager.playerCharacters.Add(currentCharacter);
        DisplayStats();
        manager.LoadCharacters();
        CharacterSelectBoxes();
    }
    public void CreateNewCharacterSheet()
    {//The preset characters will be loaded here //Requires unique abilities and tokens!!
        if (manager.SaveCheck())
        {//The presets are only loaded the first time // Requires review!!
            CharacterSheet presetArcher = new CharacterSheet("Archer", 0, 10, 6, 3, tokenObject[0]);
            currentCharacter = presetArcher; SaveCharacter();
            CharacterSheet presetMage = new CharacterSheet("Mage", 0, 8, 5, 4, tokenObject[0]);
            currentCharacter = presetMage; SaveCharacter();
            CharacterSheet presetStealth = new CharacterSheet("Stealth", 0, 9, 8, 2, tokenObject[0]);
            currentCharacter = presetStealth; SaveCharacter();
            CharacterSheet presetWarrior = new CharacterSheet("Warrior", 0, 12, 4, 6, tokenObject[0]);
            currentCharacter = presetWarrior; SaveCharacter();
        }
        //Uses the CharacterSheet constructor to make a new character
        CharacterSheet newCharacter = new CharacterSheet("CharName", 0, 10, 4, 4, tokenObject[0]);
        currentCharacter = newCharacter;
    }

    void CharacterSelectBoxes()
    {
        for (int i = 0; i < manager.savedCharacters.Count; i++)
        {//This will change the name of the button to the name of the save file
            charSelectHolder.transform.GetChild(i).gameObject.SetActive(true);
            charSelectHolder.transform.GetChild(i).gameObject.GetComponentInChildren<TMP_Text>().text = manager.savedCharacters[i].characterName;
        }
        for(int i = manager.savedCharacters.Count; i < charSelectHolder.transform.childCount; i++)
        {//This will set all boxes not being used to inactive
            charSelectHolder.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void SwitchPlayers()
    {
        Debug.Log(GameManager.playerCharacters.Count);
        //This allows several players to choose their characterSheet
        if(currentPlayer == -1)
        {//This runs the first time this button is clicked
            GameManager.playerCharacters.Clear();
            for (int i = 0; i < numOfPlayers; i++)
            {//Lets the first player be the current character, but creates new characters for later players
                if (i < 0) {GameManager.playerCharacters.Add( new CharacterSheet("CharName", 0, 10, 4, 4, tokenObject[0])); }
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
        Debug.Log(GameManager.playerCharacters.Count);
    }

    public void DisplayCharacterSheet(int saveFile)
    {
        //When the character is selected from the list, the corresponding character sheet will be displayed
        CharSave sheet = manager.savedCharacters[saveFile];
        if (sheet.abilityIndex.Count > 0)
        {
            List<AbilityData> abilities = new List<AbilityData>();
            for (int i = 0; i < sheet.abilityIndex.Count; i++)
            {
                //This matches the index to the master list of abilities in game manager
                abilities.Add(manager.levelData.abilitiesData[sheet.abilityIndex[i]]);
            }
            currentCharacter = new CharacterSheet(sheet.characterName, sheet.charLevel, sheet.charMaxHealth, sheet.charAttack, sheet.charDefence, tokenObject[sheet.tokenIndex], abilities);
        }
        else { currentCharacter = new CharacterSheet(sheet.characterName, sheet.charLevel, sheet.charMaxHealth, sheet.charAttack, sheet.charDefence, tokenObject[sheet.tokenIndex]); }
        currentCharacter.charVisual = tokenImage[sheet.tokenIndex];

        DisplayStats();
        if (currentPlayer == -1) { GameManager.playerCharacters[0] = currentCharacter; }
        else//The player becomes the playable character, though if multiplayer is enabled, the current player is used
        {
            GameManager.playerCharacters[currentPlayer] = currentCharacter;
        }
        charTokenImage = tokenImage[sheet.tokenIndex];
    }

    public void DisplayStats()
    {
        charNameInput.text = currentCharacter.characterName;
        charPower.text = "" + currentCharacter.powerLevel;
        charHealth.text = "" + currentCharacter.maxHealth;
        charAttack.text = "" + currentCharacter.attack;
        charDefence.text = "" + currentCharacter.defence;

        if (currentCharacter.abilityList.Count > 0)
        {
            for (int i = 0; i < currentCharacter.abilityList.Count; i++)
            {
                GameObject box = Instantiate(abilityBox, abilityBoxHolder);
                box.GetComponentInChildren<TMP_Text>().text = currentCharacter.abilityList[i].abilityName;
            }
        }
    }

    public void ChooseToken(int tokenIndex)
    {
        //The selected token is chosen as the current character sheet's token
        currentCharacter.token = tokenObject[tokenIndex];
        charTokenImage = tokenImage[tokenIndex];
        currentCharacter.charVisual = tokenImage[tokenIndex];
        currentToken = tokenIndex;
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
        List<int> index = new List<int>();
        //Changes the data to save format and calls the save function from game manager
        if (currentCharacter.abilityList.Count > 0)
        {//If the character has abilities, this is compared to the master data list of abilities and the index is saved
            for (int i = 0; i < currentCharacter.abilityList.Count; i++)
            {
                if (manager.levelData.abilitiesData.Contains(currentCharacter.abilityList[i]))
                {
                    index[i] = manager.levelData.abilitiesData.IndexOf(currentCharacter.abilityList[i]);
                }
            }
        }
        CharSave save = new CharSave(currentCharacter.characterName, currentCharacter.powerLevel, currentCharacter.maxHealth, currentCharacter.attack, currentCharacter.defence, currentToken, index);

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
}

public class CharSave
{
    public int saveFile;

    public string characterName;

    public int charLevel;

    public int charMaxHealth;
    public int charAttack;
    public int charDefence;

    public int tokenIndex;

    public List<int> abilityIndex;

    public CharSave(string charName, int charLvl, int health, int attk, int def, int token, List<int> abilities)
    {
        characterName = charName;
        charLevel = charLvl;
        charMaxHealth = health;
        charAttack = attk;
        charDefence = def;
        tokenIndex = token;
        abilityIndex = abilities;
    }
}
