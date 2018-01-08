using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AnimationTest : MonoBehaviour {

    [HideInInspector]
        public Bounds bounds;
    
    public float framesInAnimation;
    public float framesPerSecond;
    public int currentFrame;
    public float animationTime;
    public AnimationClip clip;

    public BoxCollider2D coll;
    public BoxCollider2D[] colls;
    public AnimationEvent evt;
	// Use this for initialization
	void Start () {
        evt = new AnimationEvent();
        bounds = new Bounds(transform.position, Vector3.one);
        coll = GetComponent<BoxCollider2D>();
        framesPerSecond = clip.frameRate;

    }
	
    public void AddEventInFrame() {
        


        clip.AddEvent(evt);
    }

    public void PrintEvent(string s) {
        print("TestWorks!!!!");
    }

	// Update is called once per frame
	void Update () {
        if (clip != null && clip.length > 0) {
            animationTime = clip.length;
            evt.stringParameter = "Test";
            evt.functionName = "PrintEvent";
            evt.time = (float)currentFrame / framesPerSecond;
            framesInAnimation = (int)(framesPerSecond * animationTime);

        }
    }
}
