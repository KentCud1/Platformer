using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewButton", menuName = "GameButton")]
public class GameButton : ScriptableObject {

    public string button;
    public bool isAxis;
    public bool isPositive;

    bool previousAxisBool = false;
    

    public bool IsButtonDown() {
        if(isAxis) {
            if(previousAxisBool == true) {
                if ((Input.GetAxisRaw(button) <= 0 && isPositive) ||(Input.GetAxisRaw(button) >= 0 && !isPositive)) previousAxisBool = false;
                return false;
            }
            float butInput = Input.GetAxisRaw(button); 
            if(isPositive) {
                return (previousAxisBool = Input.GetAxisRaw(button) > 0);
            }
            else {
                return (previousAxisBool = Input.GetAxisRaw(button) < 0);
            }
        }
        return Input.GetButtonDown(button);
    }

    public bool IsButtonUp() {
        if (isAxis) {
            if(previousAxisBool == false) {
                return false;
            }
            if (isPositive) {
                return (Input.GetAxisRaw(button) <= 0);
            }
            else {
                return (Input.GetAxisRaw(button) >= 0);
            }
        }
        return Input.GetButtonUp(button);
    }

    public bool IsButtonPressed() {
        Debug.Log("Previous Axis: " +previousAxisBool);
        if (isAxis) {
            if (isPositive) {
                return (Input.GetAxisRaw(button) > 0);
            }
            else {
                return (Input.GetAxisRaw(button) < 0);
            }
        }
        return Input.GetButton(button);
    }
    
    public bool IsEqual(GameButton other) {
        return (name == other.name);
    }

}
