using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*EAS12337350
 * This script will allow the camer to follow the player token, and allow for the camera to move around the board in an RTS style
 * Attaches to a camera holder, where the main camera is the child
 */

public class CameraScript : MonoBehaviour
{
    public float cameraSpeed;

    PlayerTurnScript player;
    Transform token;
    bool freeCamera = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerTurnScript>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    // The camera will be able to move away from the player token, though will snap back when free movement ends
    void Update()
    {
        if(player && !token) { FollowToken(); }
        if (freeCamera)
        {
            if (transform.parent == token) { transform.SetParent(null); }
            if (Input.GetAxis("Mouse X") > 0)
            {
                Debug.Log("happens");
                transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * cameraSpeed, 0.0f,
                    Input.GetAxisRaw("Mouse Y") * Time.deltaTime * cameraSpeed);
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * cameraSpeed, 0.0f,
                    Input.GetAxisRaw("Mouse Y") * Time.deltaTime * cameraSpeed);
            }
        }
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

    public void FreeCamera()
    {
        freeCamera = !freeCamera;
        if (freeCamera == false)
        {//If the free camera is turned off, the camera will find the player again
            transform.position = token.position;
            transform.SetParent(token);
        }
    }
}
