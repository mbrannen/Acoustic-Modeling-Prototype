using UnityEngine;
using System.Collections;

public class sphereDrag : MonoBehaviour {
	//float height = 5f;
	float posX;
	float posY;
	public float cDist;
	public float w1Dist;
	Vector3 dist;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//RaycastHit rayHit;
		//float cDist;
		//Vector3 up = transform.TransformDirection(Vector3.up)*10;
		//Ray upRay = new Ray(transform.position, Vector3.up);
		//Debug.DrawRay(transform.position, up,Color.green);
		//Debug.Log ("x: "+source.transform.position.x+"y: "+source.transform.position.y+"z: "+source.transform.position.z);
		//if(Physics.Raycast(transform.position,up, out rayHit)){
		//	Debug.Log (rayHit.distance+ " " +rayHit.collider.name);
		//	cDist = rayHit.distance;
			
	//	}
		//RaycastHit rayHit2;
		//float w1Dist;
		//Vector3 right = transform.TransformDirection(Vector3.right)*10;
		//Ray rightRay = new Ray(transform.position, Vector3.right);
		//Debug.DrawRay(transform.position, right,Color.blue);
		//Debug.Log ("x: "+source.transform.position.x+"y: "+source.transform.position.y+"z: "+source.transform.position.z);
		//if(Physics.Raycast(transform.position,right, out rayHit2)){
		//	Debug.Log (rayHit2.distance+ " " +rayHit2.collider.name);
		//	w1Dist = rayHit2.distance;
			
	//	}
	
	}
	void OnMouseDown(){
		dist = Camera.main.WorldToScreenPoint(transform.position);
		posX = Input.mousePosition.x - dist.x;
		posY = Input.mousePosition.y - dist.y;
	}
	void OnMouseDrag()
	{
		Vector3 mousePosition = new Vector3(Input.mousePosition.x - posX,Input.mousePosition.y - posY, dist.z);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
		transform.position = objPosition;
	}
}
