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

    [Tooltip("This is a button to allow the player to select previously saved character sheets")]
    public GameObject charSelectButton;
    public GameObject charSelectHolder;

    public GameObject charSheetPanel;

    public TMP_InputField charNameInput;

    public TMP_Text charPower;
    public TMP_Text charHealth;
    public TMP_Text charAttack;
    public TMP_Text charDefence;

    public Image charTokenImage;
    public GameObject defaultTokenObject;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        CreateNewCharacterSheet();
        DisplayStats();
        GameManager.playerCharacters.Add(currentCharacter);
        Debug.Log(GameManager.playerCharacters[0]);
    }
    public void CreateNewCharacterSheet()
    {
        //Uses the CharacterSheet constructor to make a new character
        CharacterSheet newCharacter = new CharacterSheet("charName", 0, 10, 4, 4, defaultTokenObject);
        currentCharacter = newCharacter;
        Debug.Log(currentCharacter);
    }

    public void DisplayCharacterSheet()
    {
        //When the character is selected from the list, the corresponding character sheet will be displayed

    }

    public void DisplayStats()
    {
        charPower.text = "" + currentCharacter.powerLevel;
        charHealth.text = "" + currentCharacter.maxHealth;
        charAttack.text = "" + currentCharacter.attack;
        charDefence.text = "" + currentCharacter.defence;
    }

    public void ChooseToken(GameObject tokenPrefab)
    {
        //The selected token is chosen as the current character sheet's token
        currentCharacter.token = tokenPrefab;
    }

    public void ChangeName()
    {
        currentCharacter.characterName = charNameInput.text;
    }
}
