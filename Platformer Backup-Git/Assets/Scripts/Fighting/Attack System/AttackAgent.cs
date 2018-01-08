using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAgent : MonoBehaviour {
    public List<AttackSlot> attackList = new List<AttackSlot>();
    public List<ComboAttackSlot> comboList = new List<ComboAttackSlot>();
    public float health = 20f;

    public string stringTest = "LightPunch";

    public string[] comboTest = new string[] { "LightPunch", "HeavyPunch" };
    public string[] strlist = new string[1];

	// Use this for initialization
	void Start () {
        comboTest = new string[] { "LightPunch", "HeavyPunch" };
        attackList.Add(new AttackSlot(new Attack("Fist"), "LightPunch"));
        attackList.Add(new AttackSlot(new Attack("Elbow"), "HeavyPunch"));
        attackList.Add(new AttackSlot(new Attack("Hook"), "LightPunch"));
        strlist = new string[] { "LightPunch", "HeavyPunch" };
        comboList.Add(new ComboAttackSlot(new ComboAttack("Uppercut"), strlist));
        comboList.Add(new ComboAttackSlot(new ComboAttack("Pound"), strlist));
        strlist = new string[] { "IightKick", "LightPunch" };
        comboList.Add(new ComboAttackSlot(new ComboAttack("OneTwo"), strlist));

    }

    // Update is called once per frame
    void Update () {
        if (attackList.Count > 0) {
            for (int i = 0; i < attackList.Count; i++) {
                attackList[i].CheckAttackTrigger(stringTest);
            }
        }

        if (comboList.Count > 0) {
            for (int i = 0; i < comboList.Count; i++) {
                comboList[i].CheckAttackTrigger(comboTest);
            }
        }
    }
}
