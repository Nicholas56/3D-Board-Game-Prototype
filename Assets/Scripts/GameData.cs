using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*EAS12337350
 * This scriptable object will hold all the data for a level in terms of monsters, traps, events and quests
 */
 [CreateAssetMenu]
public class GameData : ScriptableObject
{
    //These two will be combined for a list of all events, to randomly pick from
    public List<TileEventData> positiveEventsData;
    public List<TileEventData> negativeEventsData;

    public List<TileEventData> enemyEventsData;
    public List<TileEventData> trapsEventsData;
    public List<TileEventData> questEventsData;

    public List<AbilityData> abilitiesData;

    public List<TileScript> mapTiles;

    public List<CharacterSheet> savedCharacters;

    public void FindAllMapTiles()
    {
        //This will search for all map tiles when a new map is selected and place them in the correct list
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        for (int i = 0; i < tiles.Length; i++)
        {
            mapTiles.Add(tiles[i].GetComponent<TileScript>());
            tiles[i].GetComponent<TileScript>().LocateLocalTiles();
        }
    }
}
