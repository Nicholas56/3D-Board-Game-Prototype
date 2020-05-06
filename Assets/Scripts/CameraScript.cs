using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*EAS12337350
 * This script will allow the camer to follow the player token, and allow for the camera to move around the board in an RTS style
 * Attaches to a camera holder, where the main camera is the child
 */

public class CameraScript : MonoBehaviour
{
    PlayerTurnScript player;
    Transform token;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerTurnScript>();
    }

    // The camera will be able to move away from the player token, though will snap back when free movement ends
    void Update()
    {
        if(player && !token) { FollowToken(); }
    }

    public void FollowToken()
    {
        //This finds the current player token and sets it as the parent of this transform
        token = player.characters[player.player].gameToken.transform;
        transform.position = token.position;
        transform.SetParent(token);
    }

    public void ResetToken()
    {
        token = null;
    }
}
