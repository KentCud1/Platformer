using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttackSlot : ISlot<ComboAttack, string[]> {

    public ComboAttack attack;
    public string[] trigger;

    public void CheckAttackTrigger() {
        throw new NotImplementedException();
    }

    public void SetAttackOnTrigger(ComboAttack attackOnTrigger) {
        attack = attackOnTrigger;
    }

    public void SetSlotTrigger(string[] triggerStringList) {
        trigger = triggerStringList;
    }

    public void CheckAttackTrigger(string[] triggerTest) {
        if (triggerTest.IsEqual(trigger)) {
            attack.Trigger();
        }
    }

    public ComboAttackSlot(ComboAttack atk, string[] comboList) {
        SetAttackOnTrigger(atk);
        SetSlotTrigger(comboList);

    }
}
