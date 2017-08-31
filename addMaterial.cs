using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;

public class addMaterial : MonoBehaviour {

	public List<material> list;
	public string mat;
	public float co1;
	public float co2;
	public float co3;
	public float co4;
	public float co5;
	public float co6;
	public libraryHandler lh;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setMaterial (string n){
		mat = n;
	}
	public void setCo1(string n){
		co1 = float.Parse(n);
	}
	public void setCo2(string n){
		co2 = float.Parse(n);
	}
	public void setCo3(string n){
		co3 = float.Parse(n);
	}
	public void setCo4(string n){
		co4 = float.Parse(n);
	}
	public void setCo5(string n){
		co5 = float.Parse(n);
	}
	public void setCo6(string n){
		co6 = float.Parse(n);
	}

	public void updateLibraryFile(){
		using(FileStream fs = new FileStream(@"C:\ASS\materialLibrary.txt",
		                                     FileMode.OpenOrCreate, FileAccess.ReadWrite))
		{
			
			StreamReader sr = new StreamReader(fs);
			string buffer = sr.ReadToEnd();
			buffer = mat + ":" + co1.ToString() + ":" + co2.ToString() + ":" + co3.ToString() + ":" + co4.ToString() + ":" + co5.ToString() + ":" + co6.ToString();
		//	buffer = "\n" + mat + ":" + co1.ToString() + ":" + co2.ToString() + ":" + co3.ToString() + ":" + co4.ToString() + ":" + co5.ToString() + ":" + co6.ToString();
			StreamWriter sw = new StreamWriter(fs);
			sw.WriteLine();
			sw.Write(String.Format(buffer,true));

			sw.Flush();
			sw.Close ();

			list = lh.library;
			list.Add(new material(mat,co1,co2,co3,co4, co5,co6));
			Debug.Log (lh.library.Count);
			//lh.ReadLibraryPanel();
			//lh.ReadLibraryMain();

		}
	}
}
