using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*EAS12337350
 * This scriptable object will be stored in lists for use in the TileScript. 
 * This will hold eventDescription and eventOption with corresponding option successRate and optionOutcome 
 */
 [CreateAssetMenu]
public class TileEventData : ScriptableObject
{
    [TextArea(5, 20)]
    public string eventDescription = "";

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
}
