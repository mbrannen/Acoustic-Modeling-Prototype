using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class modeTone : MonoBehaviour {
	public synth player;
	public float frequency;
	public float noteIndexf;
	// Use this for initialization
	void Start () {
		float freqConv1 = frequency/440;
		frequency = float.Parse(gameObject.GetComponent<Text>().text);
		noteIndexf = Mathf.Log(freqConv1,1.0594630943f);
	
	}
	
	// Update is called once per frame
	void Update () {
		frequency = float.Parse(gameObject.GetComponent<Text>().text);
	
	}
	public void playTone(){
		if(frequency>15){
		float freqConv1 = frequency/440;
		noteIndexf = Mathf.Log(freqConv1,1.0594630943f); 
			player.playTone(noteIndexf+69);
		
		}
		else{
			player.playTone(0);

		}
		//Debug.Log ("Mouse Entered!");

	}
	public void stopTone(){
		player.stopTone(frequency);
		//Debug.Log ("Mouse Exited!");

	}
}
