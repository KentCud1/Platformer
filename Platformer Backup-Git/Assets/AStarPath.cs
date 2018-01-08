using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPath : MonoBehaviour {

    public Transform start;
    public Transform target;

    Waypoint startPoint;
    Waypoint endPoint;

    List<Waypoint> waypointMap;

    List<Waypoint> openList;
    List<Waypoint> closedList;

   public List<Waypoint> finalPath;


	// Use this for initialization
	void Start () {
        waypointMap = WaypointMapMaker.waypoints;

        Pathfinding();
	}


	
	// Update is called once per frame
	void Update () {
        if (finalPath.Count > 0) {
            foreach (Waypoint w in finalPath) {
                if (w.parent != null) {
                    Debug.DrawLine(w.position, w.parent.position, Color.yellow);
                    //DrawArrow(w.position, wp.position);
                }
            }
        }
        else
            Debug.Log("Waypoints empty");

        if(Input.GetButtonUp("Jump")) {
            Pathfinding();
        }
    }

    void Pathfinding() {

        foreach(Waypoint w in waypointMap) {
            w.parent = null;
        }
        startPoint = GetClosest(start.position);
        endPoint = GetClosest(target.position);
        openList = new List<Waypoint>();
        closedList = new List<Waypoint>();
        finalPath = new List<Waypoint>();

        Waypoint currentPoint;

        startPoint.hScore = (int)(Mathf.Abs(startPoint.position.x - endPoint.position.x) + Mathf.Abs(startPoint.position.y - endPoint.position.y));
        startPoint.fScore = startPoint.gScore + startPoint.hScore;
        openList.Add(startPoint);
        while (openList.Count > 0) {
            currentPoint = LowestFCost(openList);

            closedList.Add(currentPoint);
            openList.Remove(currentPoint);

            if (closedList.Contains(endPoint)) {
                finalPath = FindPath(closedList.Find(p => p.Equals(endPoint)));
                break;
            }

            List<Waypoint> neighbors = currentPoint.neigbors;

            foreach (Waypoint w in neighbors) {
                if (closedList.Contains(w))
                    continue;
                if (!openList.Contains(w)) {
                    w.gScore = currentPoint.gScore + 1;
                    w.hScore = (int)(Mathf.Abs(w.position.x - endPoint.position.x) + Mathf.Abs(w.position.y - endPoint.position.y));
                    w.fScore = w.gScore + w.hScore;
                    w.parent = currentPoint;
                    openList.Add(w);
                }
                else {
                    int newH = (int)(Mathf.Abs(w.position.x - endPoint.position.x) + Mathf.Abs(w.position.y - endPoint.position.y));
                    int newG = currentPoint.gScore + 1;
                    int newF = newH + newG;
                    if (newF < openList.Find(p => p.Equals(w)).fScore)
                        openList.Find(p => p.Equals(w)).parent = currentPoint;
                }
            }

        }
    }

    List<Waypoint> FindPath(Waypoint point) {
        Waypoint currentPoint = closedList.Find(p => p.Equals(point));
        List<Waypoint> list = new List<Waypoint>();
        do {
            list.Add(currentPoint);

            currentPoint = closedList.Find(p => p.Equals(currentPoint.parent));
            if(currentPoint.parent == null) {
                list.Add(currentPoint);
                break;
            }
        }while (currentPoint.parent != null);
        Debug.Log(list.Count);
        list.Reverse();
        return list;
    }


    Waypoint LowestFCost(List<Waypoint> list) {
        Waypoint lowest = null;
        float lowestNum = Mathf.Infinity;
        foreach(Waypoint w in list) {
            if(w.fScore < lowestNum) {
                lowestNum = w.fScore;
                lowest = w;
            }
        }

        if (lowest != null) {
            return lowest;
        }

        return null;

    }

    Waypoint GetClosest(Vector2 pos) {
        float closestDist = Mathf.Infinity;
        Waypoint closest = null;
        foreach (Waypoint w in waypointMap) {
            float dist = Mathf.Abs(pos.x - w.position.x) + Mathf.Abs(pos.y - w.position.y);
            if(dist < closestDist) {
                closestDist = dist;
                closest = w;
            }
        }
        if(closest != null) {
            return closest;
        }

        return null;
    }
}
