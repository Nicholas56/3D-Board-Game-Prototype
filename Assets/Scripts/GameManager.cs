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
    Camera mainCamera;

    public List<TileScript> mapTiles = new List<TileScript>();

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        FindAllMapTiles(mainCamera);
    }
    public void FindAllMapTiles(Camera eventCamera)
    {
        mapTiles.Clear();
        //This will search for all map tiles when a new map is selected and place them in the correct list
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        for (int i = 0; i < tiles.Length; i++)
        {
            mapTiles.Add(tiles[i].GetComponent<TileScript>());
            tiles[i].GetComponent<TileScript>().LocateLocalTiles();
            tiles[i].GetComponentInChildren<Canvas>().worldCamera = eventCamera;
        }
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
