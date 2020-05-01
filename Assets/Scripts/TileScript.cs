using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*EAS12337350
 * This script will handle the information and options available when a player lands on a tile. Will have options and success rates
 * 
 */

public class TileScript : MonoBehaviour
{
    //Lets the game know that the current character is on this tile
    bool isCurrentCharacterTile;
    public bool startOrEndTile;


    //Holds all the tiles this tile is next to
    public List<TileScript> LocalTiles = new List<TileScript>();

    public void EnterTile() 
    { 
        //All actions that occur when a character lands on the tile
    }
    public void LeaveTile() 
    { 
        //All actions that occur when a character leaves the tile
    }
    public List<TileScript> ReturnLocalTiles()
    {
        //This function returns the list of local tiles without the tile that called it
        //This will check the list and remove the tile that called this function
    }

    public void FindEventData()
    {
        //This will check the type of tile, then find the appropriate data from gameManager
        //Then this will fill in the correct information and assign the correct functions for outcomes based on the data found
    }
}
