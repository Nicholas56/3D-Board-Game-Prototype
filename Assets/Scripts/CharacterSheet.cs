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
    public Image charVisual;

    public List<AbilityData> abilityList;

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

        abilityList = new List<AbilityData>();
    }

    public CharacterSheet(string defaultName, int powerLvl, int maxHP, int attk, int def, GameObject defaultToken, List<AbilityData> abilities)
    {
        //Constructor for a new character sheet
        characterName = defaultName;
        powerLevel = powerLvl;
        maxHealth = maxHP;
        attack = attk;
        defence = def;
        token = defaultToken;

        abilityList = abilities;
    }
}
