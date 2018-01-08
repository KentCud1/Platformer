using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlot<T, U> where T : Attack {

    void SetSlotTrigger(U trigger);
    void SetAttackOnTrigger(T attack);

    void CheckAttackTrigger(U triggerTest);


}
