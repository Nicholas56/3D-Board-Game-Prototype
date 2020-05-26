using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*EAS12337350
 * This scriptable object will hold all the data for a level in terms of monsters, traps, events and quests
 */
 [CreateAssetMenu]
public class GameData : ScriptableObject
{
    //These two will be combined for a list of all events, to randomly pick from
    public List<TileEventData> positiveEventsData;
    public List<TileEventData> negativeEventsData;

    public List<TileEventData> enemyEventsData;
    public List<TileEventData> trapsEventsData;
    public List<TileEventData> questEventsData;

    public List<int> itemData;
    public GameObject itemPrefab;

    public GameObject goalObject;

    //public List<int> abilitiesData;

    public List<CharacterSheet> savedCharacters;

    public List<Sprite> icon;
}
