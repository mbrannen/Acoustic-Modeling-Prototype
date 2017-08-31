using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class dropDownControl : MonoBehaviour {

	public Dropdown menu;
	public Texture[] tex;
	public GameObject surface;
	public dataStorage data;
	public Texture t1;
	public Texture t2;
	public Texture t3;


	// Use this for initialization
	void Start(){
		GameObject surface = GameObject.Find("wall1");
		dataStorage data = GameObject.Find("dataHandler").GetComponent<dataStorage>();
		Debug.Log(menu.value);
		int i = 3;
		i = data.libraryCount;
		Debug.Log (i);
		tex = new Texture[data.libraryCount];
		tex[0] = t1;
		tex[1] = t2;
		tex[2] = t3;


		Debug.Log (tex.Length);
		surface.GetComponent<Renderer>().material.mainTexture = tex [menu.value];
	//	menu1.onValueChanged.AddListener (delegate {
	//		surfaceUpdate (menu1);
		Debug.Log(surface.name);

	//	});
	}
	
	// Update is called once per frame
	void Update () {
		//Dropdown menu = GameObject.Find ("surfaceMenu1");
		dataStorage data = GameObject.Find("dataHandler").GetComponent<dataStorage>();
		Debug.Log(menu.value);
		Debug.Log (tex.Length);
		switch(menu.value){
		case 0:
			surface.GetComponent<Renderer>().material.mainTexture = tex[0]; break;
		case 1:
			surface.GetComponent<Renderer>().material.mainTexture = tex[1]; break;
		case 2:
			surface.GetComponent<Renderer>().material.mainTexture = tex[2]; break;
		default: break;
		
		}

	}


}
