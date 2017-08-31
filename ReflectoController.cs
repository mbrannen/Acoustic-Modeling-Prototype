using UnityEngine;
using System.Collections;
//using UnityEditor;
using UnityEngine.UI;

public class ReflectoController : MonoBehaviour {

	public GameObject dataObj;
	public functions calc;

	public bool active;

	public GameObject listener;
	public GameObject source;
	public GameObject barMin;
	public GameObject barMax;
	public GameObject parent;

	public Text max;
	public Text min;
	public Text half;
	public Text quarter;
	public Text threeQuarter;

	public Text lvlmax;
	public Text lvlmin;
	public Text lvlhalf;
	public Text lvlquarter;
	public Text lvlthreeQuarter;


	public float listenerHeight;
	public float sourceHeight;
	public float dbSPL;
	public float cDist;
	public float fDist;
	public float w1Dist;
	public float w2Dist;
	public float w3Dist;
	public float w4Dist;
	public float maxTime;
	public float minLevel;
	public bool state;

	public float DirectSound;
	public float dsL;
	public float W1R1D;
	public float W2R1D;
	public float W3R1D;
	public float W4R1D;
	public float CR1D;
	public float FR1D;
	public float[] R1;
	public float[] R1L;
	public float[] R1preD;
	public float[] R1preL;



	public float W1W2R2D;
	public float W1W3R2D;
	public float W1W4R2D;
	public float W2W3R2D;
	public float W2W4R2D;
	public float W3W4R2D;
	public float W1CR2D;
	public float W1FR2D;
	public float W2CR2D;
	public float W2FR2D;
	public float W3CR2D;
	public float W3FR2D;
	public float W4CR2D;
	public float W4FR2D;
	public float CFR2D;
	public float[] R2;
	public float[] R2L;
	public float[] R2preD;
	public float[] R2preL;


	public float[] surfaceAbs;
	public float time;


	RaycastHit rayhit;
	Ray upRay;
	Ray downRay;
	Ray leftRay;
	Ray rightRay;
	Ray forwardRay;
	Ray backRay;
	
	// Use this for initialization
	void Start () {
		active = false;
		maxTime = 0;
		state = false;
		listenerHeight = 5f;
		sourceHeight = 5f;
		dbSPL = 0f;
		listener.SetActive(false);
		source.SetActive(false);
		//cDist = 1;
		//fDist = 1;
		//w1Dist =1;
		//w2Dist = 1;
		//w3Dist = 1;
		//w4Dist = 1;
	}
	
	// Update is called once per frame
	void Update () {

		if(state == true){
			RaycastHit rayHit;
			cDist = 1;
			Vector3 up = source.transform.TransformDirection(Vector3.up)*10;
			Vector3 down = source.transform.TransformDirection(Vector3.down)*10;
			Vector3 left = source.transform.TransformDirection(Vector3.left)*10;
			Vector3 right = source.transform.TransformDirection(Vector3.right)*10;
			Vector3 forward = source.transform.TransformDirection(Vector3.forward)*10;
			Vector3 back = source.transform.TransformDirection(Vector3.back)*10;
		
			Debug.DrawRay(source.transform.position, up,Color.green);
			Debug.DrawRay(source.transform.position, down,Color.green);
			Debug.DrawRay(source.transform.position, left,Color.blue);
			Debug.DrawRay(source.transform.position, right,Color.blue);
			Debug.DrawRay(source.transform.position, forward,Color.red);
			Debug.DrawRay(source.transform.position, back,Color.red);
			if(Physics.Raycast(source.transform.position,up, out rayHit)){
				cDist = rayHit.distance;
			}
			if(Physics.Raycast(source.transform.position,down, out rayHit)){
				fDist = rayHit.distance;
			}
			if(Physics.Raycast(source.transform.position,left, out rayHit)){
				w3Dist = rayHit.distance;
			}
			if(Physics.Raycast(source.transform.position,right, out rayHit)){
				w1Dist = rayHit.distance;
			}
			if(Physics.Raycast(source.transform.position,forward, out rayHit)){
				w4Dist = rayHit.distance;
			}
			if(Physics.Raycast(source.transform.position,back, out rayHit)){
				w2Dist = rayHit.distance;
			}

			DirectSound =  Vector3.Distance(source.transform.position,listener.transform.position);



		}
	
	}
	public void ListenerHeightUpdate(string height){
		listenerHeight = float.Parse(height);

	}
	public void SourceHeightUpdate(string height){
		sourceHeight = float.Parse(height);
	}
	public void dbSPLUpdate(string spl){
		dbSPL = float.Parse(spl);
	}
	public void ListnerEnabled(){
		listener.SetActive(true);
		listener.transform.position = new Vector3 (listener.transform.position.x,listenerHeight,listener.transform.position.z);

	}
	public void SourceEnabled(){
		state = true;
		source.SetActive(true);
		source.transform.position = new Vector3 (source.transform.position.x,sourceHeight,source.transform.position.z);
	}

	public void Enable(){
		state = true;
	}
	public void Disable(){
		state = false;
	}
	public void Calculate(){
		//first order
		Vector3 W1R1 = new Vector3(source.transform.position.x,source.transform.position.y,source.transform.position.z-(2*w1Dist));
		W1R1D = Vector3.Distance(W1R1, listener.transform.position);

		Vector3 W2R1 = new Vector3(source.transform.position.x-(2*w2Dist),source.transform.position.y,source.transform.position.z);
		W2R1D = Vector3.Distance(W2R1, listener.transform.position);

		Vector3 W3R1 = new Vector3(source.transform.position.x,source.transform.position.y,source.transform.position.z+(2*w3Dist));
		W3R1D = Vector3.Distance(W3R1, listener.transform.position);

		Vector3 W4R1 = new Vector3(source.transform.position.x+(2*w4Dist),source.transform.position.y,source.transform.position.z);
		W4R1D = Vector3.Distance(W4R1, listener.transform.position);

		Vector3 CR1 = new Vector3(source.transform.position.x,source.transform.position.y+(2*cDist),source.transform.position.z);
		CR1D = Vector3.Distance(CR1, listener.transform.position);

		Vector3 FR1 = new Vector3(source.transform.position.x,source.transform.position.y-(2*fDist),source.transform.position.z);
		FR1D = Vector3.Distance(FR1, listener.transform.position);

		//second order
		Vector3 W1W2R2 = new Vector3(source.transform.position.x-(2*w2Dist),source.transform.position.y,source.transform.position.z-(2*w1Dist));
		W1W2R2D = Vector3.Distance(W1W2R2, listener.transform.position);

		Vector3 W1W3R2 = new Vector3(source.transform.position.x,source.transform.position.y,source.transform.position.z+(2*w3Dist)+(2*w1Dist));
		W1W3R2D = Vector3.Distance(W1W3R2, listener.transform.position);

		Vector3 W1W4R2 = new Vector3(source.transform.position.x+(2*w4Dist),source.transform.position.y,source.transform.position.z-(2*w1Dist));
		W1W4R2D = Vector3.Distance(W1W4R2, listener.transform.position);

		Vector3 W2W3R2 = new Vector3(source.transform.position.x-(2*w2Dist),source.transform.position.y,source.transform.position.z+(2*w3Dist));
		W2W3R2D = Vector3.Distance(W2W3R2, listener.transform.position);

		Vector3 W2W4R2 = new Vector3(source.transform.position.x+(2*w4Dist)+(2*w2Dist),source.transform.position.y,source.transform.position.z);
		W2W4R2D = Vector3.Distance(W2W4R2, listener.transform.position);

		Vector3 W3W4R2 = new Vector3(source.transform.position.x+(2*w4Dist),source.transform.position.y,source.transform.position.z+(2*w3Dist));
		W3W4R2D = Vector3.Distance(W3W4R2, listener.transform.position);

		Vector3 W1CR2 = new Vector3(source.transform.position.x,source.transform.position.y+(2*cDist),source.transform.position.z-(2*w1Dist));
		W1CR2D = Vector3.Distance(W1CR2, listener.transform.position);

		Vector3 W1FR2 = new Vector3(source.transform.position.x,source.transform.position.y-(2*fDist),source.transform.position.z-(2*w1Dist));
		W1FR2D = Vector3.Distance(W1FR2, listener.transform.position);

		Vector3 W2CR2 = new Vector3(source.transform.position.x-(2*w2Dist),source.transform.position.y+(2*cDist),source.transform.position.z);
		W2CR2D = Vector3.Distance(W2CR2, listener.transform.position);

		Vector3 W2FR2 = new Vector3(source.transform.position.x-(2*w2Dist),source.transform.position.y-(2*fDist),source.transform.position.z);
		W2FR2D = Vector3.Distance(W2FR2, listener.transform.position);

		Vector3 W3CR2 = new Vector3(source.transform.position.x,source.transform.position.y+(2*cDist),source.transform.position.z+(2*w3Dist));
		W3CR2D = Vector3.Distance(W3CR2, listener.transform.position);

		Vector3 W3FR2 = new Vector3(source.transform.position.x,source.transform.position.y-(2*fDist),source.transform.position.z+(2*w3Dist));
		W3FR2D = Vector3.Distance(W3FR2, listener.transform.position);

		Vector3 W4CR2 = new Vector3(source.transform.position.x+(2*w4Dist),source.transform.position.y+(2*cDist),source.transform.position.z);
		W4CR2D = Vector3.Distance(W4CR2, listener.transform.position);

		Vector3 W4FR2 = new Vector3(source.transform.position.x+(2*w4Dist),source.transform.position.y-(2*fDist),source.transform.position.z);
		W4FR2D = Vector3.Distance(W4FR2, listener.transform.position);

		Vector3 CFR2 = new Vector3(source.transform.position.x,source.transform.position.y+(2*cDist)+(2*fDist),source.transform.position.z);
		CFR2D = Vector3.Distance(CFR2, listener.transform.position);



		R1 = new float[6];
		R1preD = new float[6];
		R1[0] = W1R1D;
		R1[1] = W2R1D;
		R1[2] = W3R1D;
		R1[3] = W4R1D;
		R1[4] = CR1D;
		R1[5] = FR1D;

		R2 = new float[15];
		R2preD = new float[15];
		R2[0] = W1W2R2D;
		R2[1] = W1W3R2D;
		R2[2] = W1W4R2D;
		R2[3] = W2W3R2D;
		R2[4] = W2W4R2D;
		R2[5] = W3W4R2D;
		R2[6] = W1CR2D;
		R2[7] = W1FR2D;
		R2[8] = W2CR2D;
		R2[9] = W2FR2D;
		R2[10] = W3CR2D;
		R2[11] = W3FR2D;
		R2[12] = W4CR2D;
		R2[13] = W4FR2D;
		R2[14] = CFR2D;

		dataStorage data = dataObj.GetComponent<dataStorage>();

		time = new float();
		R1L = new float[6];
		for (int i = 0;i < R1.Length;i++){ //converts distance to time
			R1preD[i] = R1[i];
			time = R1[i]/data.sos;
			R1[i] = time;
		}
		time = 0;
		R2L = new float[15];
		for(int i = 0;i<R2.Length;i++){
			R2preD[i] = R2[i];
			time = R2[i]/data.sos;
			R2[i] = time;
		}
		time = DirectSound/data.sos;
		dsL = new float();
		dsL = calc.distanceLoss(dbSPL,DirectSound, data.unit);

		R1preL = new float[6];
		R2preL = new float[15];
		surfaceAbs = new float[6];
		surfaceAbs[0] = data.w1avg;
		surfaceAbs[1] = data.w2avg;
		surfaceAbs[2] = data.w3avg;
		surfaceAbs[3] = data.w4avg;
		surfaceAbs[4] = data.cavg;
		surfaceAbs[5] = data.favg;

		//first order
		for(int i =0;i<R1preL.Length;i++){
			R1preL[i] = calc.surfaceAbsoprtion(surfaceAbs[i],dbSPL);

		}

		//second order
		R2preL[0] = calc.surfaceAbsoprtionSecondOrder(surfaceAbs[0],surfaceAbs[1],dbSPL);
		R2preL[1] = calc.surfaceAbsoprtionSecondOrder(surfaceAbs[0],surfaceAbs[2],dbSPL);
		R2preL[2] = calc.surfaceAbsoprtionSecondOrder(surfaceAbs[0],surfaceAbs[3],dbSPL);
		R2preL[3] = calc.surfaceAbsoprtionSecondOrder(surfaceAbs[1],surfaceAbs[2],dbSPL);
		R2preL[4] = calc.surfaceAbsoprtionSecondOrder(surfaceAbs[1],surfaceAbs[3],dbSPL);
		R2preL[5] = calc.surfaceAbsoprtionSecondOrder(surfaceAbs[2],surfaceAbs[3],dbSPL);
		R2preL[6] = calc.surfaceAbsoprtionSecondOrder(surfaceAbs[0],surfaceAbs[4],dbSPL);
		R2preL[7] = calc.surfaceAbsoprtionSecondOrder(surfaceAbs[0],surfaceAbs[5],dbSPL);
		R2preL[8] = calc.surfaceAbsoprtionSecondOrder(surfaceAbs[1],surfaceAbs[4],dbSPL);
		R2preL[9] = calc.surfaceAbsoprtionSecondOrder(surfaceAbs[1],surfaceAbs[5],dbSPL);
		R2preL[10]= calc.surfaceAbsoprtionSecondOrder(surfaceAbs[2],surfaceAbs[4],dbSPL);
		R2preL[11]= calc.surfaceAbsoprtionSecondOrder(surfaceAbs[2],surfaceAbs[5],dbSPL);
		R2preL[12]= calc.surfaceAbsoprtionSecondOrder(surfaceAbs[3],surfaceAbs[4],dbSPL);
		R2preL[13]= calc.surfaceAbsoprtionSecondOrder(surfaceAbs[3],surfaceAbs[5],dbSPL);
		R2preL[14]= calc.surfaceAbsoprtionSecondOrder(surfaceAbs[4],surfaceAbs[5],dbSPL);


		//first order
		for(int i = 0;i< R1L.Length;i++){
			R1L[i] = calc.distanceLoss(R1preL[i],R1preD[i], data.unit);
		}
		//second order
		for(int i = 0;i< R2L.Length;i++){
			R2L[i] = calc.distanceLoss(R2preL[i],R2preD[i], data.unit);
		}

		float Maxtime = new float();
		float Maxlevel = new float();
		float MinLevel = new float();

		Maxtime = 0f;
		minLevel = 0f;
		Maxlevel = dbSPL;
		MinLevel = dbSPL;

		for (int i = 0;i < R1.Length;i++){ //find the max time first order
			if(R1[i] > Maxtime){
				Maxtime = R1[i];
			}
		}
		for (int i = 0;i < R2.Length;i++){ //find the max time second order
			if(R2[i] > Maxtime){
				Maxtime = R2[i];
			}
		}
		for (int i = 0;i < R1L.Length;i++){ //find the min SPL first order
			if(R1L[i] < MinLevel){
				MinLevel = R1L[i];
			}
		}
		for (int i = 0;i < R2L.Length;i++){ //find the min SPL second order
			if(R2L[i] < MinLevel){
				MinLevel = R2L[i];
			}
		}
		Debug.Log(Maxtime);
		Debug.Log(MinLevel);
		max.text = ((Maxtime+.005f)*1000).ToString("f2");
		min.text = "0";
		half.text = (((Maxtime+.005f)*1000)/2).ToString("f2");
		quarter.text = (((Maxtime+.005f)*1000)/4).ToString("f2");
		threeQuarter.text = ((((Maxtime+.005f)*1000)/4)*3).ToString("f2");

		maxTime = Maxtime;
		minLevel = MinLevel;

		lvlmax.text = dbSPL.ToString();
		lvlmin.text = (MinLevel-5).ToString("f0");
		lvlhalf.text = (((dbSPL-(MinLevel-5))/2)+(MinLevel-5)).ToString("f0");
		lvlquarter.text = (((dbSPL-(MinLevel-5))/4)+(MinLevel-5)).ToString("f0");
		lvlthreeQuarter.text = (((((dbSPL-(MinLevel-5))/4))*3+(MinLevel-5))).ToString("f0");
		barXHandler();


	}
	public void barXHandler(){

		barMin.SetActive(true);
		barMax.SetActive(true);

		float zero = new float();
		float width = new float();
		float height = new float();
		zero = barMin.transform.position.x -46.1322f;
		width = 709f-12f;
		height = 227f-12f;
		for(int i = 0; i < R1.Length; i++){
			float perTemp = new float();
			float barPos = new float();
			float barHeight = new float();
			float perTemp2 = new float();
			perTemp = (R1[i])/(maxTime+.005f);
			perTemp2 = 1 - ((R1L[i]-(minLevel-5))/(dbSPL-(minLevel-5)));
			barPos = width*perTemp;
			barHeight = 12+height*perTemp2;
			Debug.Log (perTemp2);
			Debug.Log (barHeight+ " " +i);
			GameObject newBar = Instantiate(barMin) as GameObject;
			newBar.name = "bar"+Random.Range(0,100000);
			newBar.tag = "bar";
			RectTransform barTrans = newBar.GetComponent<RectTransform>();
			Image color = newBar.GetComponent<Image>();
			color.color = Color.green;
			barTrans.SetParent(parent.transform,false);
			barTrans.anchoredPosition = new Vector2(zero+barPos, barMin.transform.position.y);
			barTrans.offsetMin = new Vector2(barTrans.offsetMin.x, 2.5f);
			barTrans.offsetMax = new Vector2(barTrans.offsetMax.x, -barHeight);
		}
		for(int i = 0; i < R2.Length; i++){

			float perTemp = new float();
			float barPos = new float();
			float barHeight = new float();
			float perTemp2 = new float();
			perTemp = (R2[i])/(maxTime+.005f);
			perTemp2 = 1 - ((R2L[i]-(minLevel-5))/(dbSPL-(minLevel-5)));
			barPos = width*perTemp;
			barHeight = 12+height*perTemp2;
			Debug.Log (perTemp2);
			Debug.Log (barHeight+ " " +i);
			GameObject newBar = Instantiate(barMin) as GameObject;
			newBar.name = "bar"+Random.Range(0,100000);
			newBar.tag = "bar";
			Image color = newBar.GetComponent<Image>();
			RectTransform barTrans = newBar.GetComponent<RectTransform>();
			color.color = Color.cyan;
			barTrans.SetParent(parent.transform,false);
			barTrans.anchoredPosition = new Vector2(zero+barPos, barMin.transform.position.y);
			barTrans.offsetMin = new Vector2(barTrans.offsetMin.x, 2.5f);
			barTrans.offsetMax = new Vector2(barTrans.offsetMax.x, -barHeight);
		}
		for(int i = 0; i < 1; i++){
			
			float perTemp = new float();
			float barPos = new float();
			float barHeight = new float();
			float perTemp2 = new float();
			perTemp = time/(maxTime+.005f);
			perTemp2 = 1 - ((dsL-(minLevel-5))/(dbSPL-(minLevel-5)));
			barPos = width*perTemp;
			barHeight = 12+height*perTemp2;
			Debug.Log (perTemp2);
			Debug.Log (barHeight+ " " +i);
			GameObject newBar = Instantiate(barMin) as GameObject;
			newBar.name = "bar"+Random.Range(0,100000);
			newBar.tag = "bar";
			Image color = newBar.GetComponent<Image>();
			RectTransform barTrans = newBar.GetComponent<RectTransform>();
			color.color = Color.magenta;
			barTrans.SetParent(parent.transform,false);
			barTrans.anchoredPosition = new Vector2(zero+barPos, barMin.transform.position.y);
			barTrans.offsetMin = new Vector2(barTrans.offsetMin.x, 2.5f);
			barTrans.offsetMax = new Vector2(barTrans.offsetMax.x, -barHeight);
		}

		barMin.SetActive(false);
		barMax.SetActive(false);

		//float perTemp = new float();
		//float barPos = new float();

	/*	perTemp = (maxTime)/(maxTime+5);
		barPos = width*perTemp;
			GameObject newBar = Instantiate(bar) as GameObject;
			newBar.name = gameObject.name + "bar";
			RectTransform barTrans = newBar.GetComponent<RectTransform>();
			barTrans.SetParent(parent.transform,false);
			barTrans.anchoredPosition = new Vector2(zero+barPos, bar.transform.position.y);
			barTrans.offsetMin = new Vector2(barTrans.offsetMin.x, 2.5f);
			barTrans.offsetMax = new Vector2(barTrans.offsetMax.x, -12.44f); */


	}
	public void clear(){
		GameObject[] bars = GameObject.FindGameObjectsWithTag("bar");
		foreach(GameObject obj in bars){
			Destroy(obj);
		}
	}
}
