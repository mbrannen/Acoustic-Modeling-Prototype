using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class test1  {
	public void FileWrite(){
	// Use this for initialization
	string buffer = "DINGUD BUT !))))";

	using(FileStream fs = new FileStream(@"C:\ASS\materialLibrary.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
	{
		StreamReader sr = new StreamReader(fs);
		buffer = sr.ReadToEnd();
		
		
		//	using (FileStream fs2 = new FileStream( @"C:\ASS\materialLibrary.txt",
		//	                                      ,)           )
		//	{
		StreamWriter tw = new StreamWriter(fs);
		tw.WriteLine(String.Format(buffer, true ));
		tw.Flush();
		tw.Close ();
		sr = null;
		//	}
	}
	}
}
	