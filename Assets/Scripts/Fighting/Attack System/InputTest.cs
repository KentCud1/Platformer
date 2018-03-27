using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour {
    public string lastInput;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        List<GameButton> gl = GameButtonManager.gameButtons;
		for(int i = 0; i < gl.Count; i++) {
            if(gl[i].IsButtonDown()) {
                lastInput = gl[i].name;
            }
        }
	}
}
