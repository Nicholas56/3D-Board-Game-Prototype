using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*EAS12337350
 * This script will handle the player interactions throughout gameplay. 
 * This will handle mouse input for movement, hold player position on map and run the random dice function that is integral to gameplay. 
 * Also, if added will handle multiplayer aspects
 */

public class PlayerTurnScript : MonoBehaviour
{
    //This stores the character data, allows for multiple players
    public List<Character> characters = new List<Character>();

    //When a tile is found, text will appear with description and buttons
    public TMP_Text eventDescriptionText;
    public TMP_Text buttonName1;
    public TMP_Text buttonName2;
    public TMP_Text buttonName3;

    //When the player clicks on a tile, it is stored here
    public TileScript selectedTile;


    public int RollDice()
    {
        //This function will represent the dice roll. This will produce a random number as an outcome
        //The dice roll can be influenced by number of dice, abilities or some other factor. The script will need to accomodate this
    }

    public void ToggleOptionMenu()
    {
        //This opens the option menu or closes it
    }

    public void MoveCharacterCheck()
    {
        //Using the ReturnLocalTiles() in the TileScript checks if the player can move to the selected tile using the value given by RollDice()
    }
}

public struct Character
{
    public GameObject gameToken;
    public CharacterSheet charSheet;
}
