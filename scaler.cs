using UnityEngine;
using System.Collections;

public class scaler : MonoBehaviour {

	public GameObject surface;
	public GameObject wall1;
	public Component wall1T;
	public main scaleScript;
	// Use this for initialization
	void Start () {
		surface = GameObject.Find ("Canvas");
		wall1 = GameObject.Find ("wall (1)");
		wall1T = wall1.GetComponent<Transform> ();
		scaleScript = surface.GetComponent<main>();

	}
	
	// Update is called once per frame
	void Update () {

	}
}
