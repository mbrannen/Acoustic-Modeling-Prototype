using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;
using System.Collections;

public class spaceMenu : MonoBehaviour {

	//public KeyCode space;
	//public GameObject main;
	public GameObject menu;
	// Use this for initialization
	void Start () {
		//gameObject.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			Debug.Log("space pressed");
			menu.SetActive(true);
			//main.SetActive(false);

		}
		if(Input.GetKeyUp(KeyCode.Space)){
			menu.SetActive(false);
			//main.SetActive(true);

		}
	
	}
}
