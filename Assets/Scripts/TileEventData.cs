using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*EAS12337350
 * This scriptable object will be stored in lists for use in the TileScript. 
 * This will hold eventDescription and eventOption with corresponding option successRate and optionOutcome 
 */
 [CreateAssetMenu]
public class TileEventData : ScriptableObject
{
    public enum tileEventType { Event, Enemy, Trap, Quest, Item }
    public tileEventType type;

    public string eventName;
    [TextArea(2, 10)]
    public string eventDescription = "";

    public Sprite eventVisual;

    public int maxHealth;
    public int eventHealth;
    public int eventAttack;
    public int eventDefence;

    public int eventDamage;

    public int eventReward;

    public EventOption[] eventOptions;
    
}

[System.Serializable]
public class EventOption
{
    public string optionName;
    public int successRate;
    [TextArea(2, 5)]
    public string successOutcomeText;
    [TextArea(2, 5)]
    public string failureOutcomeText;

    public enum eventType { None, Attack, Heal, Power, AddAbility, TempHP, TempAtk, TempDef, Teleport, TempDuration  }
    public eventType effect1;
    public int effect1Value;
    public eventType effect2;
    public int effect2Value;
}
