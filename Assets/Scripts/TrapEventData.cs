using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*EAS12337350
 * This inherits from the tileEventData script and provides data regarding traps
 */

public class TrapEventData : TileEventData
{
    public string trapName;
    [TextArea(2, 10)]
    public string trapDescription;

    public int trapDamage;
}
