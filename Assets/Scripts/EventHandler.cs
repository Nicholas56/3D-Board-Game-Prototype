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
    public EventEffects action;
    TileEventData data;
    int optionNumber;

    public List<GameObject> actionButtons = new List<GameObject>();

    public TMP_Text eventName;
    public TMP_Text eventDescription;
    public Image eventVisual;

    public GameObject rollButton;
    public GameObject enemyHealth;
    public GameObject enemyTurnPanel;
    
    private void Update()
    {
        if (player.isEvent)
        {
            rollButton.SetActive(false);
            GetEventData();
            if (data == null) 
            {
                rollButton.SetActive(true);
                player.ResetTurn();
                player.isEvent = false;
                return;
            }
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
            enemyHealth.SetActive(false);


            eventDescription.text = data.eventDescription;
            switch (data.type)
            {//Depending on the type of event, the name and description are changed
                case TileEventData.tileEventType.Event:
                    eventName.text = "Event!";
                    break;
                case TileEventData.tileEventType.Enemy:
                    eventName.text = "Enemy!";
                    enemyHealth.SetActive(true);
                    //Resets the enemy health to full for battle
                    data.eventHealth = data.maxHealth;
                    UpdateEnemyHealth();
                    break;
                case TileEventData.tileEventType.Trap:
                    eventName.text = "Trap!";
                    DealTrapDamage();
                    break;
                case TileEventData.tileEventType.Quest:
                    eventName.text = "Quest!";
                    break;
            }
            eventVisual.sprite = data.eventVisual;
            
            player.isEvent = false;
        }
    }

    public void DealTrapDamage()
    {
        Character chara = player.characters[player.player];
        if (chara.trapDodge)
        {
            eventDescription.text = "Your Trap-Dodge ability activates! No harm befalls you.";
            EndEvent();
        }
        else
        {
            int dealtDamage = (data.eventDamage - (chara.Defence+chara.tempDefence));
            //Checks that defence isn't greater than damage, else deal no damage, not negative damage
            if (dealtDamage < 0) { dealtDamage = 0; }
            //Checks to see if temp health can be used to mitigate damage
            if (chara.tempHealth > dealtDamage) { chara.tempHealth -= dealtDamage; }
            else
            { chara.health -= dealtDamage - chara.tempHealth; }
            player.UpdatePlayerHealth();
            //If player dies from trap event, death occurs here
            if (player.characters[player.player].health <= 0)
            {
                StartCoroutine(Death());
            }
        }
    }
    
    public void CheckSuccess(int optionNum)
    {
        if (player.isRoll)
        {
            //player.StopRoll();
        }
        else
        {
            player.BeginRoll();
            ButtonHide(optionNum);
            optionNumber = optionNum;
            StartCoroutine(PerformAction());
        } 
    }

    IEnumerator PerformAction()
    {
        yield return new WaitForSeconds(0.8f);
        EventAction(optionNumber);
    }

    void EventAction(int optionNum) { 
        if (player.spacesToMove > data.eventOptionList[optionNum].successRate)
        {
            //Actions to take if successful
            eventDescription.text = data.eventOptionList[optionNum].successOutcomeText;
            action.Effects(data.eventOptionList[optionNum], this);
            transform.parent.GetComponent<GameManager>().SoundEffect(1);
        }
        else
        {
            //Actions to take if not successful
            eventDescription.text = data.eventOptionList[optionNum].failureOutcomeText;
        }
        //COMBAT
        if (data.type == TileEventData.tileEventType.Enemy && data.eventHealth > 0)
        {//If the event is an Enemy and it still has health, the enemy gets a turn
            StartCoroutine(EnemyTurn());
        }
        else
        {//For all events other than Enemy, the turn will end after one button press
            EndEvent();
        }
    }

    IEnumerator WaitForEventEnd()
    {
        if(data.type == TileEventData.tileEventType.Enemy)
        {
            eventDescription.text = "The battle is over! You survived!";
            //Gives the victory message and grants a reward to the player
            player.characters[player.player].charSheet.powerLevel += data.eventReward;
        }
        yield return new WaitForSeconds(2);
        UpdateEnemyHealth();
        rollButton.SetActive(true);
        player.ResetTurn();
    }

    IEnumerator EnemyTurn()
    {
        enemyTurnPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        //The enemy rolls the dice and damage is calculated
        player.isRoll=true; 
        float randTime = Random.Range(0.1f, 0.9f);
        yield return new WaitForSeconds(randTime);
        player.isRoll=false;
        Character chara = player.characters[player.player];
        int enemyAttack = (player.spacesToMove + data.eventAttack);
        int playerDef = (chara.Defence + chara.tempDefence);
        int damageDealt = enemyAttack - playerDef;
        if (damageDealt < 0) { damageDealt = 0; }
        //Allows temp health to mitigate damage
        if(chara.tempHealth> damageDealt) { chara.tempHealth -= damageDealt; } else 
        { chara.health -= damageDealt - chara.tempHealth; }
        player.UpdatePlayerHealth();

        //Depending on the outcome of the attack, the battle message is read, if dead, player is sent to main menu
        if (chara.health > 0)
        {
            eventDescription.text = "The enemy attacks! It deals: " + ((player.spacesToMove + data.eventAttack) - 
                (chara.Defence + chara.tempDefence)) + 
                " damage!\n" + "The enemy has " + data.eventHealth + " health left.";
        }
        else
        {
            eventDescription.text = "The enemy has killed you! Oh dear!";
            yield return new WaitForSeconds(1);
            PlayerDeath();
        }
        enemyTurnPanel.SetActive(false);
    }

    IEnumerator Death()
    {
        //Ends the event prematurely, Reads the death message and sends te player back to the main menu
        EndEvent();
        eventDescription.text = "You appear to have died! Bad luck!";
        yield return new WaitForSeconds(1);
        PlayerDeath();
    }

    void PlayerDeath()
    {
        if (GameManager.playerCharacters.Count == 1)
        {
            //The player health is restored, power is taken away and the player is sent to the main menu
            player.characters[player.player].health = player.characters[player.player].MaxHealth;
            player.characters[player.player].charSheet.powerLevel -= 10;

            LevelSelectScript.ReturnToMainMenu();
        }
        else
        {
            Character deadChar = player.characters[player.player];
            player.ResetTurn();
            Destroy(deadChar.gameToken);
            player.characters.Remove(deadChar);
        }
    }

    void UpdateEnemyHealth()
    {
        enemyHealth.GetComponentInChildren<TMP_Text>().text = "" + data.eventHealth + "/" + data.maxHealth;
    }

    void ButtonHide(int buttonNum)
    {
        for (int i = 0; i < actionButtons.Count; i++)
        {
            if (i == buttonNum) { }
            else
            {
                actionButtons[i].SetActive(false);
            }
        }
    }

    public void EndEvent()
    {
        for (int i = 0; i < actionButtons.Count; i++)
        {
            actionButtons[i].SetActive(false);
        }
        StartCoroutine(WaitForEventEnd());
    }

    void GetEventData()
    {
        data = player.GetTileData();
    }
}
