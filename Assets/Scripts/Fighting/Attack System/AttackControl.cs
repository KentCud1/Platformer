using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AttackControl : MonoBehaviour {

    public static int numAgents;
    public static AttackAgent[] agentList = new AttackAgent[0];
    public List<string> comboString = new List<string> {"LightKick", "LightPunch"};
    public GameButton[] gbs;
    public string currentInputString, nextInputString;
    public List<string> currentInputStringCombo;
    public float comboTimer = .35f, cTime;
    public string[] comboArray;

    //ComboAttackSlot slot = new ComboAttackSlot(new ComboAttack(), );

    // Use this for initialization
    void Start() {
        CheckForChangeInAgents();
        // slot.trigger = comboString.ToArray();
    }

    // Update is called once per frame
    void Update () {
        for(int i = 0; i < gbs.Length; i++) {
            if(gbs[i].IsButtonDown()) {
                cTime = comboTimer;
                currentInputString = gbs[i].button;
                currentInputStringCombo.Add(gbs[i].button);
            }
        }
        /*if(Input.GetButtonDown("LightPunch")) {
            currentInputString = "LightPunch";
            currentInputStringCombo.Add("LightPunch");
            cTime = comboTimer;
        }

        if (Input.GetButtonDown("LightKick")) {
            currentInputString = "LightKick";
            currentInputStringCombo.Add("LightKick");
            cTime = comboTimer;
        }*/
        if (cTime > 0) {
            cTime -= Time.deltaTime;
        }

         if(cTime <= 0) {
             currentInputStringCombo.Clear();

         }
         

        comboArray = currentInputStringCombo.ToArray();

        if(currentInputStringCombo.IsEqual(comboString)) {
            Debug.Log("ComboTriggered!");
            return;
        }


    }

    void CheckForChangeInAgents() {
        if (numAgents == agentList.Length) {
            return;
        }
        Debug.Log("Initiating change in agent list.");
        agentList = FindObjectsOfType<AttackAgent>();
        numAgents = agentList.Length;

    }

}
