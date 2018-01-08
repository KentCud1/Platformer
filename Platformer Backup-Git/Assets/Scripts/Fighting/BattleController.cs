using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {
    public static int numAgents;
    public static BattleAgent[] agentList = new BattleAgent[0];
    public LayerMask hitboxMask;
    public Collider2D[] activeHurtboxes;
    Dictionary<Collider2D, BattleAgent> colliderToAgentCache;
    public static List<LandedAttack> openAttacksList = new List<LandedAttack>();
    public static List<LandedAttack> resolvedAttacksList = new List<LandedAttack>();

	// Use this for initialization
	void Start () {
        CheckForChangeInAgents();
	}
	
	// Update is called once per frame
	void Update () {
        CheckForChangeInAgents();
        SetActiveHurtboxes();
        CheckForCollisionsOnHurtboxes();
        ResolveAttacks();

        if(Input.GetButtonUp("Jump")) {
            ClearFromAttackList(agentList[0]);
        }

	}

    void CheckForChangeInAgents() {
        if(numAgents == agentList.Length) {
            return;
        }
        Debug.Log("Initiating change in agent list.");
        agentList = FindObjectsOfType<BattleAgent>();
        numAgents = agentList.Length;
        Debug.Log("Number of agents: " + numAgents);

    }

    void SetActiveHurtboxes() {
        List<Collider2D> activeTempList = new List<Collider2D>();
        if(agentList == null) {
            Debug.Log("AgentList is null.");
            return;
        }
        for(int i = 0; i < BattleController.agentList.Length; i++) {
            for(int j = 0; j < BattleController.agentList[i].hurtboxes.Count; j++) {
                if(BattleController.agentList[i].hurtboxes[j].enabled == true) {
                    activeTempList.Add(BattleController.agentList[i].hurtboxes[j]);
                }
            }
        }
        if(activeHurtboxes.Length == activeTempList.Count) {
            return;
        }
        Debug.Log("There are " + activeTempList.Count + " active hurtboxes.");
        activeHurtboxes = activeTempList.ToArray();
    }

    void CheckForCollisionsOnHurtboxes() {
        for(int i = 0; i < activeHurtboxes.Length; i ++) {
            RaycastHit2D[] hits = Physics2D.BoxCastAll((Vector2)activeHurtboxes[i].transform.position + activeHurtboxes[i].offset, activeHurtboxes[i].bounds.size, 0f, Vector2.zero, 1f, hitboxMask);
            for(int j = 0; j < hits.Length; j++) {
                BattleAgent atk = null, def = null;
                for (int k = 0; k < agentList.Length; k++) {
                    if (agentList[k].hurtboxes.Contains(activeHurtboxes[i])) {
                        atk = agentList[k];
                    }
                    if(agentList[k].hitboxes.Contains(hits[j].collider)) {
                        def = agentList[k];
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
        for(int i = 0; i < openAttacksList.Count; i++) {
            openAttacksList[i].defender.hp -= 10;
            Debug.Log(openAttacksList[i].defender.name + " has " + openAttacksList[i].defender.hp + " health.");
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
