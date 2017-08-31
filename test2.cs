using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class test2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		FileWrite ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void FileWrite(){
		// Use this for initialization
		string buffer = "";
		string digits = "i got dem digits mane.";
		using(FileStream fs = new FileStream(@"C:\ASS\materialLibrary.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
		{
			StreamReader sr = new StreamReader(fs);
			buffer = sr.ReadToEnd();
			
			
			//	using (FileStream fs2 = new FileStream( @"C:\ASS\materialLibrary.txt",
			//	                                      ,)           )
			//	{
			StreamWriter tw = new StreamWriter(fs);
			tw.WriteLine(String.Format(digits, true ));
			tw.Flush();
			tw.Close ();
			sr = null;
			//	}
		}
	}
}
