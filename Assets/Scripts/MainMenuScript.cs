using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*EAS12337350
 * This script will handle the menu in the main scene
 */

public class MainMenuScript : MonoBehaviour
{
    public GameObject levelSelectScreen;
    public GameObject characterSheetScreen;
    public GameObject settingsScreen;

    public void OpenLevelSelectMenu()
    {
        //This will open the levelselectmenu after closing any open menu
        CloseMenus();
        levelSelectScreen.SetActive(true);
    }

    public void OpenCharacterSheetMenu()
    {
        //This will open the charactersheetmenu after closing any open menu
        CloseMenus();
        characterSheetScreen.SetActive(true);
    }

    public void OpenSettingsMenu()
    {
        //This will open the settingsmenu after closing any open menu
        CloseMenus();
        settingsScreen.SetActive(true);
    }

    public void CloseMenus()
    {
        //This will close all menus
        levelSelectScreen.SetActive(false);
        characterSheetScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    public void ExitGame()
    {
        //This will close the application
        Application.Quit();
    }
}
