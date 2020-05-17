using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*EAS12337350
 * This script will hold all data relating to the current character, including stats, levels and abilities. 
 * This information will be saved using the character manager.
 */

public class CharacterSheet 
{
    public string characterName;

    public int powerLevel;

    public int maxHealth;
    public int attack;
    public int defence;

    public GameObject token;
    public Sprite charVisual;

    public List<int> itemList;
    public List<int> abilityList;
    //Ability base stats
    public int moveVar;
    public int rollFix;

    //Other stats as later decided

    public CharacterSheet(string defaultName, int powerLvl, int maxHP, int attk, int def, GameObject defaultToken)
    {
        //Constructor for a new character sheet
        characterName = defaultName;
        powerLevel = powerLvl;
        maxHealth = maxHP;
        attack = attk;
        defence = def;
        token = defaultToken;

        itemList = new List<int>();
        abilityList = new List<int>();

    }

    public CharacterSheet(string defaultName, int powerLvl, int maxHP, int attk, int def, GameObject defaultToken, List<int> abilities)
    {
        //Constructor for a new character sheet
        characterName = defaultName;
        powerLevel = powerLvl;
        maxHealth = maxHP;
        attack = attk;
        defence = def;
        token = defaultToken;

        itemList = new List<int>();
        abilityList = abilities;
    }
}
