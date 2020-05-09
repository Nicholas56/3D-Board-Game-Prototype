using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*EAS12337350
 * This is the list of effects that occur when an ability is called
 */

public static class AbilityEffects
{

    public static void AbilityEffect(Ability ability, PlayerTurnScript player)
    {
        Character chara = player.characters[player.player];
        switch (ability.type)
        {
            case Ability.abilityType.Health:
                chara.charSheet.maxHealth += ability.value;
                break;
            case Ability.abilityType.Attack:
                chara.charSheet.attack += ability.value;
                break;
            case Ability.abilityType.Defence:
                chara.charSheet.defence += ability.value;
                break;
            case Ability.abilityType.TrapDodge:
                chara.trapDodge = true;
                break;
            case Ability.abilityType.AffectDice:
                chara.charSheet.moveVar += ability.value;
                break;
            case Ability.abilityType.DiceFix:
                if (ability.value > chara.charSheet.rollFix)
                {//The ability with the highest value is used for fixing the value of the dice roll
                    chara.charSheet.rollFix = ability.value;
                }
                break;
            case Ability.abilityType.Teleport:
                chara.teleport = true;
                break;
        }
    }
}
