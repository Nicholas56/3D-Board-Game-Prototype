﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
/*EAS12337350
 * This script will handle save data 
 * This will offer static function for other classes
 */

public class GameManager : MonoBehaviour
{
    public List<CharSave> savedCharacters = new List<CharSave>();
    public static List<CharacterSheet> playerCharacters = new List<CharacterSheet>();

    //Each level will have a specific data file with all the pertinent data
    public GameData levelData;
    Camera mainCamera;

    public List<TileScript> mapTiles = new List<TileScript>();
    public List<AudioSource> audios;
    public static bool muteMusic;
    public static bool muteSound;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        FindAllMapTiles(mainCamera);
        if (muteMusic) { audios[0].mute = true; }
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
            //If the tile is an item holding tile, the item prefab will be created
            if (mapTiles[i].hasItem) { Instantiate(levelData.itemPrefab, mapTiles[i].transform); }
        }
    }

    public void LoadCharacters()
    {//Allows for a max of 18 characters
        savedCharacters.Clear();
        for (int i = 0; i < 18; i++)
        {
            //This will find the saved JSON list of characters and store them here for use
            if (File.Exists(Application.persistentDataPath + "/charSave.save" + i))
            {//Finds the string and converts it into CharSave data to be put into the list of savedCharacters
                string JSONSTring = File.ReadAllText(Application.persistentDataPath + "/charSave.save" + i);
                savedCharacters.Add( JsonUtility.FromJson<CharSave>(JSONSTring));
            }

        }
    }

    public bool SaveCheck()
    {//this checks if the player's computer contains saves for this game
        if (!File.Exists(Application.persistentDataPath + "/charSave.save" + 0))
        {
            return true;
        }
        else { return false; }
    }

    public void SaveCharacter(CharSave save ,int saveFileNumber)
    {
        //This will store the list of characters as a string and create a JSON save file
        string JSONString = JsonUtility.ToJson(save);

        File.WriteAllText(Application.persistentDataPath + "/charSave.save" + saveFileNumber, JSONString);

        LoadCharacters();
    }

    public void DeleteCharacter(int fileNum)
    {
        //This will delete all data, then reconstruct it from the savedCharacter data
        for (int i = 0; i < savedCharacters.Count; i++)
        {
            File.Delete(Application.persistentDataPath + "/charSave.save" + i);
        }
        savedCharacters.Remove(savedCharacters[fileNum]);
        for (int i = 0; i < savedCharacters.Count; i++)
        {
            string JSONString = JsonUtility.ToJson(savedCharacters[i]);
            File.WriteAllText(Application.persistentDataPath + "/charSave.save" + i, JSONString);
        }
    }

    public void ToggleMuteMusic()
    {//Mutes the sound from main music track, true for other scenes
        audios[0].mute = !audios[0].mute;
        muteMusic = audios[0].mute;
    }

    public void ToggleMuteSoundEffects()
    {
        for (int i = 1; i < audios.Count; i++)
        {
            audios[i].mute = !audios[i].mute;
        }
        muteSound = audios[1].mute;
    }
}
