using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightLogic : MonoBehaviour {
    public BoxCollider2D hitbox;
    public BoxCollider2D hurtbox;
    public LayerMask mask;

    public Dictionary<FightLogic, bool> allLogicsHit;
    public static FightLogic[] allLogics;
    public static FightLogic instance;
    public bool[] haveBeenHit;

    public float attackTimer = 0f;
    public bool isAttacking = false;

    public FightLogic target;

	// Use this for initialization
	void Start () {
        if (instance == null) {
            instance = this;
            Debug.Log("Initializing...");
            allLogics = GameObject.FindObjectsOfType<FightLogic>();
            haveBeenHit = new bool[allLogics.Length];
            Debug.Log("All logic objects found. There are " + allLogics.Length + " objects.");
        }
	}
	
	// Update is called once per frame
	void Update () {
        /* Rect rect = new Rect(((Vector2)hurtbox.transform.position + hurtbox.offset) - hurtbox.size, hurtbox.size * 2);
         Rect otherrect = new Rect(((Vector2)target.hitbox.transform.position + target.hitbox.offset) - target.hitbox.size, target.hitbox.size * 2);
         if(rect.Overlaps(otherrect)) {
             Debug.Log(transform.name +" Hit");
         }*/
        if(Input.GetButtonUp("Jump") && !isAttacking) {
            attackTimer = 3f;
            isAttacking = true;
            for(int i = 0; i < haveBeenHit.Length; i++) {
                haveBeenHit[i] = false;
            }
        }
        if (isAttacking) {
            for(int i = 0; i < allLogics.Length; i++) {
                if (hurtbox.bounds.Intersects(allLogics[i].hitbox.bounds)) {
                    if (allLogics[i] != this && !haveBeenHit[i]) {
                        Debug.Log(transform.name + " has hit " + allLogics[i].transform.name);
                        haveBeenHit[i] = true;
                    }
                }
            }
            attackTimer -= Time.deltaTime;
        }

        if(attackTimer <= 0f && isAttacking) {
            isAttacking = false;
        }



        /* RaycastHit2D[] hits = Physics2D.BoxCastAll((Vector2)hurtbox.transform.position + hurtbox.offset, hurtbox.size, 0f, Vector2.zero, 0f, mask);
         foreach(RaycastHit2D hit in hits) {
             if (hit) {
                 if (hit.collider != hitbox) {
                     Debug.Log(gameObject.name + " hit " + hit.collider.name);
                 }
             }
         }*/


    }
}
