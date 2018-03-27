using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordAnim : MonoBehaviour {
    Animator animator;
    Animation currentAnim;
    float timer;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        currentAnim = GetComponent<Animation>();
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(currentAnim.clip.length);
		if(currentAnim.clip.length < timer) {
            Destroy(transform.gameObject);
        }
        timer += Time.deltaTime;
	}
}
