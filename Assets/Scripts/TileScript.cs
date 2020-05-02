using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*EAS12337350
 * This script will handle the information and options available when a player lands on a tile. Will have options and success rates
 * 
 */

public class TileScript : MonoBehaviour
{
    //Lets the game know that the current character is on this tile
    public bool isCurrentCharacterTile;
    public bool startOrEndTile;

    public Button tileButton;
    //Holds all the tiles this tile is next to
    public List<TileScript> localTiles = new List<TileScript>();

    public void EnterTile() 
    { 
        //All actions that occur when a character lands on the tile
    }
    public void LeaveTile() 
    { 
        //All actions that occur when a character leaves the tile
    }

    public void ShowMoveSpaces()
    {
        //Makes the button on the adjoining tiles interactible
        foreach(TileScript tile in localTiles)
        {
            tile.tileButton.interactable = true;
        }
    }
    public void HideMoveSpaces()
    {
        //Makes the button on the adjoining tiles non-interactible
        foreach (TileScript tile in localTiles)
        {
            tile.tileButton.interactable = false;
        }
    }

    public void FindEventData()
    {
        //This will check the type of tile, then find the appropriate data from gameManager
        //Then this will fill in the correct information and assign the correct functions for outcomes based on the data found
    }
}
