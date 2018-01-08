using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AttackControl : MonoBehaviour {


    public List<string> comboString = new List<string> {"LightKick", "LightPunch"};

    public string currentInputString, nextInputString;
    public List<string> currentInputStringCombo;
    public float comboTimer = .35f, cTime;
    public string[] comboArray;

    //ComboAttackSlot slot = new ComboAttackSlot(new ComboAttack(), );

    // Use this for initialization
    void Start() {
       // slot.trigger = comboString.ToArray();
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetButtonDown("LightPunch")) {
            currentInputString = "LightPunch";
            currentInputStringCombo.Add("LightPunch");
            cTime = comboTimer;
        }

        if (Input.GetButtonDown("LightKick")) {
            currentInputString = "LightKick";
            currentInputStringCombo.Add("LightKick");
            cTime = comboTimer;
        }

        cTime -= Time.deltaTime;

         if(cTime <= 0) {
             currentInputStringCombo.Clear();
         }

        comboArray = currentInputStringCombo.ToArray();

        //if(currentInputStringCombo.IsEqual(slot.trigger)) {
        //    Debug.Log("ComboTriggered!");
        //    return;
        //}


    }


}
