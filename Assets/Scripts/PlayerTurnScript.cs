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
    public int player=0;
    public float tokenSpeed = 2f;

    [SerializeField]
    int spacesToMove = 0;

    List<TileScript> tilesMovedOverInTurn = new List<TileScript>();

    private void FixedUpdate()
    {
        if(Vector3.Distance(characters[player].gameToken.transform.position, characters[player].currentTile.transform.position) > 0.1f)
        {
            float step = tokenSpeed * Time.deltaTime;
            characters[player].gameToken.transform.position = Vector3.MoveTowards(characters[player].previousTiles.Peek().transform.position, characters[player].currentTile.transform.position,step);
        }
    }
    public void RollDice(int numOfDice,int addedValueToDice)
    {
        //This function will represent the dice roll. This will produce a random number as an outcome
        //The dice roll can be influenced by number of dice, abilities or some other factor. The script will need to accomodate this
        int roll = Random.Range(1*numOfDice, 7*numOfDice);
        spacesToMove = roll + addedValueToDice;
    }
    public void RollDice()
    {
        int roll = Random.Range(1, 7);
        spacesToMove = roll;
    }

    public void ToggleOptionMenu()
    {
        //This opens the option menu or closes it
    }

    public void ResetTurn()
    {
        spacesToMove = 0;
        tilesMovedOverInTurn.Clear();
        //Resets preveiously traversed tiles
        characters[player].previousTiles.Clear();
        //Cycles through player numbers
        player = (player + 1) % characters.Count;
    }

    public void MoveCharacter(TileScript nextTile)
    {
        //The last tile will be exited and the new one will become the current one.
        characters[player].currentTile.LeaveTile();
        characters[player].currentTile.HideMoveSpaces();
        //The last tile will be added to the previous tiles, unless the token is going backwards
        if (characters[player].previousTiles.Contains(nextTile)) 
        {
            characters[player].previousTiles.Pop();
            //Add a space, as the player is moving backwards
            spacesToMove++;
        }
        else
        {
            //Remove a space
            characters[player].previousTiles.Push(characters[player].currentTile);
            spacesToMove--;
        }
        characters[player].currentTile = nextTile;
        WaitForMove(2f);
    }

    IEnumerator WaitForMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        characters[player].currentTile.EnterTile();
        //Once you run out of spaces to move, the moving phase of the turn is over
        if (spacesToMove >= 0)
        {
            characters[player].currentTile.ShowMoveSpaces();
        }
    }
}

public class Character
{
    public GameObject gameToken;
    public CharacterSheet charSheet;
    public TileScript currentTile;
    public Stack<TileScript> previousTiles = new Stack<TileScript>();
}
