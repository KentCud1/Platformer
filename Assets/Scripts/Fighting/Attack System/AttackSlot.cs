using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackSlot", menuName = "Attack Slot")]
public class AttackSlot : ScriptableObject, ISlot<Ability, GameButton> {
    public Ability attack;
    public GameButton trigger;

    public void SetSlotTrigger(GameButton triggerString) {
        trigger = triggerString;
    }

    public void SetAttackOnTrigger(Ability attackOnTrigger) {
        attack = attackOnTrigger;
    }

    public bool CheckAttackTrigger(GameButton triggerTest) {
        if (trigger == triggerTest) {
            return true;
        }
        return false;
    }

    public AttackSlot(Ability atk, GameButton triggerString) {
        SetAttackOnTrigger(atk);
        SetSlotTrigger(triggerString);

    }
}
