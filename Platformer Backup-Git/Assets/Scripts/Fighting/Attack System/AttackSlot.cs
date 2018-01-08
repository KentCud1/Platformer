using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSlot : ISlot<Attack, string> {
    public Attack attack;
    public string trigger;

    public void SetSlotTrigger(string triggerString) {
        trigger = triggerString;
    }

    public void SetAttackOnTrigger(Attack attackOnTrigger) {
        attack = attackOnTrigger;
    }

    public void CheckAttackTrigger(string triggerTest) {
        if (trigger == triggerTest) {
            attack.Trigger();
        }
    }

    public AttackSlot(Attack atk, string triggerString) {
        SetAttackOnTrigger(atk);
        SetSlotTrigger(triggerString);

    }
}
