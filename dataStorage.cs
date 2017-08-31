using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
public class dataStorage : MonoBehaviour {

	public int libraryCount;
	public bool unit;
	public float sos;
	public float temp;
	public float length;
	public float width;
	public float height;
	public float volume ;
	public float surfaceArea;
	public float schroederFreq;
	public float MFP;
	public float CD;
	public float wall1SA;
	public float wall2SA;
	public float wall3SA;
	public float wall4SA;
	public float floorSA;
	public float w1avg;
	public float w2avg;
	public float w3avg;
	public float w4avg;
	public float cavg;
	public float favg;

	public float[] w1a;
	public float[] w2a;
	public float[] w3a;
	public float[] w4a;
	public float[] fa;
	public float[] ca;
	public float[] rt60;
	public Dropdown w1menu;
	public Dropdown w2menu;
	public Dropdown w3menu;
	public Dropdown w4menu;
	public Dropdown fmenu;
	public Dropdown cmenu;

	public Text rtOutput1;
	public Text rtOutput2;
	public Text rtOutput3;
	public Text rtOutput4;
	public Text rtOutput5;
	public Text rtOutput6;

	public Text sosOutput;
	public Text sosUnit;

	public Text schroederOut;
	public Text mfpOut;
	public Text cdOut;

	public float ceilingSA;
	public GameObject LH;
	public float[][] list;
	public float[][]  mode;
	public float[][] modeTan;
	public float[] lMode;
	public float[] hMode;
	public float[] wMode;
	public List<material> libraryData;
	public functions calc;
	void Awake (){
		length = 40;
		width = 20;
		height = 10;
		unit = true;
		temp = 20f;
		w1avg = 0;
		w2avg = 0;
		w3avg = 0;
		w4avg = 0;
		cavg = 0;
		favg = 0;
		schroederFreq = 0;
		if(unit){
			sos = 1126f;
		}
		else{
			sos = 343.29f;
		}
		float[] rt60 = new float[6];
		list = new float[500][];
		for(int i = 0; i<500;i++)
		{
			list[i] = new float[10];
		}
		mode = new float[3][];
		for(int i = 0; i< 3;i++){

			mode[i] = new float[10];
			int counter = 1;
			for(int j = 0;j<10;j++){

				if(i==0){
					mode[i][j] = calc.modeFreq(counter,0,0,length,width,height,sos);
				}
				if(i==1){
					mode[i][j] = calc.modeFreq(0,counter,0,length,width,height,sos);
				}
				if(i==2){
					mode[i][j] = calc.modeFreq(0,0,counter,length,width,height,sos);
				}
						
					
				counter++;

			}
				

		}
		int l = list.Length;;
		

		list[0][0]= .1f;

		volume = length*width*height;
		surfaceArea = 2*(length*width)+2*(length*height)+2*(width*height);
	//	yield return new WaitForEndOfFrame();
		libraryData = new List<material>();
		using(FileStream fs = new FileStream(@"C:\ASS\materialLibrary.txt",
		                                     FileMode.OpenOrCreate, FileAccess.ReadWrite))
		{
			
			StreamReader sr = new StreamReader(fs);
			string buffer = sr.ReadLine();
			libraryData.Clear();
			while(buffer != null){
				char[] delimiter = {':'};
				string[] fields = buffer.Split(delimiter);
				libraryData.Add(new material(fields[0],float.Parse(fields[1]),float.Parse(fields[2]),
				                         float.Parse(fields[3]),float.Parse(fields[4]),
				                         float.Parse(fields[5]),float.Parse(fields[6])));
				buffer = sr.ReadLine();

			}
			libraryCount = libraryData.Count;

			Debug.Log ("storage library count: " + libraryCount);
		}
		int index = 0;
		foreach(material mat in libraryData){
			int counter = 0;
			while(counter < 10){
				setCos(index, counter, mat.GetCo(counter));
			//	Debug.Log (list[index][counter]);
				counter++;
			}
			counter = 0;
			index++;
		}
		float[] w1a = new float[6];
		float[] w2a = new float[6];
		float[] w3a = new float[6];
		float[] w4a = new float[6];
		float[] fa = new float[6];
		float[] ca = new float[6];
		for(int i = 0;i<6;i++){

			w1a[i] = list[0][0];
			w2a[i] = list[0][0];
			w3a[i] = list[0][0];
			w4a[i] = list[0][0];
			fa[i] = list[0][0];
			ca[i] = list[0][0];
		}	
		}
		void Update(){

		float[] rt60 = new float[6];
		float[] w1a = new float[6];
		float[] w2a = new float[6];
		float[] w3a = new float[6];
		float[] w4a = new float[6];
		float[] fa = new float[6];
		float[] ca = new float[6];




		for(int i =0;i<6;i++){
			w1a[i] = list[w1menu.value][i];
			w2a[i] = list[w2menu.value][i];
			w3a[i] = list[w3menu.value][i];
			w4a[i] = list[w4menu.value][i];
			fa[i] = list[fmenu.value][i];
			ca[i] = list[cmenu.value][i];
		}
		w1avg = (list[w1menu.value][0]+list[w1menu.value][1]+list[w1menu.value][2]+list[w1menu.value][3]+list[w1menu.value][4]+list[w1menu.value][5])/6;
		w2avg = (list[w2menu.value][0]+list[w2menu.value][1]+list[w2menu.value][2]+list[w2menu.value][3]+list[w2menu.value][4]+list[w2menu.value][5])/6;
		w3avg = (list[w3menu.value][0]+list[w3menu.value][1]+list[w3menu.value][2]+list[w3menu.value][3]+list[w3menu.value][4]+list[w3menu.value][5])/6;
		w4avg = (list[w4menu.value][0]+list[w4menu.value][1]+list[w4menu.value][2]+list[w4menu.value][3]+list[w4menu.value][4]+list[w4menu.value][5])/6;
		cavg  = ( list[cmenu.value][0]+ list[cmenu.value][1]+ list[cmenu.value][2]+ list[cmenu.value][3]+ list[cmenu.value][4]+ list[cmenu.value][5])/6;
		favg  = ( list[fmenu.value][0]+ list[fmenu.value][1]+ list[fmenu.value][2]+ list[fmenu.value][3]+ list[fmenu.value][4]+ list[fmenu.value][5])/6;

		wall1SA = length*height;
		wall2SA = width*height;
		wall3SA = length*height;
		wall4SA = width*height;
		floorSA = length*width;
		ceilingSA = length*width;
		surfaceArea = 2*(length*width)+2*(length*height)+2*(width*height);
		volume = length*width*height;
		for(int i = 0;i<6;i++){
		rt60[i] = calc.RT60(wall1SA,wall2SA,floorSA,w1a[i],w2a[i],w3a[i],w4a[i],fa[i],ca[i],volume,unit);
		//	Debug.Log (rt60[i]);


		}
		rtOutput1.text = rt60[0].ToString("f2");
		rtOutput2.text = rt60[1].ToString("f2");
		rtOutput3.text = rt60[2].ToString("f2");
		rtOutput4.text = rt60[3].ToString("f2");
		rtOutput5.text = rt60[4].ToString("f2");
		rtOutput6.text = rt60[5].ToString("f2");


		//calculates modes
		mode = new float[3][];
		for(int i = 0; i< 3;i++){
			
			mode[i] = new float[10];
			int counter = 1;
			for(int j = 0;j<10;j++){
				
				if(i==0){
					mode[i][j] = calc.modeFreq(counter,0,0,length,width,height,sos);
						
				}
				if(i==1){
					mode[i][j] = calc.modeFreq(0,counter,0,length,width,height,sos);

				}
				if(i==2){
					mode[i][j] = calc.modeFreq(0,0,counter,length,width,height,sos);

				}
				

				counter++;
				
			}
			
			
		}
		modeTan = new float[3][];
		for(int i = 0; i< 3;i++){
			
			modeTan[i] = new float[10];
			int counter = 1;
			for(int j = 0;j<10;j++){
				
				if(i==0){
					modeTan[i][j] = calc.modeFreq(counter,counter,0,length,width,height,sos);
					
				}
				if(i==1){
					modeTan[i][j] = calc.modeFreq(0,counter,counter,length,width,height,sos);
					
				}
				if(i==2){
					modeTan[i][j] = calc.modeFreq(counter,0,counter,length,width,height,sos);
					
				}
				
				
				counter++;
				
			}
			
			
		}

		schroederFreq = calc.SchroederFreq(rt60,length,width,height,unit);
		MFP = calc.MFP(length,width,height,unit);
		CD = calc.CriticalDistance(rt60,length,width,height,unit);

		schroederOut.text = schroederFreq.ToString("f2");
		mfpOut.text = MFP.ToString("f2");
		cdOut.text = CD.ToString("f2");
		
	}
	// Use this for initialization
	//public dataStorage (){

	//	libraryCount = libraryData.Count;
	//	length = 40;
	//	width = 20;
	//	height = 10;



	//}
	public void libraryUpdate(){
		libraryCount = libraryData.Count;

	}

	public void setLength(float l){
		length = l;
	}
	public void setWidth(float w){
		width = w;
	}
	public void setHeight(float h){
		height = h;
	}
	public void setCos(int index, int coNum, float coEf ){
		list[index][coNum] = coEf;
	}
	public void setUnit(bool u){
		if(u){
			unit = true;
		}
		else{
			unit = false;
		}

	}
	public void setW1a(int index){
		float[] w1a = new float[6];
		for (int i = 0;i<6;i++){
		w1a[i] = list[index][i];
		}
	}
	public void setW2a(int index){
		float[] w2a = new float[6];
		for (int i = 0;i<6;i++){
			w2a[i] = list[index][i];
		}
	}
	public void setW3a(int index){
		float[] w3a = new float[6];
		for (int i = 0;i<6;i++){
			w3a[i] = list[index][i];
		}
	}
	public void setW4a(int index){
		float[] w4a = new float[6];
		for (int i = 0;i<6;i++){
			w4a[i] = list[index][i];
		}
	}
	public void setFa(int index){
		float[] fa = new float[6];
		for (int i = 0;i<6;i++){
			fa[i] = list[index][i];
		}
	}
	public void setCa(int index){
		float[] ca = new float[6];
		for (int i = 0;i<6;i++){
			ca[i] = list[index][i];
		}
	}
	public void setTemp(String tempInput){
		temp = float.Parse(tempInput);
		float speedMS = new float();
		if(unit){
			temp = calc.fToC(temp);
			speedMS = calc.SOS(temp);
			sos = speedMS*3.28084f;
			sosOutput.text = sos.ToString("f2");
			sosUnit.text = "f/s";
			//	sos = 1130f;
		}
		else{
			speedMS = calc.SOS (temp);
			sos = speedMS;
			//sos = 330.4f;
			sosOutput.text = sos.ToString("f2");
			sosUnit.text = "m/s";
		}

	}


}
