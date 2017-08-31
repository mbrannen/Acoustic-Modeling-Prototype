using UnityEngine;
using System.Collections;

public class lookAT : MonoBehaviour {

	public Camera cam1;
	public Camera cam2;

	public Transform target;
	void start(){
		cam1.enabled = true;
		cam2.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.C)){
			if(cam1.enabled == true){
				cam1.enabled = false;
				cam2.enabled = true;
			} else{
				cam1.enabled = true;
				cam2.enabled = false;
			}
			//cam1.enabled = !cam1.enabled;
		//	cam2.enabled = !cam2.enabled;

		}
		Vector3 relativePos = target.position - transform.position;
		transform.rotation = Quaternion.LookRotation (relativePos);
	}
}
