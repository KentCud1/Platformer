using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class AttackAgent : MonoBehaviour {
    public List<AttackSlot> attackList = new List<AttackSlot>();
    public float health = 20f;

    public Ability currentAttack;
    public GameButton stringTest;

    private void OnEnable() {
        BattleController.numAttackAgents++;
        AttackControl.numAgents++;
    }

    private void OnDisable() {
        BattleController.numAttackAgents--;
        AttackControl.numAgents--;
    }

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {
        bool hasAttacked = false;
        if (attackList.Count > 0) {
            for (int i = 0; i < attackList.Count; i++) {
                if (attackList[i].CheckAttackTrigger(stringTest)) {
                    if (currentAttack == null || currentAttack == attackList[i].attack) {
                        hasAttacked = true;
                        currentAttack = attackList[i].attack;
                    }
                }
            }
        }
        if(hasAttacked == false) {
            currentAttack = null;
        }

        if(currentAttack != null) {
            currentAttack.Trigger();
        }
    }
}
