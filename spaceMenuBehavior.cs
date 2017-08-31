using UnityEngine;
using System.Collections;
//using UnityEditor;
using UnityEngine.UI;

public class spaceMenuBehavior : MonoBehaviour {

	public GameObject surfacePanel;
	public GameObject dimensionPanel;
	public GameObject cameraPanel;
	public GameObject axialPanel;
	public GameObject rt60Panel;
	public GameObject tangentalPanel;
	public GameObject sourListPanel;
	public GameObject variousPanel;
	public GameObject sosPanel;
	public bool surfP;
	public bool dimP;
	public bool camP;
	public bool axP;
	public bool rt60P;
	public bool tanP;
	public bool sourListP;
	public bool variousP;
	public bool sosP;

	// Use this for initialization
	void Start () {
		surfP = false;
		dimP = false;
		camP = true;
		axP = false;
		rt60P = false;
		tanP = false;
		sourListP = false;
		variousP = false;
		sosP = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void showSurfaceMaterial(){
		if(surfP){
			surfP = false;
			surfacePanel.SetActive(surfP);
		}
		else{
			surfP = true;
			surfacePanel.SetActive(surfP);
		}
	}
	public void showDimensions(){
		if(dimP){
			dimP = false;
			dimensionPanel.SetActive(dimP);
		}
		else{
			dimP = true;
			dimensionPanel.SetActive(dimP);
		}
	}
	public void showCamera(){
		if(camP){
			camP = false;
			cameraPanel.SetActive(camP);
		}
		else{
			camP = true;
			cameraPanel.SetActive(camP);
		}
	}
	public void showAxial(){
		if(axP){
			axP = false;
			axialPanel.SetActive(axP);
		}
		else{
			axP = true;
			axialPanel.SetActive(axP);
		}
	}
	public void showRT60(){
		if(rt60P){
			rt60P = false;
			rt60Panel.SetActive(rt60P);
		}
		else{
			rt60P = true;
			rt60Panel.SetActive(rt60P);
		}
	}
	public void showTangental(){
		if(tanP){
			tanP = false;
			tangentalPanel.SetActive(tanP);
		}
		else{
			tanP = true;
			tangentalPanel.SetActive(tanP);
		}
	}
	public void showSourList(){
		if(sourListP){
			sourListP = false;
			sourListPanel.SetActive(sourListP);
		}
		else{
			sourListP = true;
			sourListPanel.SetActive(sourListP);
		}
	}
	public void showVarious(){
		if(variousP){
			variousP = false;
			variousPanel.SetActive(variousP);
		}
		else{
			variousP = true;
			variousPanel.SetActive(variousP);
		}
	}
	public void showSOS(){
		if(sosP){
			sosP = false;
			sosPanel.SetActive(sosP);
		}
		else{
			sosP = true;
			sosPanel.SetActive(sosP);
		}
	}

}
