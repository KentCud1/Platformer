using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonManager : MonoBehaviour {
    public static List<GameButton> gameButtons;
    public List<GameButton> buttonList;
    static GameButtonManager instance;
	// Use this for initialization
	void Awake () {
        if(instance == null) {
            instance = this;
        }
        if(instance != this) {
            Destroy(this);
        }
        gameButtons = buttonList;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
