using UnityEngine;
using System.Collections;
using MySpace.Synthesizer;
using MySpace.Synthesizer.PM8;

public class synth : MonoBehaviour {

	private MyMixer mix;
	private MySynthesizer s;
	private ToneParam tone;


	// Use this for initialization
	void Start () {
		string defaultTone = "@pm8[7 0 op1[0 0 1 0 0 0 0 1 0 14 0 0 0 7] op2[0 0 1 0 0 0 0 1 127 31 0 0 0 15] op3[0 0 1 0 0 0 0 1 127 31 0 0 0 15] op4[0 0 1 0 0 0 0 1 127 31 0 0 0 15] lfo[0 127 0 0 31 0 0 0 0]]";
		tone = new ToneParam(defaultTone);

		mix = new MyMixer(48000, false);         // <-- raise worker thread!
		gameObject.AddComponent<MusicPlayer>();
		//gameObject.GetComponent<MusicPlayer>().Synthesizer.Channel[0].Damper(127);
		gameObject.GetComponent<MusicPlayer>().Synthesizer.Channel[0].ProgramChange(tone);
		// <-- connect synthesizer to mixer
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDisable(){
	                        // <-- disconnect mixer
		mix.Terminate();                        // <-- terminate thread
	
		mix = null;
	}
	public void playTone(float  freq){
		gameObject.GetComponent<MusicPlayer>().Synthesizer.Channel[0].NoteOn((byte)freq,100);

		Debug.Log ("called on");
	}
	public void stopTone(float freq){
		gameObject.GetComponent<MusicPlayer>().Synthesizer.Channel[0].NoteOff((byte)freq);
		//gameObject.GetComponent<MusicPlayer>().Synthesizer.Channel[0].Damper(0);
		gameObject.GetComponent<MusicPlayer>().Synthesizer.Channel[0].AllSoundOff();

		//gameObject.GetComponent<MusicPlayer>().Synthesizer.Channel[0].AllSoundOff();
		Debug.Log ("called off");
	}

	void toolTip(){

	}
}