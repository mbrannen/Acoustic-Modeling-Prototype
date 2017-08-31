using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class main : MonoBehaviour {

	public GameObject dataDump;


	public GameObject wall1;
	public GameObject wall2;
	public GameObject wall3;
	public GameObject wall4;
	public GameObject floor;
	public GameObject ceiling;
	public Camera camera1;

	public Slider sliderL;
	public Slider sliderW;
	public Slider sliderH;

	public Text unit1;
	public Text unit2;
	public Text unit3;

	public Text input1;
	public Text input2;
	public Text input3;


	public bool unit = true; //true = ft, false = m
	public float roomLength = 40;
	public float roomWidth = 20;
	public float roomHeight = 10;

	// Use this for initialization
	void Start () {
		wall1.transform.localScale = new Vector3 ((roomLength / 10), 1, (roomHeight / 10));
		wall1.transform.position = new Vector3 (0, 5, -10);

		wall2.transform.localScale = new Vector3 ((roomWidth / 10), 1, (roomHeight / 10));
	    wall2.transform.position = new Vector3 (-20, 5, 0);

		wall3.transform.localScale = new Vector3 ((roomLength / 10), 1, (roomHeight / 10));
		wall3.transform.position = new Vector3 (0, 5, 10);

		wall4.transform.localScale = new Vector3 ((roomWidth / 10), 1, (roomHeight / 10));
		wall4.transform.position = new Vector3 (20, 5, 0);

		floor.transform.localScale = new Vector3 ((roomLength / 10), 1, (roomWidth / 10));
		floor.transform.position = new Vector3 (0, 0, 0);

		ceiling.transform.localScale = new Vector3 ((roomLength / 10), 1, (roomWidth / 10));
		ceiling.transform.position = new Vector3 (0, 10, 0);



		sliderL.minValue = 0;
		sliderL.maxValue = 100;
		sliderL.wholeNumbers = false;
		sliderL.value = roomLength;

		sliderW.minValue = 0;
		sliderW.maxValue = 100;
		sliderW.wholeNumbers = false;
		sliderW.value = roomWidth;

		sliderH.minValue = 0;
		sliderH.maxValue = 50;
		sliderH.wholeNumbers = false;
		sliderH.value = roomHeight;

		input1.text = "" + roomLength;
		input2.text = "" + roomWidth;
		input3.text = "" + roomHeight;
	}
	
	// Update is called once per frame
	void Update () {
		
		input1.text = "" + roomLength;
		input2.text = "" + roomWidth;
		input3.text = "" + roomHeight;


	}
	public void UpdateL(float x){
		roomLength = sliderL.value;
		dataStorage dataStorage = dataDump.GetComponent<dataStorage>();
		dataStorage.setLength(roomLength);
		//cameraScaling (roomLength,roomWidth,roomHeight);

		wall1.transform.position = new Vector3 (((roomLength/2) - 20), 5, -10);
		wall1.transform.localScale = new Vector3 ((roomLength / 10), 1, (roomHeight / 10));

		wall3.transform.position = new Vector3 (((roomLength/2) - 20), 5, roomWidth - 10);
		wall3.transform.localScale = new Vector3 ((roomLength / 10), 1, (roomHeight / 10));

		wall4.transform.position = new Vector3 (roomLength -20, 5, ((roomWidth/2) - 10));
		wall4.transform.localScale = new Vector3 ((roomWidth / 10), 1, (roomHeight / 10));

		floor.transform.position = new Vector3 (((roomLength/2) - 20), (5-(roomHeight/2)), ((roomWidth/2) - 10));
		floor.transform.localScale = new Vector3 ((roomLength / 10), 1, (roomWidth / 10));

		ceiling.transform.position = new Vector3 (((roomLength/2) - 20), (5+(roomHeight/2)), ((roomWidth/2) - 10));
		ceiling.transform.localScale = new Vector3 ((roomLength / 10), 1, (roomWidth / 10));


	}
	public void UpdateW(float y){
		roomWidth = sliderW.value;
		roomLength = sliderL.value;
		dataStorage dataStorage = dataDump.GetComponent<dataStorage>();
		dataStorage.setWidth(roomWidth);
		//cameraScaling (roomLength,roomWidth,roomHeight);
		//camera1.transform.localRotation = new Quaternion (-18+roomLength,207+roomWidth,0,0);

		wall2.transform.position = new Vector3 (-20, 5, ((roomWidth/2) - 10));
		wall2.transform.localScale = new Vector3 ((roomWidth / 10), 1, (roomHeight / 10));

		wall3.transform.position = new Vector3 (((roomLength/2) - 20), 5, roomWidth - 10);
		wall3.transform.localScale = new Vector3 ((roomLength / 10), 1, (roomHeight / 10));

		wall4.transform.position = new Vector3 (roomLength -20, 5, ((roomWidth/2) - 10));
		wall4.transform.localScale = new Vector3 ((roomWidth / 10), 1, (roomHeight / 10));

		floor.transform.position = new Vector3 (((roomLength/2) - 20), (5-(roomHeight/2)), ((roomWidth/2) - 10));
		floor.transform.localScale = new Vector3 ((roomLength / 10), 1, (roomWidth / 10));

		ceiling.transform.position = new Vector3 (((roomLength/2) - 20), (5+(roomHeight/2)), ((roomWidth/2) - 10));
		ceiling.transform.localScale = new Vector3 ((roomLength / 10), 1, (roomWidth / 10));

	}

	public void UpdateH(float z){
		roomHeight = sliderH.value;
		dataStorage dataStorage = dataDump.GetComponent<dataStorage>();
		dataStorage.setHeight(roomHeight);

		floor.transform.position = new Vector3 (((roomLength/2) - 20), (5-(roomHeight/2)), ((roomWidth/2) - 10));
		ceiling.transform.position = new Vector3 (((roomLength/2) - 20), (5+(roomHeight/2)), ((roomWidth/2) - 10));
		wall1.transform.localScale = new Vector3 ((roomLength / 10), 1, (roomHeight / 10));
		wall2.transform.localScale = new Vector3 ((roomWidth / 10), 1, (roomHeight / 10));
		wall3.transform.localScale = new Vector3 ((roomLength / 10), 1, (roomHeight / 10));
		wall4.transform.localScale = new Vector3 ((roomWidth / 10), 1, (roomHeight / 10));

		camera1.transform.localPosition = new Vector3(103.7f,roomHeight+18.1f,94.4f);;
	}

	public void unitUpdate(){
		dataStorage dataStorage = dataDump.GetComponent<dataStorage>();
		if(unit){

			sliderL.value = roomLength/3.2808399f;
			sliderW.value = roomWidth/3.2808399f;
			sliderH.value = roomHeight/3.2808399f;
			unit1.text = "m"; 
			unit2.text = "m" ;
			unit3.text = "m";
			unit = false;
			dataStorage.setUnit(unit);
		}else{
			sliderL.value = roomLength*3.2808399f;
			sliderW.value = roomWidth*3.2808399f;
			sliderH.value = roomHeight*3.2808399f;
			unit1.text = "ft." ;
			unit2.text = "ft." ;
			unit3.text = "ft.";
			unit = true;
			dataStorage.setUnit(unit);
			
		}

	}
	public void cameraScaling(float x, float y, float z){
		float fov = 43.8603935f;
		float temp = ((x + y + z) / 3);
		camera1.fieldOfView = fov * (Mathf.Log10 (temp));
		
	}

}
