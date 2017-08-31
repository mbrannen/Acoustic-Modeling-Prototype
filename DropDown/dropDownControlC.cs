using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class dropDownControlC : MonoBehaviour {

	public Dropdown menu;
	public Texture[] tex;
	public GameObject surface;
	public dataStorage data;
	public Texture t1;
	public Texture t2;
	public Texture t3;
	public Texture t4;
	public Texture t5;
	public Texture t6;
	public Texture t7;
	public Texture t8;
	public Texture t9;
	public Texture t10;
	public Texture t11;
	public Texture t12;


	// Use this for initialization
	void Start(){
		GameObject surface = GameObject.Find("ceiling");
		dataStorage data = GameObject.Find("dataHandler").GetComponent<dataStorage>();
		int i = 0;
		i = data.libraryCount;
		tex = new Texture[data.libraryCount];
		tex[0] = t1;
		tex[1] = t2;
		tex[2] = t3;
		tex[3] = t4;
		tex[4] = t5;
		tex[5] = t6;
		tex[6] = t7;
		tex[7] = t8;
		tex[8] = t9;
		tex[9] = t10;
		tex[10] = t11;
		tex[11] = t12;


		Debug.Log (tex.Length);
		surface.GetComponent<Renderer>().material.mainTexture = tex [menu.value];
	//	menu1.onValueChanged.AddListener (delegate {
	//		surfaceUpdate (menu1);
		Debug.Log(surface.name);

	//	});
	}
	
	// Update is called once per frame
	void Update () {
		//Dropdown menu = GameObject.Find ("surfaceMenuF");
		if(menu.value < 12){
			surface.GetComponent<Renderer>().material.mainTexture = tex [menu.value];
		}
	/*	switch(menu.value){
		case 0:
			surface.GetComponent<Renderer>().material.mainTexture = tex[0]; break;
		case 1:
			surface.GetComponent<Renderer>().material.mainTexture = tex[1]; break;
		case 2:
			surface.GetComponent<Renderer>().material.mainTexture = tex[2]; break;
		default: break;
	*/	
		}

	}



