using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Player))]
public class AiInput : MonoBehaviour {

    public Transform aStar;
    public Transform target;
    List<Waypoint> walkList;
    bool jump;
    bool oldJump;
    int horizontalInput;
    int verticalInput;
    Player player;
    bool isFollowingPath;
    int indexOfPath;
    float timer;

	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
	}

    // Update is called once per frame
    void LateUpdate() {
       // walkList = aStar.GetComponent<AStarPath>().finalPath;
      //  Debug.Log(walkList.Count);
      //  FollowPath(.36f);
    }

    void FollowPath(float timeStep) {
        int i = indexOfPath;
        int c = 0;
 

        if (!isFollowingPath) {
            i = 0;
            c = 0;
            isFollowingPath = true;
            timer = timeStep;
        }

                string action = walkList[i].action[walkList[i].neigbors.FindIndex(p => p.Equals(walkList[i + 1]))];
                Debug.Log("Index: " + i + " Waypoint: " + walkList[i].position + " Next: " + walkList[i + 1].position + " Action: " + action);
                if (action == "Left") {
                    jump = false;
                    horizontalInput = -1;
                }
                else if (action == "Right") {
                    jump = false;
                    horizontalInput = 1;
                }
                else if (action == "Jump Left") {
                    jump = true;
                    horizontalInput = -1;
                }
                else if (action == "Jump Right") {
                    jump = true;
                    horizontalInput = 1;
                }
                else {
                    jump = false;
                    horizontalInput = 0;
                }
            timer -= Time.deltaTime;

            if(timer <= 0 && i < walkList.Count - 2) {
                timer = timeStep;
                i++;
            }
            if(c > 100) {
                Debug.Log("Ended by counter");
                
            }
        indexOfPath = i;

    }

    private void Update() {

        Vector2 directionalInput = new Vector2(horizontalInput, verticalInput);
        player.SetDirectionalInput(directionalInput);

        if(jump) {
            player.OnJumpInputDown();
        }
        if (false) {//Input.GetKeyUp(KeyCode.Space)) {
            player.OnJumpInputUp();
        }
    }
}
