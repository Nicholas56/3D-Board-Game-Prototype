using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*EAS12337350
 * This will hold the Ability class, as well as a static list for all abilities
 */

public class AbilityScript : MonoBehaviour
{
    public static List<Ability> gameAbilities = new List<Ability>();

    private void Start()
    {
        gameAbilities.Clear();
        List<Ability> abilities = new List<Ability>();
        {//Number at the end is the Ability ID
            //HEALTH abilities
            abilities.Add(new Ability("", Ability.abilityType.Health, 0, 000));
            abilities.Add(new Ability("Small HP Boon", Ability.abilityType.Health, 2, 001));
            abilities.Add(new Ability("Mid HP Boon", Ability.abilityType.Health, 5, 002));
            abilities.Add(new Ability("Large HP Boon", Ability.abilityType.Health, 10, 003));
            abilities.Add(new Ability("Small HP Curse", Ability.abilityType.Health, -2, 004));
            abilities.Add(new Ability("Mid HP Curse", Ability.abilityType.Health, -5, 005));
            abilities.Add(new Ability("Large HP Curse", Ability.abilityType.Health, -10, 006));
            //ATTACK abilities
            abilities.Add(new Ability("", Ability.abilityType.Attack, 0, 100));
            abilities.Add(new Ability("Small ATK Boon", Ability.abilityType.Attack, 2, 101));
            abilities.Add(new Ability("Mid ATK Boon", Ability.abilityType.Attack, 5, 102));
            abilities.Add(new Ability("Large ATK Boon", Ability.abilityType.Attack, 10, 103));
            abilities.Add(new Ability("Small ATK Curse", Ability.abilityType.Attack, -2, 104));
            abilities.Add(new Ability("Mid ATK Curse", Ability.abilityType.Attack, -5, 105));
            abilities.Add(new Ability("Large ATK Curse", Ability.abilityType.Attack, -10, 106));
            //DEFENCE abilities
            abilities.Add(new Ability("", Ability.abilityType.Defence, 0, 200));
            abilities.Add(new Ability("Small DEF Boon", Ability.abilityType.Defence, 2, 201));
            abilities.Add(new Ability("Mid DEF Boon", Ability.abilityType.Defence, 5, 202));
            abilities.Add(new Ability("Large DEF Boon", Ability.abilityType.Defence, 10, 203));
            abilities.Add(new Ability("Small DEF Curse", Ability.abilityType.Defence, -2, 204));
            abilities.Add(new Ability("Mid DEF Curse", Ability.abilityType.Defence, -5, 205));
            abilities.Add(new Ability("Large DEF Curse", Ability.abilityType.Defence, -10, 206));
            //TRAP-DODGE ability
            abilities.Add(new Ability("Trap-Dodge", Ability.abilityType.TrapDodge, 0, 300));
            //AFFECT DICE abilities
            abilities.Add(new Ability("", Ability.abilityType.AffectDice, 0, 400));
            abilities.Add(new Ability("Small Move Boon", Ability.abilityType.AffectDice, 1, 401));
            abilities.Add(new Ability("Mid Move Boon", Ability.abilityType.AffectDice, 2, 402));
            abilities.Add(new Ability("Large Move Boon", Ability.abilityType.AffectDice, 4, 403));
            abilities.Add(new Ability("Small Move Curse", Ability.abilityType.AffectDice, -1, 404));
            abilities.Add(new Ability("Mid Move Curse", Ability.abilityType.AffectDice, -2, 405));
            abilities.Add(new Ability("Large Move Curse", Ability.abilityType.AffectDice, -4, 406));
            //DICE-FIX abilities
            abilities.Add(new Ability("", Ability.abilityType.DiceFix, 0, 500));
            abilities.Add(new Ability("Fix One", Ability.abilityType.DiceFix, 1, 501));
            abilities.Add(new Ability("Fix Two", Ability.abilityType.DiceFix, 2, 502));
            abilities.Add(new Ability("Fix Three", Ability.abilityType.DiceFix, 3, 503));
            abilities.Add(new Ability("Fix Four", Ability.abilityType.DiceFix, 4, 504));
            abilities.Add(new Ability("Fix Five", Ability.abilityType.DiceFix, 5, 505));
            abilities.Add(new Ability("Fix Six", Ability.abilityType.DiceFix, 6, 506));
            //TELEPORT ability
            abilities.Add(new Ability("Random Teleport", Ability.abilityType.Teleport, 0, 600));
        }
        //Add the abilities to the static list GameAbilities
        gameAbilities.AddRange(abilities);
    }

    public static Ability GetAbility(int id)
    {
        //This will return the ability with the given id
        return gameAbilities.Find(x => x.abilityID == id);
    }
}

public struct Ability
{
    public int abilityID;
    public string abilityName;
    public enum abilityType { Health, Attack, Defence, TrapDodge, AffectDice, DiceFix, Teleport }
    public abilityType type;

    public int value;

    public Ability(string aName, abilityType aType, int aValue, int aID)
    {
        abilityName = aName;
        type = aType;
        value = aValue;
        abilityID = aID;
    }
}
