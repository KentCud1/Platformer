using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAgent : MonoBehaviour {
    public List<Collider2D> hitboxes = new List<Collider2D>();
    public List<Collider2D> hurtboxes = new List<Collider2D>();
    List<Collider2D> other = new List<Collider2D>();
    public bool canAttack;

    [HideInInspector]
    public AttackAgent abilityAgent;

    public Ability currentAttack;

    public int hp = 100;

    float attackTime = .25f, aTime, reloadTime = 1.25f, rTime;

	// Use this for initialization
	void Start () {
        abilityAgent = GetComponent<AttackAgent>();
        SetCollidersAsHitboxTypes();
	}
	
	// Update is called once per frame
	void Update () {
        SetCollidersAsHitboxTypes();
        AttackAndSetTimer();
	}

    private void OnEnable() {
        BattleController.numBattleAgents++;

    }

    private void OnDisable() {
        BattleController.numBattleAgents--;
    }

    void AttackAndSetTimer() {
            if (aTime > 0) {
                aTime -= Time.deltaTime;
            }
            else {
                for (int i = 0; i < hurtboxes.Count; i++) {
                    hurtboxes[i].enabled = false;
                }
            }
            if (rTime > 0) {
                rTime -= Time.deltaTime;
            }

        if (canAttack) {
            if (Input.GetButtonDown("Fire1")) {
                if (rTime <= 0) {
                    for (int i = 0; i < hurtboxes.Count; i++) {
                        hurtboxes[i].enabled = true;
                    }
                    BattleController.ClearFromAttackList(this);
                    aTime = attackTime;
                    rTime = reloadTime;
                }
            }
        }
    }

    void SetCollidersAsHitboxTypes() {
        Collider2D[] temp = GetComponentsInChildren<Collider2D>();
        if(temp.Length == (hitboxes.Count + hurtboxes.Count + other.Count)) {
            return;
        }
        if(temp.Length < (hitboxes.Count + hurtboxes.Count + other.Count)) {
            hitboxes.Clear();
            hurtboxes.Clear();
        }
        for(int i = 0; i < temp.Length; i++) {
            if(hitboxes.Contains(temp[i]) || hurtboxes.Contains(temp[i])) {
                continue;
            }
            if(temp[i].gameObject.layer == LayerMask.NameToLayer("Hitbox")) {
                hitboxes.Add(temp[i]);
            }
            else if(temp[i].gameObject.layer == LayerMask.NameToLayer("Hurtbox")) {
                hurtboxes.Add(temp[i]);
            }
            else {
                other.Add(temp[i]);
            }

        }
        Debug.Log(name + " Hitboxes: " + hitboxes.Count + " Hurtboxes: " + hurtboxes.Count);
    }

}
