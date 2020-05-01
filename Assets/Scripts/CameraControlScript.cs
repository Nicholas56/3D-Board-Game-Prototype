using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*EAS12337350
 * This will follow the current character. 
 * Option to free camera from character and look over the board in RTS style
 */

public class CameraControlScript : MonoBehaviour
{
    public Transform currentCharacter;

    public bool canMoveCamera;

    public void MoveToCharacter()
    {
        //Finds the current character and becomes a child of that transform
    }

    private void Update()
    {
        //If the canMoveCamera bool is true, this camera can be moved about the map, within limits
    }
}
