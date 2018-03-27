using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisButtonTest : MonoBehaviour {
    public GameButton[] buttons;
    bool held, pushed, lifted;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        foreach (GameButton button in buttons) {
            held = button.IsButtonPressed();
            lifted = button.IsButtonUp();
            pushed = button.IsButtonDown();



            if (pushed == true) Debug.Log(button.name + " is pushed: " + pushed);
            if (lifted) Debug.Log(button.name + " is lifted: " + lifted);
            // Debug.Log("Button is held: " + held);

        }
	}
}
