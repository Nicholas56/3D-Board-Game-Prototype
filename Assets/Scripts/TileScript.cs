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
    public enum eventType { None, Enemy, Trap, PositiveEvent, NegativeEvent, RandomEvent, Random, Quest }
    public eventType tileType;

    public bool startOrEndTile;

    GameData manager;
    public Button tileButton;
    //The radius of the locate local tiles function
    [SerializeField][Tooltip("The radius of the locate local tiles function")]
    int localArea = 3;
    //Holds all the tiles this tile is next to
    public List<TileScript> localTiles = new List<TileScript>();

    private void Start()
    {
        tileButton = transform.GetChild(0).GetComponentInChildren<Button>();
        manager = FindObjectOfType<GameData>();
        //The visual appearance of the tile will change depending on the type of tile
        switch (tileType)
        {
            case eventType.None:
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.grey);
                break;
            case eventType.Enemy:
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                break;
            case eventType.Trap:
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.yellow);
                break;
            case eventType.PositiveEvent:
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
                break;
            case eventType.NegativeEvent:
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.black);
                break;
            case eventType.RandomEvent:
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
                break;
            case eventType.Random:
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                break;
            case eventType.Quest:
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.cyan);
                break;
        }
    }

    public void EnterTile() 
    { 
        //All actions that occur when a character lands on the tile
    }
    public void LeaveTile() 
    { 
        //All actions that occur when a character leaves the tile
    }
    public TileEventData LandOnTile()
    {
        //This will check the type of tile, then find the appropriate data from gameManager
        //Then this will fill in the correct information and assign the correct functions for outcomes based on the data found.//This may be changed if events are level specific!!
        switch (tileType)
        {
            case eventType.None:
                return null;
            case eventType.Enemy:
                int rand = Random.Range(0, manager.enemyEventsData.Count);
                return manager.enemyEventsData[rand];
            case eventType.Trap:
                int rand2 = Random.Range(0, manager.trapsEventsData.Count);
                return manager.trapsEventsData[rand2];
            case eventType.PositiveEvent:
                int rand3 = Random.Range(0, manager.positiveEventsData.Count);
                return manager.positiveEventsData[rand3];
            case eventType.NegativeEvent:
                int rand4 = Random.Range(0, manager.negativeEventsData.Count);
                return manager.negativeEventsData[rand4];
            case eventType.RandomEvent:
                List<TileEventData> combinedEventList = new List<TileEventData>();
                combinedEventList.AddRange(manager.positiveEventsData);
                combinedEventList.AddRange(manager.negativeEventsData);
                int rand5 = Random.Range(0, combinedEventList.Count);
                return combinedEventList[rand5];
            case eventType.Random:
                List<TileEventData> combinedEventList2 = new List<TileEventData>();
                combinedEventList2.AddRange(manager.positiveEventsData);
                combinedEventList2.AddRange(manager.negativeEventsData);
                combinedEventList2.AddRange(manager.enemyEventsData);
                combinedEventList2.AddRange(manager.trapsEventsData);
                int rand7 = Random.Range(0, combinedEventList2.Count);
                return combinedEventList2[rand7];
            case eventType.Quest:
                int rand6 = Random.Range(0, manager.questEventsData.Count);
                return manager.questEventsData[rand6];
        }
        return null;
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
    

    public void LocateLocalTiles()
    {
        localTiles.Clear();
        //This function is used in the editor to find the adjacent tiles and populate the localTiles list
        //Uses an overlapSphere to find local colliders and checks them for tileScripts
        Collider[] coliders = Physics.OverlapSphere(transform.position, localArea);
        foreach(Collider col in coliders)
        {
            if (col.gameObject.GetComponent<TileScript>())
            {
                localTiles.Add(col.gameObject.GetComponent<TileScript>());
            }
            //Checks the local list for this instance of tileScript and removes it
            if (localTiles.Contains(this)) { localTiles.Remove(this); }
        }
    }
}
