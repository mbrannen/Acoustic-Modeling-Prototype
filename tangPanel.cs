using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tangPanel : MonoBehaviour {
	public Text[] lengthText;
	public Text[] widthText;
	public Text[] heightText;
	public dataStorage dS;
	public Color color;
	public Color colorS;


	// Use this for initialization
	void Start () {
		color.r = 0.47f;
		color.g = 0f;
		color.b = 1f;
		color.a = 255f;
		colorS.r = 1f;
		colorS.g = 1f;
		colorS.b = 1f;
		colorS.a = 255f;

		for(int i = 0; i<3;i++){
			for(int j = 0; j<10;j++){
				if(i==0){
		
	
					if (dS.modeTan[i][j]<= 15f){

						color.g = 0f;
					
					}
					if(dS.modeTan[i][j] <= 255f){
						color.g = (dS.modeTan[i][j] - 15f)/255;	
					
					}
					else{
						color.g = 1f;
	
					}

					lengthText[j].text = dS.modeTan[i][j].ToString("f1");
					lengthText[j].color = color;


				if(i==1){
						if (dS.modeTan[i][j]<= 15f){
							color.g = 0f;
						}
						if(dS.modeTan[i][j] <= 255f){
							color.g = (dS.modeTan[i][j] - 15f)/255f;	

						}
						else{
							color.g = 1f;

						}
					widthText[j].text = dS.modeTan[i][j].ToString("f1");
					widthText[j].color = color;
				}
				if(i==2){
						if (dS.modeTan[i][j]<= 15f){
							color.g = 0f;
						}
						if(dS.modeTan[i][j] <= 255f){
							color.g = (dS.modeTan[i][j] - 15f)/255;	
						}
						else{
							color.g = 1f;
						}
					heightText[j].text = dS.modeTan[i][j].ToString("f1");
					heightText[j].color = color;
				}
			}
		}

	
	}
	}
	
	// Update is called once per frame
	void Update () {
		float sFreq = dS.schroederFreq;
		float sFreqMin = sFreq - 1.5f;
		float sFreqMax = sFreq + 2.5f;
		color.r = .47f;
		for(int i = 0; i<3;i++){
			for(int j = 0; j<10;j++){
				if(i==0){
					if (dS.modeTan[i][j]<= 15f){
						color.g = 0f;

					}
					if(dS.modeTan[i][j] <= 255f){


						color.g = (dS.modeTan[i][j] - 15f)/255f;	
					
					}
					else{
						color.g = 1f;

					}
					if(dS.modeTan[i][j] > sFreqMin && dS.modeTan[i][j] < sFreqMax){
							lengthText[j].text = dS.mode[i][j].ToString("f1");
							lengthText[j].color = colorS;
						}else{
					lengthText[j].text = dS.modeTan[i][j].ToString("f1");
					lengthText[j].color = color;
						}
				}
				if(i==1){
					if (dS.modeTan[i][j]<= 15f){
						color.g = 0f;

					}
					if(dS.modeTan[i][j] <= 255f){
						color.g = (dS.mode[i][j] - 15f)/255f;	

					}
					else{
						color.g = 255f;
					
					}
					if(dS.modeTan[i][j] > sFreqMin && dS.modeTan[i][j] < sFreqMax){
						widthText[j].text = dS.modeTan[i][j].ToString("f1");
						widthText[j].color = colorS;
					}else{
					widthText[j].text = dS.modeTan[i][j].ToString("f1");
					widthText[j].color = color;
					}
				}
				if(i==2){
					if (dS.modeTan[i][j]<= 15f){
						color.g = 0f;

					}
					if(dS.modeTan[i][j] <= 255f){
						color.g = (dS.modeTan[i][j] - 15f)/255f;	

					}
					else{
						color.g = 1f;

					}
					if(dS.modeTan[i][j] > sFreqMin && dS.modeTan[i][j] < sFreqMax){
						heightText[j].text = dS.modeTan[i][j].ToString("f1");
						heightText[j].color = colorS;
					}else{
					heightText[j].text = dS.modeTan[i][j].ToString("f1");
					heightText[j].color = color;
					}
				}
			}
		}
	
	}
}
