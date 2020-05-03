using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacterScript : MonoBehaviour
{
    public GameObject token;
    public TileScript startTile;

    // Start is called before the first frame update
    void Start()
    {
        GameObject testToken = Instantiate(token);
        Character test = new Character(testToken, null, startTile);
        FindObjectOfType<PlayerTurnScript>().characters.Add(test);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
