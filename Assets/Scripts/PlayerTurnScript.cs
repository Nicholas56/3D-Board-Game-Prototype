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
    public bool isRoll;
    public int rollAddedValue;
    public int rollFixValue;
    float rollTimer;
    int roll;

    public TMP_Text playerHealth;

    public List<int> itemList = new List<int>();
    public List<int> abilityList = new List<int>();
    

    List<TileScript> tilesMovedOverInTurn = new List<TileScript>();

    private void Start()
    {
        //Beginning of the game, all characters are loaded and the tokens placed
        for (int i = 0; i < GameManager.playerCharacters.Count; i++)
        {
            GameObject token = Instantiate(GameManager.playerCharacters[i].token, startingTile.transform.position, Quaternion.identity);
            Character character = new Character(token, GameManager.playerCharacters[i], startingTile);
            character.health = GameManager.playerCharacters[i].maxHealth;
            if (character.charSheet.abilityList.Contains(300)) { character.trapDodge = true; }
            if (character.charSheet.abilityList.Contains(600)) { character.teleport = true; }
            characters.Add(character);
        }
        UpdatePlayerHealth();
        itemList = new List<int>(FindObjectOfType<GameManager>().levelData.itemData);
        abilityList = new List<int>(FindObjectOfType<GameManager>().levelData.abilitiesData);
    }
    private void FixedUpdate()
    {
        if(Vector3.Distance(characters[player].gameToken.transform.position, characters[player].currentTile.transform.position) > 0.1f)
        {
            tokenStep += Time.fixedDeltaTime * tokenSpeed;
            //Moves from last tile to current tile using speed to calculate step per second
            characters[player].gameToken.transform.position = Vector3.MoveTowards(characters[player].lastTile.transform.position, characters[player].currentTile.transform.position,tokenStep);
        }
        if (isRoll && rollTimer<Time.time)
        {
            RollDice();
            rollTimer = Time.time + 0.1f;
        }
    }

    public void RollDice()
    {
        //This function will represent the dice roll. This will produce a random number as an outcome
        //The dice roll can be influenced by number of dice, abilities or some other factor. The script will need to accomodate this!!
        //int roll = Random.Range(1 + rollAddedValue, 7 + rollAddedValue);
        roll = (roll+1) % 6+rollAddedValue;
        spacesToMove = roll+1;
        UpdateMovesToGo();
    }

    public void BeginRoll() 
    {
        isRoll = true;
        if (characters[player].charSheet.moveVar != 0) { rollAddedValue = characters[player].charSheet.moveVar; } else { rollAddedValue = 0; }
        if (characters[player].charSheet.rollFix != 0) { rollFixValue = characters[player].charSheet.rollFix; } else { rollFixValue = 0; }
    }
    public void StopRoll() 
    { 
        isRoll = false;
        //If the dice roll has been fixed, the number is set
        if (rollFixValue > 0) { spacesToMove = rollFixValue; }
        UpdateMovesToGo();
    }

    public void RollToggle()
    {
        if (isRoll)
        {
            StopRoll();
        }
        else { BeginRoll(); }
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
        if (characters[player].tempHealth > 0)
        {
            playerHealth.text = "" + characters[player].health + "(" + characters[player].tempHealth + ")/" + characters[player].MaxHealth;
        }
        else
        {
            playerHealth.text = "" + characters[player].health + "/" + characters[player].MaxHealth;
        }
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
        //Random Teleportation
        if (characters[player].teleport) { TeleportPlayer
                (Random.Range(0, FindObjectOfType<GameManager>().mapTiles.Count)); }
        characters[player].currentTile.ShowMoveSpaces();
        FindObjectOfType<CameraScript>().ResetToken();
        tileEvent = null;
        UpdatePlayerHealth();
    }

    public void MoveCharacter(TileScript nextTile)
    {
        if (spacesToMove > 0  && !isRoll)
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
        int rand = Random.Range(0, itemList.Count);
        characters[player].charSheet.itemList.Add(itemList[rand]);
        itemList.Remove(itemList[rand]);
        //This removes the previous tiles list from player
        characters[player].previousTiles.Clear();
    }

    public void AddAbility(int abilityID)
    {//There is a limit of five abilities per character
        if (characters[player].charSheet.abilityList.Count < 5)
        {
            //This can be called by item, or by event and adds the ability id to the characters list of id's
            characters[player].charSheet.abilityList.Add(abilityID);
            //This calls the static class, ability effects, using the static list of abilities in AbilityScript
            AbilityEffects.AbilityEffect(AbilityScript.GetAbility(abilityID), this);
            UpdatePlayerHealth();
        }
    }


    public void TeleportPlayer(int teleportNum)
    {
        GameManager data = FindObjectOfType<GameManager>();
        if (teleportNum < data.mapTiles.Count)
        {
            TileScript destinationTile = data.mapTiles[teleportNum];
            characters[player].currentTile = destinationTile;
            characters[player].lastTile = destinationTile;
            //the effects of entering a tile still occur
            characters[player].currentTile.EnterTile();
        }
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

    public int Power { 
        get 
        { 
            if (Mathf.CeilToInt(charSheet.powerLevel / 100) < 1) 
            { 
                return 1; 
            } 
            else 
            { 
                return Mathf.CeilToInt(charSheet.powerLevel / 100); 
            } 
        } 
    }
    public int MaxHealth { get { return charSheet.maxHealth * Power; } }
    public int Attack { get { return charSheet.attack * Power; } }
    public int Defence { get { return charSheet.defence * Power; } }

    public int health;
    //Temporary stats
    public int tempCounter;

    public int tempHealth;
    public int tempAttack;
    public int tempDefence;

    //Ability traits
    public bool trapDodge;
    public bool teleport;

    public Character(GameObject charToken, CharacterSheet sheet, TileScript startTile)
    {
        gameToken = charToken;
        charSheet = sheet;
        currentTile = startTile;
        lastTile = startTile;
        currentTile.ShowMoveSpaces();
    }
}
