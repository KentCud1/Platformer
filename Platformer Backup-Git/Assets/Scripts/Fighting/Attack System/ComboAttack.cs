using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : Attack {

    public ComboAttack() {

    }

    public ComboAttack(string name) {
        this.name = name;
    }

    public ComboAttack(string name, int dam) {
        this.name = name;
        damage = dam;
    }

    public ComboAttack(string name, string desc, int dam) {
        this.name = name;
        description = desc;
        damage = dam;
    }


}
