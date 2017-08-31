using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
//using (FileStream fs = new FileStream( @"materialLibrary.txt"
                                  //    , FileMode.OpenOrCreate
                                  //   , FileAccess.ReadWrite));           

public class libraryHandler : MonoBehaviour {

	public List<material> library;
	public dataStorage dataStorage;
	public GameObject printButton;
	public Text textWindowName;
	public Text textWindowCo1;
	public Text textWindowCo2;
	public Text textWindowCo3;
	public Text textWindowCo4;
	public Text textWindowCo5;
	public Text textWindowCo6;
	public Text textWindowCo7;
	public Text textWindowCo8;
	public Text textWindowCo9;
	public Text textWindowCo10;
	public Dropdown ddW1;
	public Dropdown ddW2;
	public Dropdown ddW3;
	public Dropdown ddW4;
	public Dropdown ddC;
	public Dropdown ddF;

	public GameObject textLength;


	// Use this for initialization
	void Awake () {
		library = new List<material>();
		using(FileStream fs = new FileStream(@"C:\ASS\materialLibrary.txt",
		                                     FileMode.OpenOrCreate, FileAccess.ReadWrite))
		{
			
			StreamReader sr = new StreamReader(fs);
			string buffer = sr.ReadLine();
			library.Clear();
			while(buffer != null){
				char[] delimiter = {':'};
				string[] fields = buffer.Split(delimiter);
				library.Add(new material(fields[0],float.Parse(fields[1]),float.Parse(fields[2]),
				                         float.Parse(fields[3]),float.Parse(fields[4]),
				                         float.Parse(fields[5]),float.Parse(fields[6])));
				buffer = sr.ReadLine();
			}
		}
	//	ReadLibraryMain();
		
	}

	
	// Update is called once per frame
	void Update () {


	}

	public void Menu()
	{

	}
	private void AddMaterial(string name, float co1, float co2, float co3, float co4,
	                         float co5, float co6){
		material m = new material(name, co1 ,co2,co3,co4,co5,co6);
	}

	public void SaveLibrary(){
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
			foreach(material mat in library){
				StreamWriter tw = new StreamWriter(fs);
				tw.WriteLine(String.Format(digits, true ));
				tw.Flush();
				tw.Close ();
				sr = null;
			}
				//	}
			}
		}
	public void ReadLibraryPanel(){

		using(FileStream fs = new FileStream(@"C:\ASS\materialLibrary.txt",
		                                     FileMode.OpenOrCreate, FileAccess.ReadWrite))
		{

			StreamReader sr = new StreamReader(fs);
			string buffer = sr.ReadLine();
		
			library.Clear();

			while(buffer != null){
				char[] delimiter = {':'};
				string[] fields = buffer.Split(delimiter);
				library.Add(new material(fields[0],float.Parse(fields[1]),float.Parse(fields[2]),
				                         float.Parse(fields[3]),float.Parse(fields[4]),
				                         float.Parse(fields[5]),float.Parse(fields[6])));
				buffer = sr.ReadLine();
			}
			sr = null;
			updateWindowText(textWindowName, 348, 0);
			updateWindowText(textWindowCo1, 50, 1);
			updateWindowText(textWindowCo2, 50, 2);
			updateWindowText(textWindowCo3, 50, 3);
			updateWindowText(textWindowCo4, 50, 4);
			updateWindowText(textWindowCo5, 50, 5);
			updateWindowText(textWindowCo6, 50, 6);
				}
		}
	public void ReadLibraryMain(){
		
		using(FileStream fs = new FileStream(@"C:\ASS\materialLibrary.txt",
		                                     FileMode.OpenOrCreate, FileAccess.ReadWrite))
		{
			
			StreamReader sr = new StreamReader(fs);
			string buffer = sr.ReadLine();
			library.Clear();
			while(buffer != null){
				char[] delimiter = {':'};
				string[] fields = buffer.Split(delimiter);
				library.Add(new material(fields[0],float.Parse(fields[1]),float.Parse(fields[2]),
				                         float.Parse(fields[3]),float.Parse(fields[4]),
				                         float.Parse(fields[5]),float.Parse(fields[6])));
				buffer = sr.ReadLine();
			}
			sr = null;


			ddF.options.Clear();
			ddC.options.Clear();
			ddW1.options.Clear();
			ddW2.options.Clear();
			ddW3.options.Clear();
			ddW4.options.Clear();

			int index = 0;
			foreach(material mat in library){
				float co1 = mat.GetCo1();
				float co2 = mat.GetCo2();
				float co3 = mat.GetCo3();
				float co4 = mat.GetCo4();
				float co5 = mat.GetCo5();
				float co6 = mat.GetCo6();
			
				string temp = mat.GetMaterial();
				int counter = 0;
				while(counter < 10){
					dataStorage.setCos(index, counter, mat.GetCo(counter));
					counter++;
				}
				counter = 0;
				ddF.options.Add(new Dropdown.OptionData(){text = temp});
				ddC.options.Add(new Dropdown.OptionData(){text = temp}); //adds the material names to the dropdown menus
				ddW1.options.Add(new Dropdown.OptionData(){text = temp});
				ddW2.options.Add(new Dropdown.OptionData(){text = temp});
				ddW3.options.Add(new Dropdown.OptionData(){text = temp});
				ddW4.options.Add(new Dropdown.OptionData(){text = temp});
				index++;


			}
			ddF.value = 1;
			ddF.value = 0;
			ddC.value = 1;
			ddC.value = 0;
			ddW1.value = 1;
			ddW1.value = 0;
			ddW2.value = 1;
			ddW2.value = 0;
			ddW3.value = 1;
			ddW3.value = 0;
			ddW4.value = 1;
			ddW4.value = 0;

		}
	}

	public void PrintLibrary(){

					foreach(material mat in library){
						Debug.Log(mat.GetMaterial());
						Debug.Log(mat.GetCo1());
						Debug.Log(mat.GetCo2());
					}
				}
	public void updateWindowText(Text textWindow, int width, int type) {
		string buffer = "";
		int sizeCounter = 0;
		textWindow.text = buffer;
		switch(type){
			case 0:
			foreach(material mat in library){
				sizeCounter++;
					buffer +=  mat.GetMaterial();
					buffer += "\n"; 
			
				}break;
			case 1:
				foreach(material mat in library){
					sizeCounter++;
					buffer += mat.GetCo1();
					buffer += "\n";
					
				}break;
			case 2:
				foreach(material mat in library){
					sizeCounter++;
					buffer +=  mat.GetCo2();
					buffer += "\n";
					
				}break;
			case 3:
				foreach(material mat in library){
					sizeCounter++;
					buffer +=  mat.GetCo3();
					buffer += "\n";
					
				}break;
			case 4:
				foreach(material mat in library){
					sizeCounter++;
					buffer +=  mat.GetCo4();
					buffer += "\n";
					
				}break;
			case 5:
				foreach(material mat in library){
					sizeCounter++;
					buffer +=  mat.GetCo5();
					buffer += "\n";
					
				}break;
			case 6:
				foreach(material mat in library){
					sizeCounter++;
					buffer +=  mat.GetCo6();
					buffer += "\n";
					
				}break;

		}
		textWindow.text = buffer;
		textWindow.rectTransform.sizeDelta = new Vector2(width, 35*sizeCounter);
		textLength.GetComponent<RectTransform>().sizeDelta = new Vector2(860, 60 + 16*sizeCounter);

	}
	
}

//library.Add(mat);