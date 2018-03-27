using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {
    public static int numBattleAgents, numAttackAgents;
    public static BattleAgent[] battleAgentList = new BattleAgent[0];
    public static AttackAgent[] attackAgentList = new AttackAgent[0];
    public LayerMask hitboxMask;
    public Collider2D[] activeHurtboxes;
    Dictionary<Collider2D, BattleAgent> colliderToAgentCache;
    Dictionary<BattleAgent, AttackAgent> battleToAttackCache = new Dictionary<BattleAgent, AttackAgent>();
    public static List<LandedAttack> openAttacksList = new List<LandedAttack>();
    public static List<LandedAttack> resolvedAttacksList = new List<LandedAttack>();
    public GameObject animObject;

    // Use this for initialization
    void Start() {
        CheckForChangeInAgents();
    }

    // Update is called once per frame
    void Update() {
        CheckForChangeInAgents();
        SetBattleToAttackCache();
        SetActiveHurtboxes();
        CheckForCollisionsOnHurtboxes();
        ResolveAttacks();

        if (Input.GetButtonUp("Jump")) {
            ClearFromAttackList(battleAgentList[0]);
        }

    }

    void SetBattleToAttackCache() {
        for (int i = 0; i < battleAgentList.Length; i++) {
            if (battleAgentList.Length >= 1) {
                if (battleToAttackCache.ContainsKey(battleAgentList[i])) {
                    continue;
                }
            }
            AttackAgent tempAttack = null;
            for (int j = 0; j < attackAgentList.Length; j++) {
                if (attackAgentList[j].transform == battleAgentList[i].transform) {
                    tempAttack = attackAgentList[j];
                    break;
                }
            }
            battleToAttackCache.Add(battleAgentList[i], tempAttack);
        }
    }

    void CheckForChangeInAgents() {
        if (numBattleAgents == battleAgentList.Length && numAttackAgents == attackAgentList.Length) {
            return;
        }
        Debug.Log("Initiating change in agent list.");
        battleAgentList = FindObjectsOfType<BattleAgent>();
        numBattleAgents = battleAgentList.Length;
        attackAgentList = FindObjectsOfType<AttackAgent>();
        numAttackAgents = attackAgentList.Length;
        Debug.Log("Number of battle agents: " + numBattleAgents);
        Debug.Log("Number of attack agents: " + numAttackAgents);

    }

    void SetActiveHurtboxes() {
        List<Collider2D> activeTempList = new List<Collider2D>();
        if (battleAgentList == null) {
            Debug.Log("AgentList is null.");
            return;
        }
        for (int i = 0; i < BattleController.battleAgentList.Length; i++) {
            for (int j = 0; j < BattleController.battleAgentList[i].hurtboxes.Count; j++) {
                if (BattleController.battleAgentList[i].hurtboxes[j].enabled == true) {
                    activeTempList.Add(BattleController.battleAgentList[i].hurtboxes[j]);
                }
            }
        }
        if (activeHurtboxes.Length == activeTempList.Count) {
            return;
        }
        Debug.Log("There are " + activeTempList.Count + " active hurtboxes.");
        activeHurtboxes = activeTempList.ToArray();
    }

    void CheckForCollisionsOnHurtboxes() {
        for (int i = 0; i < activeHurtboxes.Length; i++) {
            RaycastHit2D[] hits = Physics2D.BoxCastAll((Vector2)activeHurtboxes[i].transform.position + activeHurtboxes[i].offset, activeHurtboxes[i].bounds.size, 0f, Vector2.zero, 1f, hitboxMask);
            for (int j = 0; j < hits.Length; j++) {
                BattleAgent atk = null, def = null;
                for (int k = 0; k < battleAgentList.Length; k++) {
                    if (battleAgentList[k].hurtboxes.Contains(activeHurtboxes[i])) {
                        atk = battleAgentList[k];
                    }
                    if (battleAgentList[k].hitboxes.Contains(hits[j].collider)) {
                        def = battleAgentList[k];
                    }
                }
                if ((atk == def) || atk == null || def == null) {
                    continue;
                }
                LandedAttack LA = new LandedAttack(atk, def);
                LA.hit = hits[j];
                if (!openAttacksList.Contains(LA) && !resolvedAttacksList.Contains(LA)) {
                    openAttacksList.Add(LA);
                    Debug.Log(atk.name + " hit " + def.name + " at " + LA.hit.point);
                }
            }
        }
    }

    void ResolveAttacks() {
        for (int i = 0; i < openAttacksList.Count; i++) {
            if (openAttacksList[i].attacker.abilityAgent != null) {
                Ability ab = openAttacksList[i].attacker.abilityAgent.currentAttack;
                if (ab != null) {
                    openAttacksList[i].defender.hp -= ab.damage;
                    Debug.Log(openAttacksList[i].defender + " has been hit by " + ab.name);
                }
                else {
                    openAttacksList[i].defender.hp -= 10;
                    Debug.Log("No attack has been set. Returning base damage output.");
                }
            }
            else {
                Debug.Log("No attack agent on object. No damage done.");
            }
            Debug.Log(openAttacksList[i].defender.name + " has " + openAttacksList[i].defender.hp + " health.");
            Instantiate(animObject, openAttacksList[i].hit.point, Quaternion.identity);
            resolvedAttacksList.Add(openAttacksList[i]);
            openAttacksList.Remove(openAttacksList[i]);
        }
    }

    public static void ClearFromAttackList(BattleAgent BA) {
        if (openAttacksList.Count > 0) {
            for (int i = 0; i < openAttacksList.Count; i++) {
                if (openAttacksList[i].attacker == BA) {
                    openAttacksList.Remove(openAttacksList[i]);
                }
            }
        }
        if (resolvedAttacksList.Count > 0) {
            for (int i = 0; i < resolvedAttacksList.Count; i++) {
                if (resolvedAttacksList[i].attacker == BA) {
                    resolvedAttacksList.Remove(resolvedAttacksList[i]);
                }
            }
        }
    }

    public static void ClearAllAttacks() {
        openAttacksList.Clear();
        resolvedAttacksList.Clear();
    }
}

public struct LandedAttack {
    public BattleAgent attacker;
    public BattleAgent defender;

    public RaycastHit2D hit;

    public LandedAttack(BattleAgent _attkr, BattleAgent _dfndr) {
        attacker = _attkr;
        defender = _dfndr;
        hit = new RaycastHit2D();
    }

    public override bool Equals(object obj) {
        LandedAttack LAObj = (LandedAttack)obj;
        return (attacker.Equals(LAObj.attacker)) && (defender.Equals(LAObj.defender));
    }

    public override int GetHashCode() {
        return attacker.GetHashCode() + defender.GetHashCode();
    }

    public static bool operator ==(LandedAttack LA1, LandedAttack LA2) {
        return ((LA1.attacker == LA2.attacker) && (LA1.defender == LA2.defender));
    }

    public static bool operator !=(LandedAttack LA1, LandedAttack LA2) {
        return ((LA1.attacker != LA2.attacker) || (LA1.defender != LA2.defender));
    }
}
