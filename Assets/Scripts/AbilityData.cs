using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*EAS12337350
 * This scriptable object will hold data on what effects an ability has
 */
 [CreateAssetMenu]
public class AbilityData : ScriptableObject
{
    public string abilityName;
    [TextArea(2,5)]
    public string abilityDescription;

    public bool affectHealth;
    public int healthEffect;

    public bool affectAttack;
    public int attackEffect;

    public bool affectDefence;
    public int defenceEffect;

    public bool dodgeTraps;

    public bool canTeleport;
    public int teleportsPerLevel;

    public bool invisiblity;
}
