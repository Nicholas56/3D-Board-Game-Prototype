using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*EAS12337350
 * This script will hold various lists of monsters, traps, events and other data that the game requires. 
 * This will offer static function for other classes
 */

public class GameManager : MonoBehaviour
{
    //These two will be combined for a list of all events, to randomly pick from
    public List<TileEventData> positiveEventsData;
    public List<TileEventData> negativeEventsData;

    public List<TileEventData> enemyEventsData;
    public List<TileEventData> trapsEventsData;
    public List<TileEventData> questEventsData;

    public List<TileScript> mapTiles;

    public List<CharacterSheet> savedCharacters;

    public void FindAllMapTiles()
    {
        //This will search for all map tiles when a new map is selected and place them in the correct list
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
