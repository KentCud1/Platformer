using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlot<T, U> where T : Ability {

    void SetSlotTrigger(U trigger);
    void SetAttackOnTrigger(T attack);

    bool CheckAttackTrigger(U triggerTest);


}
