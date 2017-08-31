using UnityEngine;
using System.Collections;

public class hideWalls : MonoBehaviour {

	public bool state;
	public GameObject wall3;
	public GameObject wall4;
	public GameObject ceiling;

	// Use this for initialization
	void Start () {
		state = false; //walls are not hidden
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}
	public void showHide(){
		if(state){ //if walls are hidden
			state = false; //then set the state to not hidden
			wall3.GetComponent<MeshRenderer>().enabled = true;
			wall4.GetComponent<MeshRenderer>().enabled = true;
			ceiling.GetComponent<MeshRenderer>().enabled = true;
			//wall3.SetActive(true); //show all the walls
			//wall4.SetActive(true);
			//ceiling.SetActive(true);
		}else{ //if the wals are not hidden
			state = true; // then set the state to hidden
		//	wall3.SetActive(false); //hide all the walls;
			wall3.GetComponent<MeshRenderer>().enabled = false;
			wall4.GetComponent<MeshRenderer>().enabled = false;
			ceiling.GetComponent<MeshRenderer>().enabled = false;
			//wall4.SetActive(false);
			//ceiling.SetActive(false);

		}

	}
}
