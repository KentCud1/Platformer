using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMapMaker : MonoBehaviour {

    GameObject[] mapPlatforms;
    public static List<Waypoint> waypoints;
    public LayerMask collisionMask;

    private void Awake() {
        waypoints = new List<Waypoint>();
        mapPlatforms = GameObject.FindGameObjectsWithTag("map");

        CreateWaypoints();
        GetWalkingNeigbors();
        GetJumpingNeighbors();
        GetFallingNeighbors();
    }
    // Use this for initialization 
    void Start () {




    }

    void CreateWaypoints() {
        foreach (GameObject go in mapPlatforms) {
            int numWaypoints = (int)go.transform.localScale.x;
            for (int i = 0; i < numWaypoints; i++) {
                Vector2 tempVect = new Vector2((go.transform.position.x - go.transform.localScale.x / 2) + (i + .5f), go.transform.position.y);
                RaycastHit2D hit = Physics2D.Raycast(tempVect, Vector2.up, 1f, collisionMask);


                if (!hit || (hit && hit.transform == go.transform)) {
                    RaycastHit2D hit2 = Physics2D.Raycast(tempVect + Vector2.up, Vector2.zero, 1f, collisionMask);
                    if (!hit2) {
                        waypoints.Add(new Waypoint(tempVect + Vector2.up));
                    }
                }
            }
        }
    }

    void GetWalkingNeigbors() {
        for (int i = 0; i < waypoints.Count; i++) {
            Waypoint tempLeft = new Waypoint(waypoints[i].position + Vector2.left);
            Waypoint tempRight = new Waypoint(waypoints[i].position + Vector2.right);

            if (waypoints.Contains(tempLeft)) {
                waypoints[i].neigbors.Add(waypoints.Find(p => p.Equals(tempLeft)));
                waypoints[i].action.Add("Left");
            }

            if (waypoints.Contains(tempRight)) {
                waypoints[i].neigbors.Add(waypoints.Find(p => p.Equals(tempRight)));
                waypoints[i].action.Add("Right");
            }
        }
    }

    void GetJumpingNeighbors() {
        for (int i = 0; i < waypoints.Count; i++) {
            Vector2[] movementsRight = { Vector2.up, Vector2.up, Vector2.up, Vector2.right, Vector2.right, Vector2.down, Vector2.right, Vector2.down };
            Vector2[] movementsLeft = { Vector2.up, Vector2.up, Vector2.up, Vector2.left, Vector2.left, Vector2.down, Vector2.left, Vector2.down };
            Vector2 currentPos = waypoints[i].position;

            foreach (Vector2 move in movementsLeft) {
                Vector2 newPos = currentPos + move;
                Waypoint newPoint = new Waypoint(newPos);
                RaycastHit2D hit = Physics2D.Raycast(currentPos, move, Vector2.Distance(currentPos, newPos), collisionMask);
                if (hit) {
                    break;
                }

                if (waypoints.Contains(newPoint)) {
                    waypoints[i].neigbors.Add(waypoints.Find(p => p.Equals(newPoint)));
                    waypoints[i].action.Add("Jump Left");
                }
                currentPos = newPos;

            }

            currentPos = waypoints[i].position;

            foreach (Vector2 move in movementsRight) {
                Vector2 newPos = currentPos + move;
                Waypoint newPoint = new Waypoint(newPos);
                RaycastHit2D hit = Physics2D.Raycast(currentPos, move, Vector2.Distance(currentPos, newPos), collisionMask);
                if (hit) {
                    break;
                }

                if (waypoints.Contains(newPoint)) {
                    waypoints[i].neigbors.Add(waypoints.Find(p => p.Equals(newPoint)));
                    waypoints[i].action.Add("Jump Right");

                }
                currentPos = newPos;

            }

        }
    }

    void GetFallingNeighbors() {
        for (int i = 0; i < waypoints.Count; i++) {
            RaycastHit2D hitLeft = Physics2D.Raycast(waypoints[i].position, Vector2.left, 1, collisionMask);

            if (!hitLeft) {
                Waypoint tempLeft = new Waypoint(waypoints[i].position + Vector2.left);
                if (!waypoints.Contains(tempLeft)) {

                    Vector2 tempPos = tempLeft.position;
                    int counter = 0;
                    while (true) {
                        if (counter >= 15) {
                            break;
                        }

                        tempPos += Vector2.down;

                        Waypoint point = new Waypoint(tempPos);
                        if (waypoints.Contains(point)) {
                            waypoints[i].neigbors.Add(waypoints.Find(p => p.Equals(point)));
                            waypoints[i].action.Add("Left");
                            break;
                        }

                        counter++;
                    }
                }
            }

            RaycastHit2D hitRight = Physics2D.Raycast(waypoints[i].position, Vector2.right, 1, collisionMask);

            if (!hitLeft) {
                Waypoint tempRight = new Waypoint(waypoints[i].position + Vector2.right);
                if (!waypoints.Contains(tempRight)) {

                    Vector2 tempPos = tempRight.position;
                    int counter = 0;
                    while (true) {
                        if (counter >= 15) {
                            break;
                        }

                        tempPos += Vector2.down;

                        Waypoint point = new Waypoint(tempPos);
                        if (waypoints.Contains(point)) {
                            waypoints[i].neigbors.Add(waypoints.Find(p => p.Equals(point)));
                            waypoints[i].action.Add("Left");
                            break;
                        }

                        counter++;
                    }
                }
            }
        }
    }
    
	// Update is called once per frame
	void Update () {
        if (waypoints.Count > 0) {
            foreach (Waypoint w in waypoints) {
                Debug.DrawLine(w.position, w.position + Vector2.up, Color.green);
                foreach(Waypoint wp in w.neigbors) {
                 //   Debug.DrawLine(w.position, wp.position,Color.magenta);
                    //DrawArrow(w.position, wp.position);
                }
            }
        }
        else
            Debug.Log("Waypoints empty");
    }

    void DrawArrow(Vector2 pos, Vector2 otherPos) {
        Debug.DrawLine(pos, otherPos);

        Vector2 dif = pos - otherPos;
        float angle = Mathf.Atan(dif.y / dif.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 180;
        Debug.Log(angle);
        float leftAngle = angle - 30;
        float rightAngle = angle + 30;

        Vector2 leftWing = otherPos + new Vector2(Mathf.Cos(leftAngle * Mathf.Deg2Rad) * .25f, Mathf.Sin(leftAngle * Mathf.Deg2Rad) * .25f);
        Vector2 rightWing = otherPos + new Vector2(Mathf.Cos(rightAngle * Mathf.Deg2Rad) * .25f, Mathf.Sin(rightAngle * Mathf.Deg2Rad) * .25f);

        Debug.DrawLine(otherPos, leftWing);
        Debug.DrawLine(otherPos, rightWing);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        for(int i = 0; i < waypoints.Count; i++) {
            Gizmos.DrawSphere(waypoints[i].position, .25f);
        }

    }
}
