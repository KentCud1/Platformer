using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack {

    public enum AttackType {
        DAMAGE,
        HEAL,
        BUFF,
        DEBUFF
    }

    public string name;
    public string description;
    public int damage;

    public Attack() {

    }

    public Attack(string name) {
        this.name = name;
    }

    public Attack(string name, int dam) {
        this.name = name;
        damage = dam;
    }

    public Attack(string name, string desc, int dam) {
        this.name = name;
        description = desc;
        damage = dam;
    }
    
    public void Trigger() {
        Debug.Log(name + " attackTriggered");
    }
}
