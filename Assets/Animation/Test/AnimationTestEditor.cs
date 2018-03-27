using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;


[CustomEditor(typeof(AnimationTest))]
[CanEditMultipleObjects]
public class AnimationTestEditor : Editor {

    BoxBoundsHandle m_BoundsHandle = new BoxBoundsHandle(0);
    BoxBoundsHandle m_bounds2 = new BoxBoundsHandle(1);

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        AnimationTest animTest = (AnimationTest)target;
        animTest.colls = animTest.GetComponents<BoxCollider2D>();
        Mathf.Clamp(animTest.currentFrame, 0, animTest.framesInAnimation);
        if(GUILayout.Button("Add Event")) {
            AnimationTest at = Selection.activeGameObject.GetComponent<AnimationTest>();
            Collider2D collider = Selection.activeGameObject.GetComponent<Collider2D>();
            AnimationEvent[] events = AnimationUtility.GetAnimationEvents(at.clip);
            AnimationEvent[] newEv = new AnimationEvent[events.Length + 1];
            for (int i = 0; i < events.Length; i++) {
                newEv[i] = events[i];
            }
            newEv[newEv.Length - 1] = at.evt;
            AnimationUtility.SetAnimationEvents(at.clip, newEv);
        }
    }

    protected virtual void OnSceneGUI() {
        AnimationTest animTest = (AnimationTest)target;
        m_BoundsHandle.axes = PrimitiveBoundsHandle.Axes.X | PrimitiveBoundsHandle.Axes.Y;
        m_BoundsHandle.center = animTest.bounds.center;
        m_BoundsHandle.size = animTest.bounds.size;
        m_bounds2.center = animTest.transform.position;
        m_bounds2.size = Vector3.one;

        EditorGUI.BeginChangeCheck();
        m_BoundsHandle.DrawHandle();
        m_bounds2.DrawHandle();
        if(EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(animTest, "Change Bounds");

            Bounds newBounds = new Bounds();
            newBounds.center = m_BoundsHandle.center;
            newBounds.size = m_BoundsHandle.size;
            animTest.bounds = newBounds;
            animTest.coll.offset = newBounds.center - animTest.transform.position;
            animTest.coll.size = newBounds.size;
            
            
        }
    
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
