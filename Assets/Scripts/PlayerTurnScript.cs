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

    public TileScript startingTile;

    public bool isEvent = false;
    TileEventData tileEvent = null;
    //When a tile is found, text will appear with description and buttons
    public GameObject eventPanel;

    //When the player clicks on a tile, it is stored here
    public int player=0;
    public float tokenSpeed = 2f;
    float tokenStep;

    public int spacesToMove = 0;
    public TMP_Text movesToGo;
    public TMP_Text playerHealth;

    public List<int> itemList = new List<int>();

    List<TileScript> tilesMovedOverInTurn = new List<TileScript>();

    private void Start()
    {
        //Beginning of the game, all characters are loaded and the tokens placed
        for (int i = 0; i < GameManager.playerCharacters.Count; i++)
        {
            GameObject token = Instantiate(GameManager.playerCharacters[i].token, startingTile.transform.position, Quaternion.identity);
            Character character = new Character(token, GameManager.playerCharacters[i], startingTile);
            character.health = GameManager.playerCharacters[i].maxHealth;
            characters.Add(character);
        }
        UpdatePlayerHealth();
        itemList = new List<int>(FindObjectOfType<GameManager>().levelData.itemData);
    }
    private void FixedUpdate()
    {
        if(Vector3.Distance(characters[player].gameToken.transform.position, characters[player].currentTile.transform.position) > 0.1f)
        {
            tokenStep += Time.fixedDeltaTime * tokenSpeed;
            //Moves from last tile to current tile using speed to calculate step per second
            characters[player].gameToken.transform.position = Vector3.MoveTowards(characters[player].lastTile.transform.position, characters[player].currentTile.transform.position,tokenStep);
        }
    }

    public void RollDice()
    {
        //This function will represent the dice roll. This will produce a random number as an outcome
        //The dice roll can be influenced by number of dice, abilities or some other factor. The script will need to accomodate this!!
        int roll = Random.Range(1, 7);
        spacesToMove = roll;
        UpdateMovesToGo();
    }

    public void ToggleOptionMenu()
    {
        //This opens the option menu or closes it
    }

    public TileEventData GetTileData()
    {
        return tileEvent;
    }

    public void UpdateMovesToGo()
    {
        movesToGo.text = "" + spacesToMove;
    }

    public void UpdatePlayerHealth()
    {
        playerHealth.text = "" + characters[player].health + "/" + characters[player].charSheet.maxHealth;
    }

    public void ResetTurn()
    {
        eventPanel.SetActive(false);
        spacesToMove = 0;
        UpdateMovesToGo();
        tilesMovedOverInTurn.Clear();
        //Provides check for temporary stats
        if (characters[player].tempCounter > 0) { characters[player].tempCounter--; }
        TempStatCheck();
        //Resets preveiously traversed tiles
        characters[player].previousTiles.Clear();
        //Cycles through player numbers
        player = (player + 1) % characters.Count;
        characters[player].currentTile.ShowMoveSpaces();
        FindObjectOfType<CameraScript>().ResetToken();
        tileEvent = null;
        UpdatePlayerHealth();
    }

    public void MoveCharacter(TileScript nextTile)
    {
        if (spacesToMove > 0)
        {//If the inventory is open, it will be closed
            gameObject.GetComponent<InGameMenu>().DisplayInventory(true);
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
            UpdateMovesToGo();
            //Stores the previous tile as the last tile for movement purposes
            characters[player].lastTile = characters[player].currentTile;
            characters[player].currentTile = nextTile;
            //Resets the step counter for the token move function, allowing it to begin moving again
            tokenStep = 0;
            StartCoroutine(WaitForMove(2f));
        }
    }

    IEnumerator WaitForMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        characters[player].currentTile.EnterTile();
        //Once you run out of spaces to move, the moving phase of the turn is over
        if (spacesToMove > 0)
        {
            characters[player].currentTile.ShowMoveSpaces();
        }
        else 
        {
            if (!characters[player].currentTile.endTile)
            {
                //The move phase is over and the event phase begins
                StartEvent();
            }
        }
    }

    public void StartEvent()
    {
        tileEvent = characters[player].currentTile.LandOnTile();
        eventPanel.SetActive(true);
        isEvent = true;
    }

    public void AddItem()
    {//This adds one of the level items to the players inventory and removes the item from the level instance
        Debug.Log("Length of itemList is " + itemList.Count);
        int rand = Random.Range(0, itemList.Count);
        characters[player].charSheet.itemList.Add(itemList[rand]);
        itemList.Remove(itemList[rand]);
        //This removes the previous tiles list from player
        characters[player].previousTiles.Clear();
    }

    public void TeleportPlayer(int teleportNum)
    {
        GameManager data = gameObject.GetComponent<GameManager>();
        TileScript destinationTile = data.mapTiles[teleportNum];
        Debug.Log("Tile to go to: " + destinationTile + " Data: " + data.mapTiles[3]);
        characters[player].currentTile = destinationTile;
        characters[player].lastTile = destinationTile;
        characters[player].previousTiles.Clear();
    }

    void TempStatCheck()
    {//If the temp counter drops to 0, the temp stats are removed
        if (characters[player].tempCounter < 1)
        {
            characters[player].tempHealth = 0;
            characters[player].tempAttack = 0;
            characters[player].tempDefence = 0;
        }
    }
}

public class Character
{
    public GameObject gameToken;
    public CharacterSheet charSheet;
    public TileScript currentTile;
    public TileScript lastTile;
    public Stack<TileScript> previousTiles = new Stack<TileScript>();

    public int health;
    //Temporary stats
    public int tempCounter;

    public int tempHealth;
    public int tempAttack;
    public int tempDefence;

    public Character(GameObject charToken, CharacterSheet sheet, TileScript startTile)
    {
        gameToken = charToken;
        charSheet = sheet;
        currentTile = startTile;
        lastTile = startTile;
        currentTile.ShowMoveSpaces();
    }
}
