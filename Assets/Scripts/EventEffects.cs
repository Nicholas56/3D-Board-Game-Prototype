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
    public void Effects(eventOption option, EventHandler handler)
    {
        Character chara = player.characters[player.player];

        if (handler)
        {
            //COMBAT
            //This is the effect for standard attack //Barely used option data, needs review!!
            if (option.isAttack)
            {
                TileEventData data = player.GetTileData();
                data.eventHealth -= (chara.Attack + chara.tempAttack + player.spacesToMove - data.eventDefence);
                handler.enemyHealth.GetComponentInChildren<TMP_Text>().text = "" + data.eventHealth + "/" + data.maxHealth;
            }
            if (option.isInstaKill && player.GetTileData().type == TileEventData.tileEventType.Enemy)
            {
                TileEventData data = player.GetTileData();
                data.eventHealth -= data.maxHealth * 2;
                handler.EndEvent();
            }
        }

        //This will perform different effect based on the event specifications
        //This is the effect for changing the player's power level
        if (option.willChangePower)
        {
            chara.charSheet.powerLevel += option.powerChange;
        }
        //This is the effect for changing the player's health
        if (option.willChangeHealth)
        {
            chara.health += option.healthChange;
        }
        //This is the effect for teleporting
        if (option.willTeleport)
        {
            player.TeleportPlayer(option.teleportTo);
        }

        //ABILITIES
        if (option.isAddAbility)
        {//Adds the given ability
            player.AddAbility(option.abilityIdNum);
        }

        //DICE //Dice manipulation will occur here
        if (option.isStop)
        {//Forces player to land on current tile
            player.spacesToMove = 0;
            chara.currentTile.LandOnTile();
        }
        if (option.isAddMove)
        {//Adds/removes spaces to move in turn
            player.spacesToMove += option.moveChange;
            player.UpdateMovesToGo();
        }

        //These effects will change and boost temporary stats
        if (option.isAddTempHP)
        {
            chara.tempHealth += option.tempHP;
        }
        if (option.isAddTempAtk)
        {
            chara.tempAttack += option.tempAtk;
        }
        if (option.isAddTempDef)
        {
            chara.tempDefence += option.tempDef;
        }
        if (option.tempDuration > 0)
        {
            chara.tempCounter += option.tempDuration;
        }
    }
}
