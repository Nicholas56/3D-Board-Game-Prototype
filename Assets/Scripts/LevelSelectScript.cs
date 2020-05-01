using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*EAS12337350
 * This script will handle data for selecting levels. 
 * This will present data and provide options for the level select menu
 */

public class LevelSelectScript : MonoBehaviour
{
    //This will hold all the scenes available
    public List<Scene> levels = new List<Scene>();

    public void LoadLevel()
    {
        //This will take an input and select from the list above which level to load
        //Level should be loaded on top of previous scene, not replace
    }
}

[System.Serializable]
public class Level
{
    public string levelName;
    [TextArea(5,15)]
    public string levelDescription;
    public Scene levelScene;

}
