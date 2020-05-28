using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*EAS12337350
 * This will contain the effects used in events, battles and items
 */

public class EventEffects: MonoBehaviour
{
    PlayerTurnScript player;

    public void SetPlayerTurn(PlayerTurnScript turnScript) { player = turnScript; }
    public void Effects(EventOption option)
    {
        Character chara = player.characters[player.player];

        //Puts the effects into a list, puts the values into a list
        List<EventOption.eventType> options = new List<EventOption.eventType>();
        List<int> optionValues = new List<int>();
        options.Add(option.effect1); optionValues.Add(option.effect1Value);
        options.Add(option.effect2); optionValues.Add(option.effect2Value);
        //This goes through the effects on the event options and produces the correct effect
        for (int i = 0; i < options.Count; i++)
        {
            switch (options[i])
            {
                case EventOption.eventType.None:
                    break;
                case EventOption.eventType.Heal:
                    chara.health += optionValues[i];
                    break;
                case EventOption.eventType.Power:
                    chara.charSheet.powerLevel += optionValues[i];
                    break;
                case EventOption.eventType.Attack:
                    TileEventData data = player.GetTileData();
                    data.eventHealth -= (chara.Attack + chara.tempAttack + player.spacesToMove - data.eventDefence);
                    player.gameObject.GetComponent<EventHandler>().enemyHealth.GetComponentInChildren<TMP_Text>().text = "" + data.eventHealth + "/" + data.maxHealth;
                    break;
                case EventOption.eventType.Run:
                    player.gameObject.GetComponent<EventHandler>().isRunning = true;
                    break;
                case EventOption.eventType.AddAbility:
                    player.AddAbility(optionValues[i]);
                    break;
                case EventOption.eventType.TempHP:
                    chara.tempHealth += optionValues[i];
                    break;
                case EventOption.eventType.TempAtk:
                    chara.tempAttack += optionValues[i];
                    break;
                case EventOption.eventType.TempDef:
                    chara.tempDefence += optionValues[i];
                    break;
                case EventOption.eventType.Teleport:
                    player.TeleportPlayer(optionValues[i]);
                    break;
                case EventOption.eventType.TempDuration:
                    chara.tempCounter += optionValues[i];
                    break;
            }
        }
    }
}
