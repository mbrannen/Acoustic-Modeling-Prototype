using UnityEngine;
using System.Collections;
//using UnityEditor;

public class particleStretching : MonoBehaviour {

	public GameObject lengthContainer;
	public ParticleSystem plm11;
	public ParticleSystem plm12;
	public ParticleSystem plm21;
	public ParticleSystem plm22;
	public ParticleSystem plm31;
	public ParticleSystem plm32;
	public Gradient grad;
	public GradientColorKey[] gradCol;
	public GradientAlphaKey[] gradAlph;
	public ParticleSystem comp;
	public ParticleSystem comp2;

	// Use this for initialization
	void Start () {
		plm31.startLifetime = 6;
		plm32.startLifetime = 6;
		comp = plm21.GetComponent<ParticleSystem>();
		comp2 = plm22.GetComponent<ParticleSystem>();
		var col = comp.colorOverLifetime;
		var col2 = comp2.colorOverLifetime;
		col.enabled = false;
		col2.enabled = false;
		gradCol = new GradientColorKey[8];
		gradCol[0].color = Color.green;
		gradCol[0].time = 0.0f;
		gradCol[1].color = Color.yellow;
		gradCol[1].time = 0.1f;
		gradCol[2].color = Color.red;
		gradCol[2].time = 0.25f;
		gradCol[3].color = Color.yellow;
		gradCol[3].time = 0.4f;
		gradCol[4].color = Color.green;
		gradCol[4].time = 0.5f;
		gradCol[5].color = Color.yellow;
		gradCol[5].time = 0.6f;
		gradCol[6].color = Color.red;
		gradCol[6].time = 0.75f;
		gradCol[7].color = Color.yellow;
		gradCol[7].time = 0.9f;
	//	gradCol[8].color = Color.green;
		//gradCol[8].time = 1.0f;
		gradAlph = new GradientAlphaKey[6];
		gradAlph[0].alpha = 0.47f;
		gradAlph[0].time = 0.0f;
		gradAlph[1].alpha = 0.03f;
		gradAlph[1].time = 0.25f;
		gradAlph[2].alpha = 0.47f;
		gradAlph[2].time = 0.5f;
		gradAlph[3].alpha = 0.03f;
		gradAlph[3].time = 0.75f;
		gradAlph[4].alpha = 0.47f;
		gradAlph[4].time = 0.90f;
		gradAlph[5].alpha = 0.2f;
		gradAlph[5].time = 1.0f;

		grad.SetKeys(gradCol,gradAlph);
		col.color = new ParticleSystem.MinMaxGradient(grad);
		col2.color = new ParticleSystem.MinMaxGradient(grad);
		col.enabled = true;
		col2.enabled = true;

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void lengthParticleChange(float scale){
		



		float scaleTrimmed = scale/10f;
		lengthContainer.transform.localScale = new Vector3(scaleTrimmed,0,1);
		lengthContainer.transform.position = new Vector3 (((scale/2) - 20), 2, 0);
		plm11.startLifetime = (scaleTrimmed*0.75f);
		plm12.startLifetime = (scaleTrimmed*0.75f);
		plm21.startLifetime = (scaleTrimmed*0.75f);
		plm22.startLifetime = (scaleTrimmed*0.75f);
		plm31.startLifetime = (scaleTrimmed*0.75f);
		plm32.startLifetime = (scaleTrimmed*0.75f);



	}

}
