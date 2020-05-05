using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*EAS12337350
 * This script will attach to the main canvas and when the playerTurnScript makes the event screen visible, this script takes over
 * This will read the event data and provide the necessary functionality
 */

public class EventHandler : MonoBehaviour
{
    public PlayerTurnScript player;
    TileEventData data;

    public List<GameObject> actionButtons = new List<GameObject>();

    public TMP_Text eventName;
    public TMP_Text eventDescription;
    public Image eventVisual;
    
    private void Update()
    {
        if (player.isEvent)
        {
            GetEventData();
            if (data.eventOptionList.Length > 0)
            {
                for (int i = 0; i < data.eventOptionList.Length; i++)
                {//Sets Active the buttons required for this event
                    actionButtons[i].SetActive(true);
                    actionButtons[i].GetComponentInChildren<TMP_Text>().text = data.eventOptionList[i].optionName;
                }
            }
            else
            {
                EndEvent();
                StartCoroutine(WaitForEventEnd());
            }
            switch (data.type)
            {//Depending on the type of event, the name and description are changed
                case TileEventData.tileEventType.Event:
                    eventName.text = "Event!";
                    break;
                case TileEventData.tileEventType.Enemy:
                    eventName.text = "Enemy!";
                    break;
                case TileEventData.tileEventType.Trap:
                    eventName.text = "Trap!";
                    DealTrapDamage();
                    break;
                case TileEventData.tileEventType.Quest:
                    eventName.text = "Quest!";
                    break;
            }
            eventDescription.text = data.eventDescription;
            eventVisual = data.eventVisual;

            player.isEvent = false;
        }
    }

    public void DealTrapDamage()
    {
        int dealtDamage = (data.eventDamage - player.characters[player.player].charSheet.defence);
        //Checks that defence isn't greater than damage, else deal no damage, not negative damage
        if (dealtDamage < 0) { dealtDamage = 0; }
        player.characters[player.player].health -= dealtDamage;
        player.UpdatePlayerHealth();
    }

    public void EventEffects(int optionNum)
    {
        //This will perform different effect based on the event specifications
        if (data.eventOptionList[optionNum].willChangePower)
        {
            player.characters[player.player].charSheet.powerLevel += data.eventOptionList[optionNum].powerChange;
        }
        if (data.eventOptionList[optionNum].willChangeHealth)
        {
            player.characters[player.player].health += data.eventOptionList[optionNum].healthChange;
        }
        if (data.eventOptionList[optionNum].willTeleport)
        {
            player.TeleportPlayer(data.eventOptionList[optionNum].teleportTo);
        }
    }

    public void CheckSuccess(int optionNum)
    {
        player.RollDice();
        if (player.spacesToMove > data.eventOptionList[optionNum].successRate)
        {
            //Actions to take if successful
            eventDescription.text = data.eventOptionList[optionNum].successOutcomeText;
            EventEffects(optionNum);
        }
        else
        {
            //Actions to take if not successful
            eventDescription.text = data.eventOptionList[optionNum].failureOutcomeText;
        }
        EndEvent();
        StartCoroutine(WaitForEventEnd());
    }

    IEnumerator WaitForEventEnd()
    {
        yield return new WaitForSeconds(2);
        player.ResetTurn();
    }

    void EndEvent()
    {
        for (int i = 0; i < actionButtons.Count; i++)
        {
            actionButtons[i].SetActive(false);
        }
    }

    void GetEventData()
    {
        data = player.GetTileData();
    }
}
