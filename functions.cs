using UnityEngine;
using System.Collections;

public class functions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float RT60(float w1SA,float w2SA,float fSA, float a1, float a2, float a3, float a4, float a5, float a6,float V, bool u ){
		float rt60 = new float();
	
		if(u){
			rt60 = 0.049f*(V/((w1SA*a1)+(w2SA*a2)+(w1SA*a3)+(w2SA*a4)+(fSA*a5)+(fSA*a6)));
			return rt60;
			//0.049f
		}
			
		else{
			rt60 = 0.1611f*(V/((w1SA*a1)+(w2SA*a2)+(w1SA*a3)+(w2SA*a4)+(fSA*a5)+(fSA*a6)));
		return rt60;
		//	0.1611f
		}
	}


	public float modeFreq(int x1,int y1, int z1, float length, float width, float height, float c){

		float frequency = new float();

		frequency = (c/2)*Mathf.Sqrt(Mathf.Pow((x1/length),2)+Mathf.Pow((y1/width),2)+ Mathf.Pow((z1/height),2));

		return frequency;
	}
	public float SchroederFreq(float[]rt, float length, float width, float height, bool unit){

		if(unit){
			float l = length/3.2808399f;
			float w = width/3.2808399f;
			float h = height/3.2808399f;
			float v = l*w*h;
			float avgRt = (rt[0]+rt[1]+rt[2]+rt[3]+rt[4]+rt[5])/6;
			float freq = 2000*Mathf.Sqrt(avgRt/v);
			return freq;
		}else{
			float l = length;
			float w = width;
			float h = height;
			float v = l*w*h;
			float avgRt = (rt[0]+rt[1]+rt[2]+rt[3]+rt[4]+rt[5])/6;
			float freq = 2000*Mathf.Sqrt(avgRt/v);
			return freq;
		}

	}
	public float distanceLoss(float source, float distance, bool unit){
		if(unit){
			float SPL = source - Mathf.Abs(20*Mathf.Log10(3.2808399f/distance));
			return SPL;
		}else{
			float SPL = source - Mathf.Abs(20*Mathf.Log10(1/distance));
			return SPL;
		}
	}
	public float surfaceAbsoprtion(float absorption, float level){
		float pascals = new float();
		float absInv = new float();
		float pascalsReflected = new float();
		float dbReflected = new float();
		float pRef = new float();


		absInv = 1 - absorption;
		pRef = 0.00002f;
		pascals = pRef*Mathf.Pow(10,(level/20));
		pascalsReflected = pascals*absInv;
		dbReflected = 20*Mathf.Log10(pascalsReflected/pRef);

		return dbReflected;


	}
	public float surfaceAbsoprtionSecondOrder(float absorption,float absorption2, float level){
		float pascals = new float();
		float absInv = new float();
		float absInv2 = new float();
		float pascalsReflected = new float();
		float dbReflected = new float();
		float pRef = new float();
		
		
		absInv = 1 - absorption;
		absInv2 = 1 - absorption2;
		pRef = 0.00002f;
		pascals = pRef*Mathf.Pow(10,(level/20));
		pascalsReflected = pascals*absInv*absInv2;
		dbReflected = 20*Mathf.Log10(pascalsReflected/pRef);
		
		return dbReflected;
		
		
	}
	public float SOS(float temp){
		float sos = new float();
		sos = 331.5f + (0.6f*temp);
		return sos;

	}
	public float fToC(float fahr){
		float cels = new float();
		cels = (fahr - 32)/1.8f;
		return cels;
	}

	public float MFP( float length, float width, float height, bool unit){

		if(unit){
			float l = length/3.2808399f;
			float w = width/3.2808399f;
			float h = height/3.2808399f;
			float v = l*w*h;
			float SA = (2*(l*w))+(2*(l*h))+(2*(h*w));
			float mfp = ((4*v)/SA)*3.2808399f;
			return mfp;
		
		}else{
			float l = length;
			float w = width;
			float h = height;
			float v = l*w*h;
			float SA = (2*(l*w))+(2*(l*h))+(2*(h*w));
			float mfp = (4*v)/SA;
			return mfp;
		}
		
	}
	public float CriticalDistance(float[]rt, float length, float width, float height, bool unit){
		
		if(unit){
			float l = length/3.2808399f;
			float w = width/3.2808399f;
			float h = height/3.2808399f;
			float v = l*w*h;
			float avgRt = (rt[0]+rt[1]+rt[2]+rt[3]+rt[4]+rt[5])/6;

			float dist = 0.057f*Mathf.Sqrt(v/avgRt);
			return dist*3.2808399f;
		}else{
			float l = length;
			float w = width;
			float h = height;
			float v = l*w*h;
			float avgRt = (rt[0]+rt[1]+rt[2]+rt[3]+rt[4]+rt[5])/6;

			float dist = 0.057f*Mathf.Sqrt(v/avgRt);
			return dist;
		}
		
	}
}

	

