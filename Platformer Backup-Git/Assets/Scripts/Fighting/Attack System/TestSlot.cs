using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(fileName = "NewAttackSlot", menuName = "Attack Slot")]
public class TestSlot : ScriptableObject, ISlot<Attack, string> {
    [SerializeField]
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

}
