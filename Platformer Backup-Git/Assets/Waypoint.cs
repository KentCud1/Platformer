using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : IEquatable<Waypoint> {


    public Vector2 position;
    public List<Waypoint> neigbors;
    public List<string> action;
    public  int fScore, gScore, hScore;
    public Waypoint parent = null;


    public override bool Equals(object obj) {
        if (obj == null) return false;
        Waypoint objAsWaypoint = obj as Waypoint;
        if (objAsWaypoint == null) return false;
        else return Equals(objAsWaypoint);
    }

    public bool Equals(Waypoint other) {
        return (position == other.position);
    }

    public Waypoint(Vector2 pos) {
        position = pos;
        neigbors = new List<Waypoint>();
        action = new List<string>();
        hScore = fScore = gScore = 0;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
