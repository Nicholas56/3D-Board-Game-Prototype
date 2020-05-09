using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*EAS12337350
 * This script will list all the effects called when an item is used
 */

public class ItemEffects : MonoBehaviour
{
    PlayerTurnScript player;
    public void SetPlayerTurn(PlayerTurnScript turnScript) { player = turnScript; }
    public void ItemEffect(Item item)
    {
        Character chara = player.characters[player.player];
        switch (item.iType)
        {
            case Item.itemType.Stop:
                //Forces the player to land on the current tile
                player.spacesToMove = 0;
                player.StartEvent();
                break;
            case Item.itemType.Move:
                //Alters the movement spaces left to player
                player.spacesToMove += item.value;
                player.UpdateMovesToGo();
                break;
            case Item.itemType.TempHP:
                //Alters the temporary health boost of player
                chara.tempHealth += item.value;
                chara.tempCounter +=item.tempDuration;
                player.UpdatePlayerHealth();
                break;
            case Item.itemType.TempAtk:
                //Alters the temporary health boost of player
                chara.tempAttack += item.value;
                chara.tempCounter += item.tempDuration;
                break;
            case Item.itemType.TempDef:
                //Alters the temporary health boost of player
                chara.tempDefence += item.value;
                chara.tempCounter += item.tempDuration;
                break;
            case Item.itemType.InstaKill:
                //InstaKills the enemy
                TileEventData data = player.GetTileData();
                if (data.type == TileEventData.tileEventType.Enemy)
                {
                    data.eventHealth -= data.maxHealth * 2;
                    FindObjectOfType<EventHandler>().EndEvent();
                }
                break;
            case Item.itemType.AbilityAdd:
                //This adds the listed ability to the characters current abilities
                player.AddAbility(item.value);
                break;
        }
    }
}
