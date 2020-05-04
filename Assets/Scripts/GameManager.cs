using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*EAS12337350
 * This script will handle save data 
 * This will offer static function for other classes
 */

public class GameManager : MonoBehaviour
{
    public List<CharacterSheet> savedCharacters;
    public static List<CharacterSheet> playerCharacters = new List<CharacterSheet>();

    //Each level will have a specific data file with all the pertinent data
    public GameData levelData;

    private void Awake()
    {
        levelData.FindAllMapTiles();
    }
    public void LoadCharacters()
    {
        //This will find the saved JSON list of characters and store them here for use
    }

    public void SaveCharacters()
    {
        //This will store the list of characters as a string and create a JSON save file
    }

    public void DeleteCharacter()
    {
        //This will remove the current character from the save list, which will therefore not be saved
    }
}
