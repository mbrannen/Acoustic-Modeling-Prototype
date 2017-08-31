﻿using UnityEngine;
using System.Collections;

public class mouseHandlerW1 : MonoBehaviour {

	Animator anim;
	public bool wallstate = false;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		stateUpdate ();
	}
	void OnMouseEnter () {
	}

	void OnMouseExit () {
	}
	void stateUpdate(){
		if (wallstate == false) {

			if (Input.GetKeyUp (KeyCode.Alpha1)) {
				anim.SetBool("state", true);
				wallstate = true;
			}
		} else {
			if (wallstate) {
				if (Input.GetKeyUp (KeyCode.Alpha1)) {
					anim.SetBool("state", false);
					wallstate = false;	
			}
		}


		}
	}
}
