using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*EAS12337350
 * This inherits from TileEventData and carries field specific to the enemy event encounters
 */
 [CreateAssetMenu]
public class EnemyEventData : TileEventData
{
    public string enemyName;
    [TextArea(2,10)]
    public string enemyDescription;

    public Image enemyVisual;

    public int enemyHealth;
    public int enemyAttack;
    public int enemyDefence;
}
