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

    public GameObject levelSelectHelp;
    public GameObject characterSheetHelp;
    public GameObject settingsHelp;
    public bool help;
    int screen;

    public void OpenLevelSelectMenu()
    {
        screen = 0;
        //This will open the levelselectmenu after closing any open menu
        CloseMenus();
        levelSelectScreen.SetActive(true);
    }

    public void OpenCharacterSheetMenu()
    {
        screen = 1;
        //This will open the charactersheetmenu after closing any open menu
        CloseMenus();
        characterSheetScreen.SetActive(true);
    }

    public void OpenSettingsMenu()
    {
        screen = 2;
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
        HelpBoxes();
    }

    public void ExitGame()
    {
        //This will close the application
        Application.Quit();
    }

    public void Help()
    {
        help = !help;
        HelpBoxes();
        if (help == true)
        {//This will only start to wait if the boxes are visible
            StartCoroutine(HideHelp());
        }
        else { StopAllCoroutines(); }
    }

    IEnumerator HideHelp()
    {//This will wait 5 seconds before hiding the help boxes
        yield return new WaitForSeconds(5);
        if (help == true)
        {
            Help();
        }
    }

    void HelpBoxes() 
    {//This hides all help, then shows the relevant boxes
        levelSelectHelp.SetActive(false);
        characterSheetHelp.SetActive(false);
        settingsHelp.SetActive(false);
        switch (screen)
        {
            case 0:
                levelSelectHelp.SetActive(help);
                break;
            case 1:
                characterSheetHelp.SetActive(help);
                break;
            case 2:
                settingsHelp.SetActive(help);
                break;
        }        
    }
}
