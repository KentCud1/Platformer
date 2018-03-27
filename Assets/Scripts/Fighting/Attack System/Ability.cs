using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewAbility",menuName ="Ability")]
public class Ability : ScriptableObject {

    public enum AttackType {
        DAMAGE,
        HEAL,
        BUFF,
        DEBUFF
    }

    public string name;
    public string description;
    public int damage;
    public Animation anim;

    
    public void Trigger() {
        Debug.Log(name + " attackTriggered");
        if(anim != null) {

        }
        else {
            Debug.Log("Ability " + name + " does not have animation");
        }
    }
}
