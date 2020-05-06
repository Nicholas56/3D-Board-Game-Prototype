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
    public enum tileEventType { Event, Enemy, Trap, Quest }
    public tileEventType type;

    public string eventName;
    [TextArea(2, 10)]
    public string eventDescription = "";

    public Image eventVisual;

    public int maxHealth;
    public int eventHealth;
    public int eventAttack;
    public int eventDefence;

    public int eventDamage;

    public int eventReward;

    public eventOption[] eventOptionList = new eventOption[3];
    
}

[System.Serializable]
public class eventOption
{
    public string optionName;
    public int successRate;
    [TextArea(2, 5)]
    public string successOutcomeText;
    [TextArea(2, 5)]
    public string failureOutcomeText;

    public bool willChangePower;
    public int powerChange;
    public bool willChangeHealth;
    public int healthChange;

    public bool willTeleport;
    [Tooltip("This number should be lower than number of map tiles")]
    public int teleportTo;

    public bool isAttack;
}
